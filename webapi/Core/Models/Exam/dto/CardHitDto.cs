﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtzLand.Core.Models.Exam.dto
{
	public class CardHitDto
	{
		public int id { get; set; }
		public IEnumerable<string>? answers { get; set; }
		public int hitsInRow { get; set; }
		public int requiredHits { get; set; }
		public int totalHits { get; set; }
		public int level { get; set; }
		public DateTime nextExamDate { get; set; }
		public bool isCompleted { get; set; }
	}
}
