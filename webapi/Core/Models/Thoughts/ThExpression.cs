using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Thoughts
{
    public class ThExpression: IDbEntity
    {
        public int id { get; set; }
        public int thoughtId { get; set; }
        public string? text { get; set; }
        public DateTime? createdDate { get; set; }
        public int lngId { get; set; }
        //public int scores { get; set; }// сколько раз правильно ответил
        public WhoMade madeBy {  get; set; }// мой опыт или готовое выражение
        public ThExpressionTypeEnum? type { get; set; }
    }
}
