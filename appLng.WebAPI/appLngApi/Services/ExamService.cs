using Models.Exam.dto;
using Models.Thought;
using Services.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ExamService : IExamService
    {
        private readonly IThoughtRepo threpo;
        private readonly IThExpressionRepo thexprepo;
        private readonly IQuestionRepo questionRepo;

        public ExamService(IThoughtRepo threpo, IThExpressionRepo thexprepo, IQuestionRepo questionRepo)
        {
            this.threpo = threpo;
            this.thexprepo = thexprepo;
            this.questionRepo = questionRepo;
        }

        //public IEnumerable<Thought> GetThoughts(int nodeId /*добавить на каком языке выражения... нужна коллекция строк для теста, и это должно быт связанно с мыслью*/)
        //{
        //    return new List<Thought>
        //    {
        //        new Thought{ nodeId = nodeId, text = "rrrrr"},
        //        new Thought{ nodeId = nodeId, text = "nnnnnn"}
        //    };
        //}

        /*
 * Именно здесь возвращаем пункты для теста.
 * Возвращаем коллекцию Thought и их коллекции ThExpression
 * Клиент должен запросить проверить решение пользователя.
 * Вопрос буджет состоять из выражения на выбранном языке и идентификаторе Thought.
 * Этого достаточно чтобы проверить правильность решения.
 * 
 * Для составления нужно отдать клиенту коллекцию мыслей и их все выражения.
 * -- клиент сам решает как задать вопрос.
 * -- решение пользователя теперь будет состоять из строки, id мысли, id языка ответа
 * 
 */

        public IEnumerable<Question> GetQuestions(int nodeId)
        {
            return questionRepo.GetQuestions(nodeId);
        }

        // На странице ноды где то можно типа истории ответов хранить; или очки.
        // очки получаются путем выборки всех очков всех Expression of Thoughts данной ноды.
        public CheckResult Check(QuestSolution sol)
        {
            var exps = thexprepo.GetAllOf(sol.thoughtId);

            foreach (var exp in exps)
            {
                if (exp.text.ToUpper().Equals(sol.solution.ToUpper()))
                {
                    thexprepo.AddScore(exp.id, 1);

                    return new CheckResult
                    {
                        correctStrings = exps.Select(x => x.text).ToArray(),
                        isCorrect = true
                    };
                }

            }

            return new CheckResult
            {
                correctStrings = exps.Select(x => x.text).ToArray(),
                isCorrect = false
            };
        }
    }
}