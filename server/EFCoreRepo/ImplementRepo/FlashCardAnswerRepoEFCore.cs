using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.EFCoreRepo.Entities;

namespace ThoughtzLand.ImplementRepo.EFCoreRepo
{
    public class FlashCardAnswerRepoEFCore : IFlashCardAnswerRepo
	{
		private readonly AppData db;
		IMapper mapper;

		public FlashCardAnswerRepoEFCore(AppData db)
		{
			this.db = db;

			var mapCfg = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<FlashCardAnswer, FlashCardAnswerDb>().ReverseMap();
			});

			mapper = mapCfg.CreateMapper();
		}

		public FlashCardAnswer Create(CreateFlashCardAnswerDto dto)
		{
			var ent = new FlashCardAnswerDb
			{
				cardId = dto.cardId,
				languageId = dto.lngId,
				text = dto.text
			};

			db.FlashCardAnswers.Add(ent);
			var success = db.SaveChanges() > 0;
			if (!success)
				throw new InvalidOperationException("something went wrong when creating a node");
			return mapper.Map<FlashCardAnswer>(Get(ent.id));
		}

		public void Remove(int id)
		{

			//var thing = context.Things.Find(id);
			//if (thing != null)
			//{
			//	context.Things.Remove(thing);
			//	context.SaveChanges();
			//}

			var answer = new FlashCardAnswerDb { id = id };
			db.Entry(answer).State = EntityState.Deleted;
			db.SaveChanges();
		}

		public void Update(UpdateCardAnswerDto dto)
		{
			// Create an instance of the FlashCardDb with only the ID set
			var cardAnswer = new FlashCardAnswerDb { id = dto.id };

			// Attach the entity to the context
			db.FlashCardAnswers.Attach(cardAnswer);

			cardAnswer.text = dto.text;
			cardAnswer.languageId = dto.languageId;

			db.Entry(cardAnswer).Property(ca => ca.text).IsModified = true;
			db.Entry(cardAnswer).Property(ca => ca.languageId).IsModified = true;

			db.SaveChanges();
		}

		public int UpdateProperty(int entId, string propname, object propvalue)
		{
			return propertyUpdater.UpdateProperty(entId, propname, propvalue);
		}

		private FlashCardAnswerDb Get(int id)
		{
			return db.FlashCardAnswers.Include(x => x.language).FirstOrDefault(x => x.id == id);
		}
	}
}
