using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Location;
using ThoughtzLand.Core.Models.Location.dto;
using ThoughtzLand.Core.Repos;
using ThoughtzLand.ImplementRepo.SQLitePepo.Entities;

namespace ThoughtzLand.ImplementRepo.SQLitePepo
{
	public class NodeRepoSQLite : INodeRepo
	{
		private readonly AppData db;
		IMapper mapper;

		public NodeRepoSQLite(AppData db)
		{
			this.db = db;

			var mapCfg = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Node, Node>();
			});

			mapper = mapCfg.CreateMapper();
		}

		public NodeDetailDto GetDetail(int nodeId)
		{
			var n = db.Nodes.FirstOrDefault(x => x.id == nodeId);
			if (n == null) { throw new InvalidOperationException($"no such node in db (id={nodeId})"); }

			var ths = db.FlashCards.Where(x => x.nodeId == nodeId)
				.Include(card => card.language)
				.Include(card => card.answers)
				.Select(card => new FlashCardTitle
				{
					id = card.id,
					lngTag = card.language.tag,
					nodeId = nodeId,
					hitsInRow = card.hitsInRow,
					requiredHits = card.requiredHits,
					totalHits = card.totalHits,
					question = card.question,
					level = card.level,
					answersCount = card.answers.Count
				})
				.ToList();

			var rtxts = db.ResearchTexts.Where(txt => txt.nodeId == nodeId).ToArray();

			return new NodeDetailDto(n, ths, rtxts);
		}

		public Node Create(Node node)
		{
			node.id = 0;

			db.Nodes.Add(node);
			var success = db.SaveChanges() > 0;
			if (!success)
				throw new InvalidOperationException("something went wrong when creating a node");
			return node;
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
			db.Nodes.Remove(new Node { id = nodeId });
			var success = db.SaveChanges() > 0;
			if (!success)
				throw new InvalidOperationException("something went wrong when deleting a node");
		}

		public IEnumerable<Node> GetByTerrainId(int terrainId)
		{
			return db.Nodes.Where(x => x.terrainId == terrainId).ToArray();
		}
	}
}
