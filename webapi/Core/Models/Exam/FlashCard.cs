using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtzLand.Core.Models.Common;
using ThoughtzLand.Core.Models.Thoughts;

namespace ThoughtzLand.Core.Models.Exam
{

	// в бд храним только теги. нам не интересны теги - это иехническая особенность
	// бд. 
	// здесь интересна только сформированная флеш карта и уже готовый объект,
	//  который используется в логике сервиса.
	// принимаем сверху и отдаем наверх только интерфейсные дтошки, конечно содержат
	// указатели на связанные данные

	/*
	 * Интерфейс репозитория должен только вернуть коллекцию thexpression из 
	 * указанной даты для указанной ноды.
	 * 
	 * Вернуть теги... Или выражения, а в сервисе уже формировать.
	 * Нужна ... сервису нужна информация для осуществления логики работы ящика.
	 * 
	 * 09-09-2023 13:39
	 * - мы запрашиваем карту
	 * - на клиенте сами выбираем на каком языке ответим
	 * - но тогда клиенту отдавать ответ со списком языков ответа, доступном для
	 * карты в пределах данной мысли
	 * 
	 * 10-09-2023
	 * 
	 * 
	 */
	public class FlashCard: IDbEntity
	{
		public int id { get; set; }
		public ThExpression? expressionUnderTest { get; set; }
		public IEnumerable<ThExpression> questions { get; set; }
		public int boxCellNo { get; set; }
		public bool passed { get; set; }
		public DateTime NextExamDate { get; set; }
		public int rightSolutionScores { get; set; }
	}
}
