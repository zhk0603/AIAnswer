using System;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIAnswer
{
    public class HttpHelper : IDisposable
    {
        private static readonly HttpClient Client;

        static HttpHelper()
        {
            Client = new HttpClient {MaxResponseContentBufferSize = int.MaxValue};
            Client.DefaultRequestHeaders.Add("user-agent",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
        }

        public static async Task<string> GetAsync(string url)
        {
            var response = await Client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}