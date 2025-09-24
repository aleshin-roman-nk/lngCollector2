using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Repos;

namespace ThoughtzLand.Core.Services.FlashCards
{
	public class FlashCardExamService
	{
		private readonly IFlashCardRepo fcrepo;

		private readonly CardParametersScheme CardParametersScheme;

		public FlashCardExamService(
			IFlashCardRepo fcrepo,
			CardParametersSchemeProvider cardLevelSchemeProvider)
		{
			this.fcrepo = fcrepo;
			CardParametersScheme = cardLevelSchemeProvider.CardParametersScheme;
		}

		public CheckResult Check(CardSolution sol)
		{
			var cardHit = fcrepo.GetCardHit(sol.cardId);

			var cardHitUpdateDto = new UpdateCardHitDto
			{
				hitsInRow = cardHit.hitsInRow,
				id = cardHit.id,
				isCompleted = cardHit.isCompleted,
				totalHits = cardHit.totalHits,
				level = cardHit.level,
				nextExamDate = cardHit.nextExamDate,
			};
			
			var isCorrect = cardHit.answers.Any(answer => IsSimilar(sol.solution.Trim(), answer.Trim(), 93));

			var justCompleted = false;

			if (isCorrect && !sol.helpIsUsed)
			{
				cardHitUpdateDto.hitsInRow = cardHit.hitsInRow + 1;
				cardHitUpdateDto.totalHits = cardHit.totalHits + 1;

				if(cardHitUpdateDto.hitsInRow >= cardHit.requiredHits)
				{
					justCompleted = cardHit.isCompleted ? false : true;
					cardHitUpdateDto.isCompleted = true;
					cardHitUpdateDto.hitsInRow = cardHit.requiredHits;
				}
			}
			else
			{
				cardHitUpdateDto.hitsInRow = cardHit.hitsInRow >= CardParametersScheme.cardAimHitInRow ? CardParametersScheme.cardAimHitInRow : 0;
				isCorrect = false;
			}

			{
				var calcLevel = CardParametersScheme.calc(cardHitUpdateDto.hitsInRow);

				cardHitUpdateDto.level = calcLevel.level;
				cardHitUpdateDto.nextExamDate = calcLevel.NextExamDate;

				fcrepo.UpdateCardHit(cardHitUpdateDto);

				CheckResult res = new CheckResult
				{
					cardId = cardHitUpdateDto.id,
					hitsInRow = cardHitUpdateDto.hitsInRow,
					isCorrect = isCorrect,
					isJustCompleted = justCompleted,
					level = cardHitUpdateDto.level,
					nextExamDate = cardHitUpdateDto.nextExamDate,
					totalHits = cardHitUpdateDto.totalHits
				};

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