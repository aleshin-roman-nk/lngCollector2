using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.ImplementRepo.SQLitePepo.Entities;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	public class FlashCardRepo : IFlashCardRepo
	{
		private readonly AppData db;

		public FlashCardRepo(AppData db) 
		{
			this.db = db;
		}

		private FlashCard toDomain(FlashCardDb e)
		{
			return new FlashCard
			{
				expression = e.expression,
				id = e.id,
				LastExam = e.LastExam,
				NextExamDate = e.NextExamDate,
				rightSolutionScores = e.rightSolutionScores,
				SpacedRepetitionBoxCellId = e.SpacedRepetitionBoxCellId
			};
		}

		private FlashCardDb toDb(FlashCard e)
		{
			return new FlashCardDb
			{
				expression = e.expression,
				expressionId = e.expression.id,
				id = e.id,
				LastExam = e.LastExam,
				NextExamDate = e.NextExamDate,
				rightSolutionScores = e.rightSolutionScores,
				SpacedRepetitionBoxCellId = e.SpacedRepetitionBoxCellId
			};
		}

		public FlashCard Create(FlashCard entity)
		{
			entity.id = 0;

			var e = toDb(entity);

			db.FlashCards.Add(e);
			var success = db.SaveChanges() > 0;
			if (!success)
				throw new InvalidOperationException("something went wrong when creating a node");
			return toDomain(e);
		}

		public IEnumerable<FlashCard> GetAll()
		{
			throw new NotImplementedException();
		}

		public FlashCard GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Remove(int entId)
		{
			throw new NotImplementedException();
		}

		public FlashCard Update(FlashCard entity)
		{
			throw new NotImplementedException();
		}
	}
}
