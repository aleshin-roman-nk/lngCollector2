using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
    public interface IThoughtRepo: IRepository<Thought>, IPropertyUpdaterByName
    {
        bool ThoughtHasWork(int thId);
    }
}