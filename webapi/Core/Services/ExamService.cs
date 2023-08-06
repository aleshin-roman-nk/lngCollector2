using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
    public class ExamService
    {
        private readonly IThExpressionRepo thexprepo;

        public ExamService(IThExpressionRepo thexprepo)
        {
            this.thexprepo = thexprepo;
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

        // На странице ноды где то можно типа истории ответов хранить; или очки.
        // очки получаются путем выборки всех очков всех Expression of Thoughts данной ноды.
        public OperationResult<CheckResult> Check(QuestSolution sol)
        {
            try
            {
                var thexprs = thexprepo.GetByThoughtId(sol.thoughtId);

                foreach (var exp in thexprs)
                {
                    if (exp.text.ToUpper().Equals(sol.solution.ToUpper()))
                    {
                        exp.scores += 1;
                        thexprepo.UpdateInt(exp.id, "scores", exp.scores);

                        return new OperationResult<CheckResult>(true, "success", new CheckResult
                        {
                            correctStrings = thexprs.Select(x => x.text).ToArray(),
                            isCorrect = true
                        });
                    }
                }

                return new OperationResult<CheckResult>(true, "success", new CheckResult { correctStrings = thexprs.Select(x => x.text).ToArray(), isCorrect = false });
            }
            catch (Exception e)
            {
                return new OperationResult<CheckResult>(false, e.Message, null);
            }
        }
    }
}