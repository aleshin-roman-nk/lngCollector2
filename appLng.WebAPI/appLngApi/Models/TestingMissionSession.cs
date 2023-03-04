using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TestingMissionSession
    {
        public int id { get; set; }
        public int testMissionId { get; set; }
        public decimal Total { get; set; }
        public int TotalPoints { get; set; }
        public string? lexemsIdList 
        {
            get
            {
                return ser();
            }
            set
            {
                Lexems = deser(value);
            }
        }
        [NotMapped]
        public List<int>? Lexems { get; set; }

        private string ser()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in Lexems)
                sb.Append($" {item}");

            return sb.ToString();
        }
        private List<int>? deser(string str)
        {
            var res = new List<int>();

            var idlst = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in idlst)
                res.Add(int.Parse(item));

            return res;
        }
    }
}
