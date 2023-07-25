using Models.Thought;

namespace Services.repo
{
    public interface IThExpressionRepo
    {
        void AddScore(int expId, int scores);
        ThExpression Create(int thoughtId, ThExpression thexp);
        IEnumerable<ThExpression> GetAllOf(int thoughtId);
        ThExpression GetOne(int id);
        OperationResult UpdateString(int id, string propname, string newtext);
        RepoOperationResult Delete(int id);
    }
}