using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

internal class Crawler
{
    Uri baseUrl;
    public Crawler(string baseUrl1)
    {
        baseUrl = new Uri(baseUrl1);
    }
    IList<string> _uris = new List<string>();
    public async Task<CrawlResult> ScanUrl(string url="")
    {
        Stopwatch watch = Stopwatch.StartNew();
        if (string.IsNullOrEmpty(url))
        {
          url = baseUrl.ToString();   
        }
        var u = new Uri(url);
       
        var h = new HttpClient();
        var html = await h.GetStringAsync(u);
        Console.WriteLine($"{url} in {watch.ElapsedMilliseconds} ms");
        AddLinks(html, baseUrl);
        foreach (var link in _uris)
        {
            if  (baseUrl.ToString()!=link)
            {
                await ScanUrl(link);
            }
        }
        return new CrawlResult {Url="", SeenUris= _uris, TimeTaken= watch.Elapsed};
    }

    void AddLinks(string html, Uri baseUrl)
    {
        foreach (var docLink in GetLinksFromHtml(html))
        {            
            if (!_uris.Contains(docLink))
            {
                _uris.Add(docLink);
            }
        }
    }
    IList<string> GetLinksFromHtml(string html)
    {
        string SafeUri(HtmlNode n)
        {
            string uri = n.Attributes["href"].Value.Trim();
            Uri newUri;
            if (!Uri.TryCreate(uri, UriKind.RelativeOrAbsolute, out newUri))
            {
                Console.Write("BAD URL:" + uri);
            }
            return newUri.ToString();
        }
        
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);
        HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");
        return links.Select(n=>FilterUrl(SafeUri(n))).ToList();                  
    }

    private string FilterUrl(string u)
    {
        string result = string.Empty;
        if (!u.ToLower().Contains("javascript:") && Uri.IsWellFormedUriString(u, UriKind.RelativeOrAbsolute))
        {
            Uri uri = new Uri(u, UriKind.RelativeOrAbsolute);
            if (!_uris.Contains(uri.ToString()))
            {
                if (uri.IsAbsoluteUri && uri.ToString() != baseUrl.ToString())
                {
                    result = u;               
                }
                else
                {
                    result = new Uri(baseUrl, u).ToString();
                }
            }
        }
        return result;
    }

    public class CrawlResult
    {
        public string Url { get; set; }
        public IList<string> SeenUris { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public string Summary => $"{Url} has {SeenUris.Count} in {TimeTaken} ms." ;
    }
}