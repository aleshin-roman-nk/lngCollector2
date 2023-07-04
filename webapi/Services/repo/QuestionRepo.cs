using Microsoft.EntityFrameworkCore;
using Models.Thought;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly IDbFactory factory;

        public QuestionRepo(IDbFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<Question> GetQuestions(int nodeId)
        {
            using (var db = factory.Create())
            {
                return db.Thoughts
                    .Where(t => t.nodeId == nodeId)
                    .Select(t => new Question
                    {
                        thoughtId = t.id,
                        expressions = db.ThExpressions
                            .Where(e => e.thoughtId == t.id)
                            .ToList()
                    })
                    .ToArray();
            }
        }
    }
}
