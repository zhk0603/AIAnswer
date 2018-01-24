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
        private static long _problemCnt;
        private static long[] _answerCnt;
        private static long[] _problemAndAnswerCnt;


        public static void Analyzer(Problem problem)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Console.WriteLine("开始分析……");

            var taskList = new List<Task>();

            var analyzerProblemTask =
                Task.Factory.StartNew(RunAnalyzer,
                    new AnalyzerItem {ProblemTitle = problem.Title, Type = AnalyzerType.分析问题});
            taskList.Add(analyzerProblemTask);

            foreach (var item in problem.Answer)
            {
                var task1 = Task.Factory.StartNew(RunAnalyzer,
                    new AnalyzerItem {ProblemTitle = problem.Title, Answer = item, Type = AnalyzerType.分析答案});

                var task2 = Task.Factory.StartNew(RunAnalyzer,
                    new AnalyzerItem {ProblemTitle = problem.Title, Answer = item, Type = AnalyzerType.分析问题与答案});

                taskList.Add(task1);
                taskList.Add(task2);
            }

            Task.WaitAll(taskList.ToArray());

            sw.Stop();

            Console.WriteLine("分析结束，耗时：" + sw.ElapsedMilliseconds);
        }

        public static void RunAnalyzer(object obj)
        {
            var item = obj as AnalyzerItem;

            var keyWord = string.Empty;
            switch (item.Type)
            {
                case AnalyzerType.分析问题:
                    keyWord = item.ProblemTitle;
                    break;
                case AnalyzerType.分析答案:
                    keyWord = item.Answer.Title;
                    break;
                case AnalyzerType.分析问题与答案:
                    keyWord = $"{item.ProblemTitle} {item.Answer.Title}";
                    break;
            }

            Console.WriteLine($"分析：{keyWord}");
            var searchUrl = "https://www.baidu.com/s?ie=utf-8&w=" +
                            System.Web.HttpUtility.UrlEncode(keyWord, Encoding.UTF8);

            var searchCnt = GetSearchCnt(searchUrl);
            switch (item.Type)
            {
                case AnalyzerType.分析问题:
                    _problemCnt = searchCnt;
                    break;
                case AnalyzerType.分析答案:

                    break;
                case AnalyzerType.分析问题与答案:

                    break;
            }
        }

        public static long GetSearchCnt(string url)
        {
            var searchRes = HttpHelper.GetAsync(url).Result;
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
        public AnalyzerType Type { get; set; }
        public string ProblemTitle { get; set; }
        public Answer Answer { get; set; }
    }

    public enum AnalyzerType
    {
        分析问题,
        分析答案,
        分析问题与答案
    }
}
