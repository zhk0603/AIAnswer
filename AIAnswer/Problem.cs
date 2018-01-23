using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAnswer
{
    public class Problem
    {
        public Problem()
        {
            Answer = new List<AIAnswer.Answer>();
        }

        public Problem(string title) : this()
        {
            Title = title;
        }

        public Problem(string title, IEnumerable<Answer> answer) : this(title)
        {
            Answer = answer;
        }
        public string Title { get; set; }

        public IEnumerable<Answer> Answer { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"问题：{Title}");
            foreach (var answer in Answer)
            {
                sb.AppendLine($"  {answer.Order}. {answer.Title}");
            }
            return sb.ToString();
        }
    }

    public class Answer
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public double Pmi { get; set; }
    }
}
