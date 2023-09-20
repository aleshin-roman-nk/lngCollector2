using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	/// <summary>
	/// Update a property of TDbEntity entity
	/// </summary>
	/// <typeparam name="TDtoEntity">Dto entity</typeparam>
	/// <typeparam name="TDbEntity">Db entity</typeparam>
	internal class DtoPropertyUpdater<TDtoEntity, TDbEntity>
		where TDtoEntity: class, IDbEntity
		where TDbEntity : class, IDbEntity, new()
	{
		private readonly AppData db;

		public DtoPropertyUpdater(AppData db)
		{
			this.db = db;
		}

		public int UpdateProperties(TDtoEntity dtoEnt, params Expression<Func<TDtoEntity, object>>[] propertySelectors)
		{
            if (dtoEnt == null)
			{
				throw new ArgumentNullException(nameof(dtoEnt));
			}

			var dbSet = db.Set<TDbEntity>();
			TDbEntity? dbEnt = dbSet.Local.FirstOrDefault(x => x.id == dtoEnt.id);
			if(dbEnt == null)
				dbEnt = dbSet.Where(x => x.id == dtoEnt.id).Select(x => new TDbEntity { id = x.id}).FirstOrDefault();
			if (dbEnt == null)
				throw new InvalidOperationException($"no object with id = {dtoEnt.id} of {typeof(TDbEntity).Name}");

			foreach (var propertySelector in propertySelectors)
			{
                //if (propertySelector == null)
                //{
                //	throw new ArgumentNullException(nameof(propertySelector));
                //}

                Console.WriteLine(propertySelector.Body.Print());

				//if (!(propertySelector.Body is MemberExpression memberExpression))
				//{
				//	throw new ArgumentException("Invalid property selector. Please use a lambda expression that selects a property.");
				//}

				//var propertyName = memberExpression.Member.Name;
				//var func = propertySelector.Compile();
				//var propertyValue = func(dtoEnt);

				//var entry = db.Entry(dbEnt);
				//entry.Property(propertyName).CurrentValue = propertyValue;
				//entry.Property(propertyName).IsModified = true;
			}

			//return db.SaveChanges();
			return 1;
		}

		public int UpdateProperty<TProperty>(TDtoEntity dtoEnt, Expression<Func<TDtoEntity, TProperty>> propertySelector)
		{
			if (dtoEnt == null)
			{
				throw new ArgumentNullException(nameof(dtoEnt));
			}

			var dbSet = db.Set<TDbEntity>();
			TDbEntity? dbEnt = dbSet.Local.FirstOrDefault(x => x.id == dtoEnt.id);
			if (dbEnt == null)
				dbEnt = dbSet.Where(x => x.id == dtoEnt.id).Select(x => new TDbEntity { id = x.id }).FirstOrDefault();
			if (dbEnt == null)
				throw new InvalidOperationException($"no object with id = {dtoEnt.id} of {typeof(TDbEntity).Name}");

			if (propertySelector == null)
			{
				throw new ArgumentNullException(nameof(propertySelector));
			}

			Console.WriteLine(propertySelector.Body.Print());

			if (!(propertySelector.Body is MemberExpression memberExpression))
			{
				throw new ArgumentException("Invalid property selector. Please use a lambda expression that selects a property.");
			}

			var propertyName = memberExpression.Member.Name;
			var func = propertySelector.Compile();
			var propertyValue = func(dtoEnt);

			var entry = db.Entry(dbEnt);
			entry.Property(propertyName).CurrentValue = propertyValue;
			entry.Property(propertyName).IsModified = true;

			return db.SaveChanges();
		}
	}
}
