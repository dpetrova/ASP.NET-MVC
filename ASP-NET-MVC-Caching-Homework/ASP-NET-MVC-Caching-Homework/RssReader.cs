using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_NET_MVC_Caching_Homework
{
    using System.Net;
    using System.Text;
    using System.Xml.Linq;

    public class RssReader
    {
        public static IEnumerable<Rss> GetFeed()
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var xmlData = client.DownloadString("https://softuni.bg/feed/news");

            XDocument xml = XDocument.Parse(xmlData);

            var updates = (from story in xml.Descendants("item")
                                  select new Rss
                                  {
                                      Title = ((string)story.Element("title")),
                                      Url = ((string)story.Element("link"))
                                  }).Take(10);

            return updates;
        }
    }
}