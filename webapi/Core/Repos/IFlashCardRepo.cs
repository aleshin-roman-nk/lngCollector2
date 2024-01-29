using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Models.Exam.dto;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
    public interface IFlashCardRepo: 
		IRepoCreator<CreateFlashCardDto, FlashCard>,
		IRepoGetterOneById<FlashCard>,
		IRepoPropertiesUpdater<FlashCard>,
		IRepoPropertyUpdater,
		IRepoEntityUpdater<UpdateFlashCardDto, FlashCard>
	{
		IEnumerable<FlashCardTitle> GetCards(int nodeId, DateTime dt);
		//IEnumerable<FlashCard> GetPlayingCards(int nodeId, DateTime dt);
		IEnumerable<FlashCard> GetPlayingCards(int nodeId);
	}
}
