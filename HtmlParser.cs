using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace HttpWarmpUp
{
    internal class HtmlParser
    {
        static int globalDepth = 0;                
        static List<String> requestedUrls = new List<string>();

        internal async void WalkDoc(string html, string baseUrl, int depth)
        {
            if (String.IsNullOrEmpty(html)) return;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            int found = 0;
            string target = string.Empty;
                       
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");
                
                foreach (HtmlNode link in links)
                {
                    target = FilterUrl(link.Attributes["href"].Value,baseUrl);
                    if (target.Length > 0 && !requestedUrls.Contains(target))
                    {
                        found++;
                        requestedUrls.Add(target);
                        string html2 = await HttpClient.MakeRequest(target);
                        while (depth > globalDepth)
                        {
                            globalDepth++; 
                            new HtmlParser().WalkDoc(html2, baseUrl, depth);
                                                       
                        }

                        
                    }                    
                }
                Console.WriteLine("Found {0}/{1} urls in depth {2} from {3}",
                                        found, links.Count, globalDepth, target);
           
        }

        static string FilterUrl(string u, string baseUrl)
        {
            //Console.WriteLine(u);
            string result = string.Empty;
            if (Uri.IsWellFormedUriString(u, UriKind.RelativeOrAbsolute))
            {
                Uri uri = new Uri(u, UriKind.RelativeOrAbsolute);
                if (uri.IsAbsoluteUri)
                {
                    if (uri.DnsSafeHost == new Uri(baseUrl).DnsSafeHost)
                    {
                        result = u;
                    }
                }
                else
                {
                    if (!u.StartsWith("/"))
                    {
                        u = "/" +u;
                    }
                    result = new Uri(baseUrl + u).ToString();
                }
            }
            return result;
        }
    }
}