using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exam
{
    /* 17-04-2023 01:12
     * Лучше собрать тестирование в один комплекс.
     * Назвать не тест, так как это вызовет путаницу с тестированием кода
     * 
     */

    public class ExamSession
    {
        public int id { get; set; }
        public int nodeId { get; set; }
        public decimal Total { get; set; }
        public int TotalPoints { get; set; }
        public string? thoughtIdList
        {
            get
            {
                return ser();
            }
            set
            {
                ThoughtIds = deser(value);
            }
        }
        [NotMapped]
        public List<int>? ThoughtIds { get; set; }

        private string ser()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in ThoughtIds)
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
