using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Repos.Common;
using ThoughtzLand.ImplementRepo.SQLitePepo.Entities.FlashCards;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
    public class FlashCardRepoSQLite: IFlashCardRepo
	{
		private readonly AppData db;
		private readonly PropertyUpdater<FlashCardDb, FlashCard> tool;
		private readonly PropertyUpdater<FlashCardDb> propertyUpdater;
		IMapper mapper;

		public FlashCardRepoSQLite(AppData db)
		{
			this.db = db;
			tool = new PropertyUpdater<FlashCardDb, FlashCard>(db);
			propertyUpdater = new PropertyUpdater<FlashCardDb>(db);

			var mapCfg = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<FlashCard, FlashCardDb>().ReverseMap();
				cfg.CreateMap<FlashCardAnswer, FlashCardAnswerDb>().ReverseMap();
				cfg.CreateMap<FlashCardTitle, FlashCardDb>().ReverseMap();
			});

			mapper = mapCfg.CreateMapper();
		}

		public IEnumerable<FlashCardTitle> GetCards(int nodeId, DateTime dt)
		{
			var flashCardsQuery = db.FlashCards
						.Where(card => card.nodeId == nodeId && card.nextExamDate <= dt)
						//.Include(card => card.answers)
						.Include(card => card.language)
						.Select(card => new FlashCardTitle
						{
							id = card.id,
							lngTag = card.language.tag,
							nodeId = nodeId,
							question = card.question,
							requiredHits = card.requiredHits,
							hitsInRow = card.hitsInRow,
							totalHits = card.totalHits,
							level = card.level,
							//answersCount = card.answers.Count
						})
						.ToArray();

			return flashCardsQuery;
		}

		public FlashCard Get(int cid)
		{
			var res = db.FlashCards
				.Where(card => card.id == cid)
				.Include(card => card.language)
				.Include(card => card.answers)
				.ThenInclude(answer => answer.language)
				.FirstOrDefault();

			return mapper.Map<FlashCard>(res);
		}

		public int UpdateProperties(FlashCard ent, params Expression<Func<FlashCard, object>>[] propSelectors)
		{
			return tool.UpdateProperties(ent, propSelectors);
		}

		public FlashCard Create(CreateFlashCardCoreDto dto)
		{
			var ent = new FlashCardDb { 
				nodeId = dto.nodeId,
				languageId = dto.languageId,
				nextExamDate = dto.nextExamDate,
				hitsInRow = 0,
				totalHits = 0,
				requiredHits = dto.requiredHits,
				question = dto.question,
				description = dto.description,
				level = dto.level,
				questPrice = dto.completedQuestPrice,
				isCompleted = dto.isCompleted
			};

			db.FlashCards.Add(ent);
			var success = db.SaveChanges() > 0;
			if (!success)
				throw new InvalidOperationException("something went wrong when creating a node");
			//return mapper.Map<FlashCard>(ent);
			return Get(ent.id);
		}

		public int UpdateProperty(int entId, string propname, object propvalue)
		{
			return propertyUpdater.UpdateProperty(entId, propname, propvalue);
		}

		public FlashCard Update(UpdateFlashCardDto dto)
		{
			// Assuming you have a DbContext instance named _context
			var flashCard = db.FlashCards.Find(dto.id);
			if (flashCard == null)
			{
				// Handle the case where the flash card doesn't exist
				throw new InvalidOperationException("FlashCard not found.");
			}

			// Update fields from the DTO
			flashCard.question = dto.question ?? flashCard.question;
			flashCard.description = dto.description ?? flashCard.description;
			flashCard.languageId = dto.languageId == 0 ? flashCard.languageId : dto.languageId;
			//flashCard.NextExamDate = dto.nextExamDate;
			//flashCard.hitsInRow = dto.hitsInRow;
			//flashCard.requiredHits = dto.requiredHits;
			//flashCard.totalHits = dto.totalHits;

			// Save changes
			db.SaveChanges();

			return Get(flashCard.id);
		}

		//public IEnumerable<FlashCard> GetPlayingCards(int nodeId, DateTime dt)
		public IEnumerable<FlashCard> GetPlayingCards(int nodeId)
		{
			var flashCardsQuery = db.FlashCards
			//.Where(card => card.nodeId == nodeId && card.NextExamDate <= dt)
			.Where(card => card.nodeId == nodeId)
			.Include(card => card.answers)
			.ThenInclude(answ => answ.language)
			.Include(card => card.language)
			.Select(card => mapper.Map<FlashCard>(card))
			.ToArray();

			return flashCardsQuery;
		}

		public CardHitDto? GetCardHit(int cardId)
		{
			return db.FlashCards
				.Where(card => card.id == cardId)
				.Select(card => new CardHitDto
				{
					hitsInRow = card.hitsInRow,
					isCompleted = card.isCompleted,
					level = card.level,
					nextExamDate = card.nextExamDate,
					requiredHits = card.requiredHits,
					totalHits = card.totalHits,
					answers = card.answers.Select(a => a.text),
					id = card.id
				})
				.FirstOrDefault();
		}

		public void UpdateCardHit(UpdateCardHitDto dto)
		{
			var card = db.FlashCards.FirstOrDefault(c => c.id == dto.id);

			if (card != null)
			{
				card.isCompleted = dto.isCompleted;
				card.level = dto.level;
				card.totalHits = dto.totalHits;
				card.hitsInRow = dto.hitsInRow;
				card.level = dto.level;
				card.nextExamDate = dto.nextExamDate;

				db.SaveChanges();
			}
		}
	}
}
