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

		/// <summary>
		/// Returns all cards available on a given date dt
		/// </summary>
		/// <param name="nodeId"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		public OperationResult<IEnumerable<FlashCard>> GetCards(int nodeId, DateTime dt)
		{
			try
			{
				return new OperationResult<IEnumerable<FlashCard>>(true, "success", fcrepo.GetCards(nodeId, dt));
			}
			catch (Exception ex)
			{
				return new OperationResult<IEnumerable<FlashCard>>(false, ex.Message, null);
			}
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

			var questions = thexprepo.GetByThoughtId(expr.thoughtId);
			questions = questions.Where(x => x.lngId != expr.lngId);

			FlashCard card = new FlashCard
			{
				expressionUnderTest = expr,
				NextExamDate = DateTime.Now,
				rightSolutionScores = 0,
				boxCellNo = cbrepo.GetFirstCell().cellName,
				questions = questions
			};

			try
			{
				var res = fcrepo.Create(card);
				return new OperationResult<FlashCard>(true, "success", res);
			}
			catch (Exception ex)
			{
				return new OperationResult<FlashCard>(false, ex.Message, null);
			}
		}

		public OperationResult<FlashCard> GetCard(int id)
		{
			try
			{
				var res = fcrepo.GetById(id);
				return new OperationResult<FlashCard>(true, "success", res);
			}
			catch (Exception ex)
			{
				return new OperationResult<FlashCard>(false, ex.Message, null);
			}
		}

		// На странице ноды где то можно типа истории ответов хранить; или очки.
		// очки получаются путем выборки всех очков всех Expression of Thoughts данной ноды.
		public OperationResult<CheckResult> Check(CardSolution sol)
		{
			try
			{
				var card = fcrepo.GetById(sol.cardId);

				//!!! Ответ в sol.solution должен совпадать с card.expressionUnderTest.text

				if (card.expressionUnderTest.text.ToUpper().Equals(sol.solution.ToUpper()))
				{
					/*
					 * добавить FlashCard.rightSolutionScores
					 * найти следующую ячеку от FlashCard.boxCellNo
					 * обновить дату сл упражнения
					 * 
					 * и вообще загрузить надо FlashCard
					 * 
					 * проверка можно более щадящая, либо по правилу допуска (например отработка буквы)
					 * либо процент, допускается не ниже 85% совпадения по буквам
					 * 
					 * 
					 */

					card.rightSolutionScores += 1;

					fcrepo.UpdateProperties(card, x => x.rightSolutionScores);

					bool passed = false;
					putToNext(card, out passed); // отработать случай, когда карта вышла из последней ячейки.

					if (passed)
					{
						return new OperationResult<CheckResult>(true, "success: box is passed", new CheckResult
						{
							correctString = card.expressionUnderTest.text,
							isCorrect = true
						});
					}

					return new OperationResult<CheckResult>(true, "success", new CheckResult
					{
						correctString = card.expressionUnderTest.text,
						isCorrect = true
					});
				}

				putToStart(card);
				return new OperationResult<CheckResult>(true, "success", new CheckResult { correctString = card.expressionUnderTest.text, isCorrect = false });
			}
			catch (Exception e)
			{
				return new OperationResult<CheckResult>(false, e.Message, null);
			}
		}

		private void putToNext(FlashCard card, out bool passed)
		{
			/*
			 * 1. Текущая ячейка указывает на дату сл повторения
			 * - дату сл повторения сложить из текущей ячейки
			 * 2. Пометить карточку номером сл ячейки
			 * 
			 */

			var cells = cbrepo.GetAll();

			var currentCell = cells.FirstOrDefault(x => x.cellName == card.boxCellNo);

			passed = false;

			if (currentCell != null)
			{
				var nextCell = cells.FirstOrDefault(x => x.cellName == currentCell.cellName + 1);

				if(nextCell != null)
				{
					card.boxCellNo = nextCell.cellName;
					card.NextExamDate = card.NextExamDate.AddMinutes(currentCell.nextIntervalInMinutes);

					fcrepo.UpdateProperties(card, x => x.boxCellNo, x => x.NextExamDate);
				}
				else
				{
					passed = true;
					card.boxCellNo = 0;
					card.passed = true;

					fcrepo.UpdateProperties(card, x => x.passed, x => x.boxCellNo);
				}
			}
		}

		private void putToStart(FlashCard card)
		{
			var cell = cbrepo.GetFirstCell();

			card.boxCellNo = cell.cellName;
			//card.NextExamDate = card.NextExamDate.AddMinutes(30);

			//fcrepo.UpdateProperties(card, x => x.boxCellNo, x => x.NextExamDate);
			fcrepo.UpdateProperty(card, x => x.boxCellNo);
			//fcrepo.UpdateProperty(card, x => x.NextExamDate);
		}
	}
}