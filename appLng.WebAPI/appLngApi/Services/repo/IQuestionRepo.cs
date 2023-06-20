using Models.Thought;

namespace Services.repo
{
    public interface IQuestionRepo
    {
        IEnumerable<Question> GetQuestions(int nodeId);
    }
}