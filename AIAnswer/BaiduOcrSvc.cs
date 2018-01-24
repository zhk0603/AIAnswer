using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AIAnswer
{
    public class BaiduOcrSvc : OcrServiceBase
    {
        private Baidu.Aip.Ocr.Ocr _ocrSvc;

        public override void Init()
        {
            var appKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            var secretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"];
            _ocrSvc = new Baidu.Aip.Ocr.Ocr(appKey, secretKey);
        }

        protected override Problem ProcessCore(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            var json = _ocrSvc.GeneralBasic(bytes);

            JToken[] wordArr = json["words_result"].ToArray();

            if (wordArr.Length < 5)
            {
                throw new Exception("题目解析错误");
            }

            var title = string.Empty;
            var titleIndex = wordArr.Length - 5;
            for (var i = 0; i <= titleIndex; i++)
            {
                title += wordArr[i]["words"].ToString();
            }

            var answereList = new List<Answer>();
            for (var i = titleIndex + 1; i < wordArr.Length; i++)
            {
                answereList.Add(new Answer
                {
                    Order = answereList.Count + 1,
                    Title = wordArr[i]["words"].ToString()
                });
            }

            return new Problem(title, answereList);
        }
    }
}
