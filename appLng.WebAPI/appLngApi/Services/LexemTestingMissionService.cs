using Models;
using Models.dto;
using Services.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LexemTestingMissionService
    {
        private readonly DbFactory factory;

        public LexemTestingMissionService(DbFactory factory)
        {
            this.factory = factory;
        }

        public void StartSession(TestingMission tm)
        {
            using (var db = factory.Create())
            {
                if (db.TestingMissionSessions.FirstOrDefault(x => x.testMissionId == tm.id) != null)
                    throw new InvalidOperationException($"Mission `{tm.id}` is already in progress");

                var testingMission = db.TestingMissions.FirstOrDefault(x => x.id == tm.id);
                if (testingMission == null)
                    throw new InvalidOperationException($"No testing mission with id = {tm.id}");

                TestingMissionSession sess = new TestingMissionSession();
                sess.testMissionId = tm.id;
                sess.Lexems = db.Lexems.Where(x => x.testMissionId == tm.id).Select(x => x.id).ToList();
                sess.Total = 0;
                sess.TotalPoints = sess.Lexems.Count();

                db.TestingMissionSessions.Add(sess);
                db.SaveChanges();
            }
        }

        public TestingMissionSession GetSession(int missionId)
        {
            using (var db = factory.Create())
            {
                return db.TestingMissionSessions.FirstOrDefault(x => x.testMissionId == missionId);
            }
        }

        public TestingQuestionFrame CurrentQuestion(TestingMission tm)
        {
            using (var db = factory.Create())
            {
                var sess = db.TestingMissionSessions.FirstOrDefault(x => x.testMissionId == tm.id);

                if (sess == null)
                    throw new InvalidOperationException($"Mission with id = {tm.id} is passive");

                var lexemId = sess.Lexems.First();

                var q = db.Lexems.FirstOrDefault(x => x.id == lexemId);

                return new TestingQuestionFrame 
                { 
                    lexemId = q.id, 
                    Left = sess.Lexems.Count, 
                    text = q.text, 
                    Total = sess.TotalPoints 
                };
            }
        }

        /* 
         * Необходимо протестировать позицию именно в контексте сессии.
         * Так как даже если уже проверена точка, 
         */
        public SolutionCheckResultFrame CheckSolutionAndNext(QuestSolution sol)
        {
            using (var db = factory.Create())
            {
                var lexem = db.Lexems.FirstOrDefault(x => x.id == sol.lexemId);
                if (lexem == null)
                    throw new InvalidOperationException($"There is no lexem with id = {sol.lexemId}");

                var isCorrect = db.LexemMeanings
                    .Any(x => x.lexemId == lexem.id && x.text.ToLower().Equals(sol.solution.ToLower()));

                var sess = db.TestingMissionSessions.FirstOrDefault(x => x.testMissionId == lexem.testMissionId);
                bool isCompleted = false;
                if (sess != null)
                {
                    if(!sess.Lexems.Any(x => x == lexem.id))
                        throw new InvalidOperationException($"Word `{lexem.text}` is not included into any current session");

                    sess.Lexems.Remove(lexem.id);

                    if (isCorrect)
                    {
                        var testingPointCount = db.Lexems.Count(x => x.testMissionId == sess.testMissionId);
                        sess.Total += 100.0m / testingPointCount;
                    }

                    if(sess.Lexems.Count == 0)// completed; очки жизни (прочности) закончились
                    {
                        isCompleted = true;

                        Grade grade = new Grade();
                        grade.missionId = sess.testMissionId;
                        grade.value = sess.Total;
                        grade.Date = DateTime.Now;
                        db.Grades.Add(grade);

                        db.TestingMissionSessions.Remove(sess);
                    }

                    db.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"No session started with lexem `{lexem.text}`");
                }

                return new SolutionCheckResultFrame
                {
                    CorrectStrings = db.LexemMeanings.Where(x => x.lexemId == sol.lexemId).Select(x => x.text).ToArray(),
                    isCorrect = isCorrect,
                    Completed = isCompleted
                };
            }
        }
    }
}
