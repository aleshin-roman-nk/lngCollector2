using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.ImplementRepo.EFCoreRepo
{
	public class NodeRepoEFCore : INodeRepo
	{
		private readonly AppData db;

		public NodeRepoEFCore(AppData db)
		{
			this.db = db;
		}

		public NodeDetailDto GetNodeDetail(int nodeId)
		{
			//var n = db.Nodes.FirstOrDefault(x => x.id == nodeId);
			//if (n == null) { throw new InvalidOperationException($"no such node in db (id={nodeId})"); }

			//var flashCards = db.FlashCards.Where(x => x.nodeId == nodeId)
			//	.Include(card => card.language)
			//	.Include(card => card.answers)
			//	.Select(card => new FlashCardTitle
			//	{
			//		id = card.id,
			//		lngTag = card.language.tag,
			//		nodeId = nodeId,
			//		hitsInRow = card.hitsInRow,
			//		requiredHits = card.requiredHits,
			//		totalHits = card.totalHits,
			//		question = card.question,
			//		level = card.level,
			//		answersCount = card.answers.Count
			//	})
			//	.ToList();

			var nodeDetailDto = db.Nodes
				.Where(node => node.id == nodeId)
				.Select(node => new NodeDetailDto
				{
					id = node.id,
					terrainId = node.terrainId,
					name = node.name,
					description = node.description,
					//questsMinimumTotalPrice = node.questsMinimumTotalPrice,
					FlashCardsTitles = node.FlashCards.Select(flashCard => new FlashCardTitle
					{
						id = flashCard.id,
						nodeId = flashCard.nodeId,
						question = flashCard.question,
						lngTag = flashCard.language.tag,
						hitsInRow = flashCard.hitsInRow,
						requiredHits = flashCard.requiredHits,
						totalHits = flashCard.totalHits,
						level = flashCard.level,
						answersCount = flashCard.answers.Count,
						questPrice = flashCard.questPrice,
						isCompleted = flashCard.isCompleted
					}).ToArray(),
					ResearchTexts = node.ResearchTexts.ToArray()
				})
				.FirstOrDefault();

			if (nodeDetailDto != null)
				return nodeDetailDto;
			else
				throw new InvalidOperationException($"no such node with id = {nodeId}");
		}

		public void Update(UpdateNodeNameAndDescriptionDto node)
		{
			try
			{
				var nodeDb = db.Nodes.FirstOrDefault(x => x.id == node.id);
				if (nodeDb != null)
				{
					nodeDb.name = node.name;
					nodeDb.description = node.description;
					//nodeDb.y = node.y;
					//nodeDb.x = node.x;

					//mapper.Map(node, nodeDb);

					var success = db.SaveChanges() > 0;
					if (!success)
						throw new InvalidOperationException("something went wrong when updating a node");
					//return nodeDb;
				}
				else
					throw new InvalidOperationException($"Not such Node in db with id = {node.id}");
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(ex.Message);
			}
		}

		public void Remove(int nodeId)
		{
			db.Nodes.Remove(new NodeDb { id = nodeId });
			var success = db.SaveChanges() > 0;
			if (!success)
				throw new InvalidOperationException("something went wrong when deleting a node");
		}

		public NodeTitleDto Create(CreateNodeDto entity)
		{
			var newNode = new NodeDb 
			{ name = entity.name, 
				description = entity.description, 
				terrainId = entity.terrainId,
				//questsMinimumTotalPrice = entity.questsPointsSumMin,
			};

			db.Nodes.Add(newNode);
			var success = db.SaveChanges() > 0;
			if (!success)
				throw new InvalidOperationException("something went wrong when creating a node");
			return new NodeTitleDto
			{
				name = newNode.name,
				description = newNode.description,
				id = newNode.id,
				terrainId = newNode.terrainId,
				//questsMinimumTotalPrice = newNode.questsMinimumTotalPrice
			};
		}
	}
}
