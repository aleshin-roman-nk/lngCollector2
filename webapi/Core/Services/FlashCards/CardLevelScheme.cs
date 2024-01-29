using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Services.FlashCards
{
	internal class CardLevelScheme
	{
		public int cardMaxLevel { get; }
		public int cardMaxHit { get; }

		public IEnumerable<CardLevel> cardLevels { get; }
		public CardLevelScheme(IEnumerable<CardLevel> levels, int cardMaxLevel, int cardMaxHit)
		{
			cardLevels = levels;
			this.cardMaxLevel = cardMaxLevel;
			this.cardMaxHit = cardMaxHit;
		}

		public LevelCalculation calc(int hitsInRow)
		{
			var cl = cardLevels.FirstOrDefault(c => hitsInRow >= c.hitsFrom && hitsInRow <= c.hitsTo);

			if (cl == null) throw new InvalidOperationException("Wrong with calculating level");

			return new LevelCalculation {
				NextExamDate = DateTime.Now.AddSeconds(cl.nextExamInMinuts) ,
				level = cl.level
			};
		}
	}

	internal class CardLevel
	{
		public CardLevel(int level, int hitsFrom, int hitsTo, int nextExamInMinuts)
		{
			this.level = level;
			this.hitsFrom = hitsFrom;
			this.hitsTo = hitsTo;
			this.nextExamInMinuts = nextExamInMinuts;
		}

		public int level { get; set; }
		public int hitsFrom { get; set; }
		public int hitsTo { get; set; }
		public int nextExamInMinuts { get; set; }
	}

	internal class LevelCalculation
	{
		public DateTime NextExamDate { get; set; }
		public int level { get; set; }
	}
}
