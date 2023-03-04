using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.repo
{
    public class LexemRepo
    {
        private readonly AppData db;

        public LexemRepo(AppData db)
        {
            this.db = db;
        }

        public IEnumerable<LexemMeaning> GetMeanings(Lexem lexem)
        {
            return new[] { new LexemMeaning { text = "идти" } };
        }

        public void CreateLexem(Lexem l)
        {

        }

        public void CreateMeaning()
        {

        }
    }
}
