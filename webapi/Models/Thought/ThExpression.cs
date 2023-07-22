using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Thought
{
    public class ThExpression
    {
        public int id { get; set; }
        public int thoughtId { get; set; }
        public string? text { get; set; }
        public DateTime? createdDate { get; set; }
        public int lngId { get; set; }
        public int scores { get; set; }// сколько раз правильно ответил
        public WhoMade madeBy {  get; set; }// мой опыт или готовое выражение
        public ThExpressionTypeEnum? type { get; set; }
    }
}
