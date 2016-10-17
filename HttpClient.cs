using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace HttpWarmpUp
{
    internal class HttpClient
    {
        
        internal static async Task<string> MakeRequest(string url)
        {
            WebRequest req = WebRequest.Create(url);
            DateTime start = DateTime.Now;
            
            string result = string.Empty;               
            try
            {
                using (WebResponse resp = await req.GetResponseAsync())
                {
                    using (Stream s = resp.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            result = sr.ReadToEnd();
                            ElapsedTime(url, start);
                        }                        
                    }
                }
            }
            catch (WebException wex)
            {
                Console.WriteLine("0, {0}:: {1} - {2}",                      
                    url, 
                    wex.Message,
                    wex.Status);

            }                                    
            return result;
        }

        private static DateTime ElapsedTime(string url, DateTime start)
        {
            TimeSpan taken = DateTime.Now.Subtract(start);
            Console.Write("{0}.{1}, {2}\r\n",
                taken.Seconds.ToString(),
                taken.Milliseconds.ToString(),
                new Uri(url).PathAndQuery);
            return start;
        }
    }
}