using ThoughtzLand.Core.Models.Common;

namespace ThoughtzLand.Core.Models.Thoughts
{
    public class Thought: IDbEntity
    {
        public Thought()
        {
            expressions = new List<ThExpression>();
        }

        public int id { get; set; }
        public int nodeId { get; set; }
        public string? text { get; set; }
        public string? description { get; set; }
        public DateTime? createdDate { get; set; }
        public ICollection<ThExpression> expressions { get; set; }
    }
}
