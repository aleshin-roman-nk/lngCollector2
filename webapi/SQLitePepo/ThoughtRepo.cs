using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.Core.Services;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	public class ThoughtRepo : IThoughtRepo
	{
		private readonly AppData db;
		private PropertyUpdater<Thought> propertyUpdater;

		public ThoughtRepo(AppData db)
		{
			this.db = db;
			propertyUpdater = new PropertyUpdater<Thought>(db);
		}

		public bool ThoughtHasWork(int thId)
		{
		//	return db.ThExpressions.Where(x => x.thoughtId == thId).Sum(x => x.scores) > 0;
			throw new NotImplementedException();
		}

		public Thought GetById(int id)
		{
			var ent = db.Thoughts.Include(x => x.expressions).FirstOrDefault(x => x.id == id);

			if (ent != null)
			{
				return ent;
			}
			else throw new InvalidOperationException($"no object with it = {id}");
		}

		public IEnumerable<Thought> GetAll()
		{
			throw new NotImplementedException();
		}

		public Thought Create(Thought th)
		{
			if (th.nodeId == 0)
				throw new InvalidOperationException("thought must be assotiated with a node");

			th.id = 0;
			th.createdDate = DateTime.Now;

			db.Thoughts.Add(th);
			var success = db.SaveChanges() > 0;
			if (success)
				return th;
			else
				throw new InvalidOperationException("something went wrong when creating a thought");
		}

		public Thought Update(Thought entity)
		{
			throw new NotImplementedException("Updating whole Thought object is not supported. Consider using UpdateString/UpdateInt");
		}

		public void Remove(int entId)
		{
			var o = db.Thoughts
				.Where(x => x.id == entId)
				//.Select(x => new Thought { id = entId })
				.FirstOrDefault();

			if (o == null) throw new InvalidOperationException($"No object with id = {entId} exists in data base");

			var expressions = db.ThExpressions.Where(x => x.thoughtId == entId).ToArray();
			db.ThExpressions.RemoveRange(expressions);
			db.Thoughts.Remove(o);
			db.SaveChanges();
		}

		public void UpdateInt(int entId, string propname, int propvalue)
		{
			throw new NotImplementedException();
		}

		public void UpdateString(int entId, string propname, string propvalue)
		{
			propertyUpdater.UpdateString(entId, propname, propvalue);
		}
	}
}
