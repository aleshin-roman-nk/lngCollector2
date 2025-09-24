using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Exam;

namespace ThoughtzLand.ImplementRepo.EFCoreRepo
{
	internal class PropertyUpdater<TInternalEntity, TExternalEntity>
		where TInternalEntity : class
		where TExternalEntity : class
	{
		private readonly AppData db;
		IMapper mapper;

		public PropertyUpdater(AppData db)
		{
			this.db = db;

			var mapCfg = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<TExternalEntity, TInternalEntity>().ReverseMap();
			});

			mapper = mapCfg.CreateMapper();
		}

		public int UpdateProperties(TExternalEntity ent, params Expression<Func<TExternalEntity, object>>[] propertySelectors)
		{
			ArgumentNullException.ThrowIfNull(ent);

			// find id property manually
			// Get all properties of the entity and find the ID property in a case-insensitive manner
			var idProperty = typeof(TExternalEntity).GetProperties()
							.FirstOrDefault(p => string.Equals(p.Name, "id", StringComparison.OrdinalIgnoreCase));

			if (idProperty == null)
			{
				throw new InvalidOperationException("ID property not found on the entity.");
			}

			int entityId = 0;

			if (idProperty?.GetValue(ent) is int idValue)
				entityId = idValue;
			else
				throw new InvalidCastException($"{typeof(TExternalEntity).Name}.id is not of int type");

			//Здесь проблема, так как FlashCardDb содержит
			//public ICollection<FlashCardAnswerDb> answers { get; set; } = new List<FlashCardAnswerDb>();
			//а FlashCard нет
			var internalEntity = mapper.Map<TInternalEntity>(ent);

			var dbSet = db.Set<TInternalEntity>();

			var dbEnt = dbSet.Local.FirstOrDefault(e => EF.Property<int>(e, "id") == entityId);

			if(dbEnt == null)
				dbEnt = dbSet.Where(e => EF.Property<int>(e, "id") == entityId).FirstOrDefault();

			if (dbEnt == null)
				throw new InvalidOperationException($"No object with id = {entityId} of {typeof(TInternalEntity).Name}");

			// Берем имена полей из внешнего типа, полагаем, что поля идентичные
			foreach (var propertySelector in propertySelectors)
			{
				if (!(propertySelector.Body is MemberExpression memberExpression))
				{
					// Handle UnaryExpression (conversion)
					if (propertySelector.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
					{
						memberExpression = operand;
					}
					else
					{
						throw new ArgumentException("Invalid property selector. Please use a lambda expression that selects a property.");
					}
				}

				var propertyName = memberExpression.Member.Name;
				var func = propertySelector.Compile();
				var propertyValue = func(ent);

				var entry = db.Entry(dbEnt);
				entry.Property(propertyName).CurrentValue = propertyValue;
				entry.Property(propertyName).IsModified = true;
			}

			return db.SaveChanges();
		}
	}

	internal class PropertyUpdater<TEntity>
	where TEntity : class
	{
		private readonly AppData db;

		public PropertyUpdater(AppData db)
		{
			this.db = db;
		}

		public int UpdateProperty(int entityId, string propertyName, object propertyValue)
		{
			var dbSet = db.Set<TEntity>();

			var dbEnt = dbSet/*.Local*/.FirstOrDefault(e => EF.Property<int>(e, "id") == entityId);

			if (dbEnt == null)
				dbEnt = dbSet.Where(e => EF.Property<int>(e, "id") == entityId).FirstOrDefault();

			if (dbEnt == null)
				throw new InvalidOperationException($"No object with id = {entityId} of {typeof(TEntity).Name}");

			var entry = db.Entry(dbEnt);
			entry.Property(propertyName).CurrentValue = propertyValue;
			entry.Property(propertyName).IsModified = true;

			return db.SaveChanges();
		}

	}
}
