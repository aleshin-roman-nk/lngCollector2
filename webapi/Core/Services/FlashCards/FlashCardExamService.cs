using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services.FlashCards
{
	public class FlashCardExamService
	{
		private readonly IFlashCardRepo fcrepo;

		CardLevelScheme cardLevelScheme;

		public FlashCardExamService(
			IFlashCardRepo fcrepo)
		{
			this.fcrepo = fcrepo;
			cardLevelScheme = initCardLevelScheme();
		}

		CardLevelScheme initCardLevelScheme()
		{
			return new CardLevelScheme(
				new CardLevel[]
				{
					new CardLevel(level: 1, hitsFrom: 0, hitsTo: 10, nextExamInMinuts: 10),
					new CardLevel(level: 2, hitsFrom: 11, hitsTo: 20, nextExamInMinuts: 15),
					new CardLevel(level: 3, hitsFrom: 21, hitsTo: 24, nextExamInMinuts: 20),
					new CardLevel(level: 4, hitsFrom: 25, hitsTo: 9999, nextExamInMinuts: 20)
				},
				4,
				25);
		}

		// На странице ноды где то можно типа истории ответов хранить; или очки.
		// очки получаются путем выборки всех очков всех Expression of Thoughts данной ноды.

		public CheckResult Check(CardSolution sol)
		{
			var card = fcrepo.Get(sol.cardId);

			if (sol.helpIsUsed)
			{
				//var newDate = DateTime.Now.AddMinutes(5);

				card.hitsInRow = card.hitsInRow >= cardLevelScheme.cardMaxHit ? cardLevelScheme.cardMaxHit : 0;
				var calcLevel = cardLevelScheme.calc(card.hitsInRow);

				card.level = calcLevel.level;
				card.nextExamDate = calcLevel.NextExamDate;

				fcrepo.UpdateProperty(card.id, "nextExamDate", card.nextExamDate);
				fcrepo.UpdateProperty(card.id, "hitsInRow", card.hitsInRow);
				fcrepo.UpdateProperty(card.id, "level", card.level);

				return new CheckResult
				{
					cardId = card.id,
					isCorrect = false,
					nextExamDate = card.nextExamDate,
					totalHits = card.totalHits,
					hitsInRow = card.hitsInRow,
					level = card.level
				};
			}
			
			var res = new CheckResult();

			var isCorrect = card.answers.Any(answer => IsSimilar(sol.solution.Trim(), answer.text.Trim(), 93));

			if (isCorrect)
			{
				card.hitsInRow = card.hitsInRow + 1;
				card.totalHits = card.totalHits + 1;
			}
			else
			{
				card.hitsInRow = card.hitsInRow >= cardLevelScheme.cardMaxHit ? cardLevelScheme.cardMaxHit : 0;
			}

			{
				var calcLevel = cardLevelScheme.calc(card.hitsInRow);

				card.level = calcLevel.level;
				card.nextExamDate = calcLevel.NextExamDate;

				fcrepo.UpdateProperty(card.id, "hitsInRow", card.hitsInRow);
				fcrepo.UpdateProperty(card.id, "totalHits", card.totalHits);
				fcrepo.UpdateProperty(card.id, "level", card.level);
				fcrepo.UpdateProperty(card.id, "nextExamDate", card.nextExamDate);

				// Трудности с маппингом. Пока по одной обновляем
				//fcrepo.UpdateProperties(card, x => x.points, x=> x.NextExamDate);

				res.cardId = card.id;
				res.isCorrect = isCorrect;
				res.nextExamDate = card.nextExamDate;
				res.totalHits = card.totalHits;
				res.hitsInRow = card.hitsInRow;
				res.level = card.level;

				return res;
			}

		}

		private bool IsSimilar(string str1, string str2, int thresholdPercentage)
		{
			str1 = str1.Trim().ToLower();
			str2 = str2.Trim().ToLower();

			int levenshteinDistance = GetLevenshteinDistance(str1, str2);
			int maxLength = Math.Max(str1.Length, str2.Length);
			int similarityPercentage = (int)((1 - (double)levenshteinDistance / maxLength) * 100);

			return similarityPercentage >= thresholdPercentage;
		}

		private int GetLevenshteinDistance(string str1, string str2)
		{
			int n = str1.Length;
			int m = str2.Length;
			int[,] dp = new int[n + 1, m + 1];

			if (n == 0) return m;
			if (m == 0) return n;

			for (int i = 0; i <= n; dp[i, 0] = i++) { }
			for (int j = 0; j <= m; dp[0, j] = j++) { }

			for (int i = 1; i <= n; i++)
			{
				for (int j = 1; j <= m; j++)
				{
					int cost = (str2[j - 1] == str1[i - 1]) ? 0 : 1;

					dp[i, j] = Math.Min(
						Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
						dp[i - 1, j - 1] + cost);
				}
			}

			return dp[n, m];
		}
	}
}