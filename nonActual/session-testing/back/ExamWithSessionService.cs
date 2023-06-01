using Models;
using Models.Exam;
using Models.Exam.dto;
using Models.Location;
using Services.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /*
     * 30-03-2023
     * Кажется надо что то более универсальное в логическом плане и именовании сделать.
     * Универсальный сервис проверки ответа;
     * - один вопрос
     * - объект собирает группу вопросов
     * - объект, имеет коллекцию вопросов; одно прохождение - минус хит поинты
     * 
     */

    public class ExamWithSessionService
    {
        private readonly DbFactory factory;

        public ExamWithSessionService(DbFactory factory)
        {
            this.factory = factory;
        }

        /*
         * 18-05-2023
         * Нужно теперь взять все мысли в рамказ данного узла
         * 
         */
        public void StartSession(Node node)
        {
            using (var db = factory.Create())
            {
                if (db.ExamSessions.FirstOrDefault(x => x.nodeId == node.id) != null)
                    throw new InvalidOperationException($"Mission `{node.id}` is already in progress");

                var testingMission = db.Nodes.FirstOrDefault(x => x.id == node.id);
                if (testingMission == null)
                    throw new InvalidOperationException($"No testing mission with id = {node.id}");

                ExamSession sess = new ExamSession();
                sess.nodeId = node.id;
                sess.ThoughtIds = db.Thoughts.Where(x => x.nodeId == node.id).Select(x => x.id).ToList();
                sess.Total = 0;
                sess.TotalPoints = sess.ThoughtIds.Count();

                db.ExamSessions.Add(sess);
                db.SaveChanges();
            }
        }

        public ExamSession GetSession(int missionId)
        {
            using (var db = factory.Create())
            {
                return db.ExamSessions.FirstOrDefault(x => x.nodeId == missionId);
            }
        }

        // Лучше искать последний вопрос в сессии, так как на клиенте нам известна коллекция сессий
        // Если сессия не завершена, ее можно открыть либо в окне ноды либо в глобальной странице начатых сессий
        // Когда загружается страница Node там же подгружается и незавершенная сессия, если есть.
        public TestingQuestionFrame CurrentQuestion(Node node)
        {
            using (var db = factory.Create())
            {
                var sess = db.ExamSessions.FirstOrDefault(x => x.nodeId == node.id);

                if (sess == null)
                    throw new InvalidOperationException($"Mission with id = {node.id} is passive");

                var lexemId = sess.ThoughtIds.First();

                var q = db.Thoughts.FirstOrDefault(x => x.id == lexemId);

                return new TestingQuestionFrame
                {
                    lexemId = q.id,
                    Left = sess.ThoughtIds.Count,
                    text = q.text,
                    Total = sess.TotalPoints
                };
            }
        }

        /* 
         * Необходимо протестировать позицию именно в контексте сессии.
         * Так как даже если уже проверена точка, 
         */
        public SolutionCheckResult CheckSolutionAndNext(QuestSolution sol)
        {
            using (var db = factory.Create())
            {
                var thought = db.Thoughts.FirstOrDefault(x => x.id == sol.thoughtId);
                if (thought == null)
                    throw new InvalidOperationException($"There is no thought with id = {sol.thoughtId}");

                var isCorrect = db.ThExpressions
                    .Any(x => x.thoughtId == thought.id && x.text.ToLower().Equals(sol.solution.ToLower()));

                var sess = db.ExamSessions.FirstOrDefault(x => x.nodeId == thought.nodeId);
                bool isCompleted = false;
                if (sess != null)
                {
                    if(!sess.ThoughtIds.Any(x => x == thought.id))
                        throw new InvalidOperationException($"Word `{thought.text}` is not included into any current session");

                    sess.ThoughtIds.Remove(thought.id);

                    if (isCorrect)
                    {
                        var testingPointCount = db.Thoughts.Count(x => x.nodeId == sess.nodeId);
                        sess.Total += 100.0m / testingPointCount;
                        //thought.scores++;
                    }

                    if(sess.ThoughtIds.Count == 0)// completed; очки жизни (прочности) закончились
                    {
                        isCompleted = true;

                        Grade grade = new Grade();
                        grade.nodeId = sess.nodeId;
                        grade.value = sess.Total;
                        grade.Date = DateTime.Now;
                        db.Grades.Add(grade);

                        db.ExamSessions.Remove(sess);
                    }

                    db.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"No session started with thought `{thought.text}`");
                }

                return new SolutionCheckResult
                {
                    CorrectStrings = db.ThExpressions.Where(x => x.thoughtId == sol.thoughtId).Select(x => x.text).ToArray(),
                    isCorrect = isCorrect,
                    Completed = isCompleted
                };
            }
        }
    }
}
