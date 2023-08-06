using ThoughtzLand.Core.Models.Thoughts;
using ThoughtzLand.Core.Repos.Common;

namespace ThoughtzLand.Core.Repos
{
    public interface IThoughtRepo: IRepository<Thought>, IPropertyUpdater
    {
        bool ThoughtHasWork(int thId);
    }
}