using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
            int depthLevel = 2;            bool skipExternal = true;
            string skipUrlsContaining = String.Empty;

            try
            {
                if (args.Length == 3)
                {
                    urlToScan = args[0];
                    depthLevel = Convert.ToInt32(args[1]);
                    skipExternal = Boolean.Parse(args[2]);
                    
                }
                else 
                {
                    ShowUsage();
                    return;
                }


                Log.WriteTabbedInfo(0, "Depth:{0}. Skip External Links:{1}", 
                 depthLevel, skipExternal.ToString());

                Stopwatch counter = Stopwatch.StartNew();

                Collection<Uri> visitedUris = new Collection<Uri>();

                //HttpClient.CurrentCredentialCache = GetCredentialCache(urlToScan); ;

                //HttpClient.CrawlUrl(null, new Uri(urlToScan), 0, depthLevel, visitedUris, skipExternal);
                HttpClient.ScanUrl(new Uri(urlToScan), depthLevel, visitedUris, skipExternal);
                counter.Stop();
                Console.ReadLine();
            
            }
            catch (Exception ex)
            {
                Log.WriteTabbedError(0, "Error: {0}", ex.ToString());
                Log.WriteTabbedInfo(0, "");
                ShowUsage();
            }
            finally
            {
                Log.FlushInfoToEventLog();
                Log.FlushWarningsToEventLog();
                Log.FlushErrorsToEventLog();
            }
        }

/**
        private static CredentialCache GetCredentialCache(string urlToScan)
        {
            NetworkCredential credentials = null;
            CredentialCache credCache = null;
            if (Settings.Default.UserName.Length != 0)
            {
                credentials = new NetworkCredential(Settings.Default.UserName, Settings.Default.Password, Settings.Default.Domain);
                credCache = new CredentialCache();
                Uri requestedUri = new Uri(urlToScan);
                Uri credCacheUri = new Uri(requestedUri.Scheme + "://" + requestedUri.Authority);

                credCache.Add(credCacheUri, "NTLM", credentials);
            }
            return credCache;
        }

        **/
        private void ShowUsage()
        {
            Log.WriteTabbedText(0, "Usage: HttpWarmp <URL> <DEPTH_LEVEL> <SKIP_EXTERNAL_LINKS>\r\n");
        }
    }
}
