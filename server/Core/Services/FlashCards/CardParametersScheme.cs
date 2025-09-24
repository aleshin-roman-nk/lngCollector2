using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Services.FlashCards
{
	public class CardParametersScheme
	{
		public int cardMaxLevel { get; }
		public int cardAimHitInRow { get; }
		public int CompletedQuestPrice { get; }

		public IEnumerable<CardLevel>? cardLevels { get; }
		public CardParametersScheme(IEnumerable<CardLevel> levels, int completedQuestPrice)
		{
			cardLevels = sortAndProcessIntersections(levels);
			if (cardLevels == null) throw new ArgumentException("Level intervals have intersection(s)");

			cardLevels = calcLevels(cardLevels);

			var lastLevel = cardLevels.LastOrDefault();

			cardMaxLevel = lastLevel.level;
			cardAimHitInRow = lastLevel.hitsTo;

			CompletedQuestPrice = completedQuestPrice;
		}

		IEnumerable<CardLevel> calcLevels(IEnumerable<CardLevel> cl)
		{
			int l = 1;

			foreach (CardLevel level in cl)
			{
				level.level = l;
				l++;
			}

			return cl;
		}

		private IEnumerable<CardLevel>? sortAndProcessIntersections(IEnumerable<CardLevel> cl)
		{
			var arr = cl.ToList();

			arr.Sort((x, y) => x.hitsFrom.CompareTo(y.hitsFrom));

			for (int i = 0; i < arr.Count - 1; i++)
			{
				if (arr[i].hitsTo >= arr[i + 1].hitsFrom)
				{
					// Intervals intersect
					return null;
				}
			}

			// No intersections found
			return arr;
		}

		public LevelCalculation calc(int hitsInRow)
		{
			var cl = cardLevels.FirstOrDefault(c => hitsInRow >= c.hitsFrom && hitsInRow <= c.hitsTo);

			if (cl == null) throw new InvalidOperationException("Wrong with calculating level");

			return new LevelCalculation {
				NextExamDate = DateTime.UtcNow.AddMinutes(cl.nextExamInMinuts),
				level = cl.level
			};
		}
	}

	public class CardLevel
	{
		public CardLevel(int hitsFrom, int hitsTo, int nextExamInMinuts)
		{
			this.hitsFrom = hitsFrom;
			this.hitsTo = hitsTo;
			this.nextExamInMinuts = nextExamInMinuts;
		}

		public int level { get; set; }
		public int hitsFrom { get; set; }
		public int hitsTo { get; set; }
		public int nextExamInMinuts { get; set; }
	}

	public class LevelCalculation
	{
		public DateTime NextExamDate { get; set; }
		public int level { get; set; }
	}
}
