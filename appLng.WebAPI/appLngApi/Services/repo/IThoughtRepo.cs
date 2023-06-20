using Models.Thought;

namespace Services.repo
{
    public interface IThoughtRepo
    {
        Thought Create(int nodeId, Thought th);
        void Delete(int thId);
        IEnumerable<Thought> Get(int nodeId);
        Thought? GetThought(int thId);
        Thought Update(int thId, Thought th);
    }
}