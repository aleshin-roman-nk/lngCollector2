﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Exam;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
	public interface IFlashCardRepo: IRepository<FlashCard>
	{
		IEnumerable<FlashCard> GetFlashCards(int nodeId, DateTime date);
	}
}
