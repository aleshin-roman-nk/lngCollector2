using Models.Exam.dto;
using Models.Thought;

namespace Services
{
    public interface IExamService
    {
        CheckResult Check(QuestSolution sol);
        IEnumerable<Question> GetQuestions(int nodeId);
    }
}