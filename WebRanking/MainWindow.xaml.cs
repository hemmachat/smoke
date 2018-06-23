using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace WebRanking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var clientBody = client.GetStringAsync("https://www.google.com.au/search?num=100&q=conveyancing+software").Result;

                var start = clientBody.IndexOf("<div id=\"center_col\">");
                var stop = clientBody.LastIndexOf("<div id=\"foot\">");
                var contentArea = clientBody.Substring(start, stop - start);

                start = contentArea.IndexOf("<div id=\"res\">");
                stop = contentArea.LastIndexOf("<div class=\"C4eCVc\"");
                var searchResult = contentArea.Substring(start, stop - start);
                searchResult = searchResult.Replace("<div id=\"res\"><div id=\"topstuff\"></div><div id=\"search\"><div id=\"ires\"><ol>", "");

                var searchItems = new List<string>();

                while (true)
                {
                    start = searchResult.IndexOf("<div class=\"g\">");
                    stop = searchResult.IndexOf("<div class=\"g\">", start + 15);
                    var newStart = stop - start;

                    if (stop > start)
                    {
                        searchItems.Add(searchResult.Substring(start, newStart));
                        searchResult = searchResult.Substring(newStart);
                    }
                    // last item
                    else
                    {
                        searchItems.Add(searchResult.Replace("</ol></div></div></div>", ""));
                        break;
                    }
                }

                var webItems = new List<WebItem>();
                //var anchorRegEx = new Regex("<[aA].*?\\s+href=[\"']([^\"']*)[\"'][^>]*>(?:<.*?>)*(.*?)(?:<.*?>)*<\\/[aA]>");

                foreach (var item in searchItems)
                {
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

                    var cite = "<cite>";
                    var linkStart = item.IndexOf(cite);
                    var linkStop = item.IndexOf("</cite>");
                    webItem.Link = item.Substring(linkStart + cite.Length, linkStop - linkStart - cite.Length);

                    var span = "<span class=\"st\">";
                    var descriptionStart = item.IndexOf(span);
                    var descriptionStop = item.LastIndexOf("</span>");
                    webItem.Description = item.Substring(descriptionStart + span.Length, descriptionStop - descriptionStart - span.Length);

                    webItems.Add(webItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void browser_LoadCompleted(object sender, NavigationEventArgs e)
        {


        }
    }
}
