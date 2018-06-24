using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using WebRanking.Interfaces;
using WebRanking.Models;

namespace WebRanking
{
    public class ManualSearchService : ISearchService
    {
        private static int LIMIT = 100;
        private static string GOOGLE_URL = $"https://www.google.com.au/search?num={LIMIT}&q=";

        public async Task<List<WebItem>> FetchWebItemsAsync(string keyword, string searchUrl)
        {
            var client = new HttpClient();
            var encodedKeyword = WebUtility.UrlEncode(keyword);
            var clientBody = await client.GetStringAsync(GOOGLE_URL + encodedKeyword);

            // outer most div
            var contentAreaStart = clientBody.IndexOf("<div id=\"center_col\">");
            var contentAreaStop = clientBody.LastIndexOf("<div id=\"foot\">");
            var contentArea = clientBody.Substring(contentAreaStart, contentAreaStop - contentAreaStart);

            // search result div
            var searchResultStart = contentArea.IndexOf("<div id=\"res\">");
            var searchResultStop = contentArea.LastIndexOf("<div class=\"C4eCVc\"");
            var searchResult = contentArea.Substring(searchResultStart, searchResultStop - searchResultStart);
            searchResult = searchResult.Replace("<div id=\"res\"><div id=\"topstuff\"></div><div id=\"search\"><div id=\"ires\"><ol>", "");

            var webItems = new List<WebItem>();
            var rank = 1;
            var innerDivToken = "<div class=\"g\">";
            var tail = "</ol></div></div></div>";

            while (true)
            {
                var innerDivStart = searchResult.IndexOf(innerDivToken);
                var innerDivStop = searchResult.IndexOf(innerDivToken, innerDivStart + 15);
                var newStart = innerDivStop - innerDivStart;

                if (innerDivStop > innerDivStart)
                {
                    var item = searchResult.Substring(innerDivStart, newStart);
                    webItems.Add(ExtractWebItem(searchUrl, rank, item));
                    searchResult = searchResult.Substring(newStart);
                }
                // last item
                else
                {
                    var item = searchResult.Replace(tail, "");
                    webItems.Add(ExtractWebItem(searchUrl, rank, item));
                    break;
                }

                rank++;
            }

            return webItems;
        }

        private WebItem ExtractWebItem(string stringUrl, int rank, string item)
        {
            // title
            var webItem = new WebItem();
            var anchor = "<a";
            var anchorClosing = "</a>";
            var anchorStart = item.IndexOf(anchor);
            var anchorStop = item.IndexOf(anchorClosing);
            var anchorValue = item.Substring(anchorStart, anchorStop - anchorStart + anchorClosing.Length);
            var close = ">";
            var titleStart = anchorValue.IndexOf(close);
            var titleStop = anchorValue.LastIndexOf("<");
            webItem.Title = anchorValue.Substring(titleStart + close.Length, titleStop - titleStart - close.Length);

            // link
            var cite = "<cite>";
            var linkStart = item.IndexOf(cite);
            var linkStop = item.IndexOf("</cite>");
            webItem.Link = item.Substring(linkStart + cite.Length, linkStop - linkStart - cite.Length);

            // description
            var span = "<span class=\"st\">";
            var descriptionStart = item.IndexOf(span);
            var descriptionStop = item.LastIndexOf("</span>");
            webItem.Description = item.Substring(descriptionStart + span.Length, descriptionStop - descriptionStart - span.Length);

            webItem.Rank = rank;

            if (webItem.Link.IndexOf(stringUrl) != -1)
            {
                webItem.ContainUrl = true;
            }

            return webItem;
        }
    }
}
