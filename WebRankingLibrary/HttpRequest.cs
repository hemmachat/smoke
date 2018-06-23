using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WebRankingLibrary
{
    public class HttpRequest : IRequest
    {
        private readonly HttpClient _client;

        public HttpRequest(string url)
        {
            _client = new HttpClient();
            _client.GetStringAsync(url);
            HtmlDocument doc = new HtmlDocument();
        }

        public List<WebItem> FetchWebItems()
        {
            return 
        }
    }
}
