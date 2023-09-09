using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services
{
    public class SRBoxService
    {
        private readonly IThExpressionRepo thexprepo;
		private readonly IBoxCellRepo cbrepo;
		private readonly IFlashCardRepo fcrepo;

		public SRBoxService(
            IThExpressionRepo thexprepo, 
            IBoxCellRepo cbrepo,
            IFlashCardRepo fcrepo)
        {
            this.thexprepo = thexprepo;
			this.cbrepo = cbrepo;
			this.fcrepo = fcrepo;
		}

        public IEnumerable<int> GetCards(int nodeId, DateTime dt, int lngId)
        {

        }

        public OperationResult<FlashCard> CreateCard(int expressionId)
        {
            ThExpression expr;


			try
            {
				expr = thexprepo.GetById(expressionId);
			}
            catch (Exception ex)
            {
                return new OperationResult<FlashCard>(false, ex.Message, null);
            }

            FlashCard card = new FlashCard
            {
                expression = expr,
				LastExam = DateTime.Now,
                NextExamDate = DateTime.Now,
                rightSolutionScores = 0,
                boxCellNo = cbrepo.GetFirstCell().cellName
			};

            try
            {
                fcrepo.Create(card);
                return new OperationResult<FlashCard>(true, "success", );
            }
            catch (Exception ex)
            {

            }



		}

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
                        //exp.scores += 1;
                        //thexprepo.UpdateInt(exp.id, "scores", exp.scores);

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