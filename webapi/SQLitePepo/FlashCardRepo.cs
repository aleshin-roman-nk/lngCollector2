using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Repos.Common;
using ThoughtzLand.ImplementRepo.SQLitePepo.Entities;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	public class FlashCardRepo: /*PropertyUpdater<FlashCard>,*/ IFlashCardRepo
	{
		private readonly AppData db;
		private readonly DtoPropertyUpdater<FlashCard, FlashCardDb> tool;

		public FlashCardRepo(AppData db)/*: base(db)*/
		{
			this.db = db;
			tool = new DtoPropertyUpdater<FlashCard, FlashCardDb>(db);
		}

		private FlashCard toDomain(FlashCardDb e)
		{
			return new FlashCard
			{
				//expressionUnderTest = e.expression,
				id = e.id,
				passed = e.passed,
				NextExamDate = e.NextExamDate,
				rightSolutionScores = e.rightSolutionScores,
				boxCellNo = e.boxCellNo,
			};
		}

		private FlashCardDb toDb(FlashCard e)
		{
			return new FlashCardDb
			{
				//expression = e.expressionUnderTest,
				expressionId = e.expressionUnderTest.id,
				passed = e.passed,
				id = e.id,
				NextExamDate = e.NextExamDate,
				rightSolutionScores = e.rightSolutionScores,
				boxCellNo = e.boxCellNo
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
			return GetById(e.id);
		}

		public IEnumerable<FlashCard> GetCards(int nodeId, DateTime dt)
		{
			/*
			 * найти все карты, выражение которых указывают на мысли, которые принадлежат ноде
			 * 
			 * 
			 */

			var res = db.FlashCards

				.Join(db.ThExpressions,
					card => card.expressionId,
					expression => expression.id,
					(card, expression) => new { Card = card, Expression = expression })

				.Join(db.Thoughts,
					combined => combined.Expression.thoughtId,
					thought => thought.id,
					(combined, thought) => new { combined.Card, combined.Expression, Thought = thought })

				.Where(combined => combined.Thought.nodeId == nodeId && combined.Card.NextExamDate <= dt)

				.Select(x => new FlashCard
				{
					id = x.Card.id,
					boxCellNo = x.Card.boxCellNo,
					passed = x.Card.passed,
					expressionUnderTest = x.Expression,
					NextExamDate = x.Card.NextExamDate,
					rightSolutionScores = x.Card.rightSolutionScores,
					questions = db.ThExpressions.Where(e => e.thoughtId == x.Thought.id && e.lngId != x.Expression.lngId).ToArray()
				})

				.ToArray();

			return res;
		}

		public IEnumerable<FlashCard> GetAll()
		{
			throw new NotImplementedException();
		}

		public void Remove(int entId)
		{
			throw new NotImplementedException();
		}

		public FlashCard Update(FlashCard entity)
		{
			var ent = toDb(entity);

			db.Entry(ent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

			db.SaveChanges();

			return GetById(ent.id);
		}

		public FlashCard GetById(int cid)
		{
			var res = db.FlashCards
				.Join(db.ThExpressions,
						card => card.expressionId,
						expression => expression.id,
						(card, expression) => new { Card = card, Expression = expression })
				.Join(db.Thoughts,
						ce => ce.Expression.thoughtId,
						thought => thought.id,
						(ce, thought) => new { ce.Card, ce.Expression, Thought = thought }
				)

				.Where(x => x.Card.id == cid)

				.Select(x => new FlashCard
				{
					boxCellNo = x.Card.boxCellNo,
					id = x.Card.id,
					NextExamDate = x.Card.NextExamDate,
					passed = x.Card.passed,
					rightSolutionScores = x.Card.rightSolutionScores,
					expressionUnderTest = x.Expression,
					questions = db.ThExpressions.Where(e => e.thoughtId == x.Thought.id && e.lngId != x.Expression.lngId).ToArray()
				})

			   .FirstOrDefault();

			return res;
		}

		public int UpdateProperties(FlashCard ent, params Expression<Func<FlashCard, object>>[] propSelectors)
		{
			return tool.UpdateProperties(ent, propSelectors);
		}

		public int UpdateProperty<TProperty>(FlashCard ent, Expression<Func<FlashCard, TProperty>> propSelector)
		{
			return tool.UpdateProperty(ent, propSelector);
		}
	}
}
