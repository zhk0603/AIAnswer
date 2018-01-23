using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIAnswer
{
    public class ProblemAnalyzer
    {
        public static void Analyzer(Problem problem)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Console.WriteLine("开始分析……");

            var taskList = new List<Task>();
            foreach (var item in problem.Answer)
            {
                var analyzerItem = new AnalyzerItem {ProblemTitle = problem.Title, Answer = item};
                var task = Task.Factory.StartNew(RunAnalyzer, analyzerItem);
                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());

            sw.Stop();

            Console.WriteLine("分析结束，耗时：" + sw.ElapsedMilliseconds);
        }

        public static void RunAnalyzer(object obj)
        {
            var item = obj as AnalyzerItem;

            var keyWord = $"{item.ProblemTitle} {item.Answer.Title}";

            Console.WriteLine($"分析：{item.Answer.Title}");
            var searchUrl = "https://www.baidu.com/s?ie=utf-8&w=" +
                            System.Web.HttpUtility.UrlEncode(keyWord, Encoding.UTF8);

            var problenAndAnswerCnt = GetSearchCnt(searchUrl);
            item.Answer.Pmi = problenAndAnswerCnt;
            Console.WriteLine($"分析：{item.Answer.Title} 结束，搜索结果：{problenAndAnswerCnt}");
        }

        public static long GetSearchCnt(string url)
        {
            var searchRes = HttpHelper.GetAsync(url).GetAwaiter().GetResult();
            var match = Regex.Match(searchRes, "百度为您找到相关结果约([^']*)个");
            if (match.Success)
            {
                return Convert.ToInt64(match.Groups[1].Value.Replace(",", ""));
            }
            return 0;
        }
    }

    public class AnalyzerItem
    {
        public string ProblemTitle { get; set; }
        public Answer Answer { get; set; }
    }
}
