using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Services.FlashCards
{
	public class CardParametersSchemeProvider
	{
		public CardParametersScheme CardParametersScheme { get; }

		public CardParametersSchemeProvider()
		{
			CardParametersScheme = new CardParametersScheme(
				levels: new CardLevel[]
				{
					new CardLevel(hitsFrom: 0, hitsTo: 10, nextExamInMinuts: 5),
					new CardLevel(hitsFrom: 11, hitsTo: 15, nextExamInMinuts: 30),
					new CardLevel(hitsFrom: 16, hitsTo: 19, nextExamInMinuts: 60),
					new CardLevel(hitsFrom: 20, hitsTo: 20, nextExamInMinuts: 0)
				},
				completedQuestPrice: 10);
		}
	}
}
