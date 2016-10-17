using System;

namespace HttpWarmpUp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            new Program().Run(args);
        }
        void Run(string[] args)
        {
            string urlToScan = string.Empty;
            int depthLevel = 0;

            if (args.Length < 2)
            {
                ShowUsage();
                return;
            }
            urlToScan = args[0];
            depthLevel = Convert.ToInt32(args[1]);
            Console.WriteLine("0, Start scanning {0} with {1} depth", urlToScan, depthLevel);
            CrawlUrl(urlToScan, depthLevel);
            Console.ReadLine();
        }

        async void CrawlUrl(string url, int depth)
        {
            string html = await HttpClient.MakeRequest(url);
            if (depth > 0)
            {
                new HtmlParser().WalkDoc(html, url, depth);
            }
        }

        private void ShowUsage()
        {
            Console.WriteLine("HttpWarmp <URL> <DEPTH_LEVEL>\r\n");
        }
    }
}
