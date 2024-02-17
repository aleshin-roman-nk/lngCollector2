using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services.FlashCards
{
	public class FlashCardService
	{
		private readonly IFlashCardRepo flashCardRepo;
		private readonly IFlashCardAnswerRepo cardAnswerRepo;
		private readonly CardParametersSchemeProvider CardParametersSchemeProvider;

		public FlashCardService(IFlashCardRepo flashCardRepo, 
			IFlashCardAnswerRepo cardAnswerRepo,
			CardParametersSchemeProvider cardParametersSchemeProvider)
		{
			this.flashCardRepo = flashCardRepo;
			this.cardAnswerRepo = cardAnswerRepo;
			this.CardParametersSchemeProvider = cardParametersSchemeProvider;
		}

		public FlashCard Add(CreateFlashCardDto fc)
		{
			var dto = new CreateFlashCardCoreDto
			{
				description = fc.description,
				languageId = fc.languageId,
				nextExamDate = DateTime.Now,
				nodeId = fc.nodeId,
				question = fc.question,
				requiredHits = CardParametersSchemeProvider.CardParametersScheme.cardAimHitInRow,
				completedQuestPrice = CardParametersSchemeProvider.CardParametersScheme.CompletedQuestPrice,
				level = 1,
				isCompleted = false
			};

			return flashCardRepo.Create(dto);
		}

		public FlashCard GetSingle(int id)
		{
			return flashCardRepo.Get(id);
		}

		public IEnumerable<FlashCardTitle> Get(int nodeid, DateTime date)
		{
			return flashCardRepo.GetCards(nodeid, date);
		}

		//public IEnumerable<FlashCard> GetPlayingCards(int nodeid, DateTime date)
		public IEnumerable<FlashCard> GetPlayingCards(int nodeid)
		{
			//return flashCardRepo.GetPlayingCards(nodeid, date);
			return flashCardRepo.GetPlayingCards(nodeid);
		}

		public FlashCardAnswer AddAnswer(CreateFlashCardAnswerDto answ)
		{
			return cardAnswerRepo.Create(answ);
		}

		//public void CardAnswerUpdateProperty(int entityId, string propertyName, object propertyValue)
		//{
		//	cardAnswerRepo.UpdateProperty(entityId, propertyName, propertyValue);
		//}

		public void CardUpdateProperty(int entityId, string propertyName, object propertyValue)
		{
			flashCardRepo.UpdateProperty(entityId, propertyName, propertyValue);
		}

		public FlashCard UpdateFlashCard(UpdateFlashCardDto dto)
		{
			return flashCardRepo.Update(dto);
		}

		public void UpdateCardAnswer(UpdateCardAnswerDto dto)
		{
			cardAnswerRepo.Update(dto);
		}

		public void DeleteCardAnswer(int answerId)
		{
			cardAnswerRepo.Remove(answerId);
		}
	}
}
