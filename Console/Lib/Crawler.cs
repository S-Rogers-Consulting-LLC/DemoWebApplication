using System;
using Abot.Crawler;
using Abot.Poco;

namespace WebSite.Lib {
    public class Crawler {
        private IWebCrawler TheWebCrawler = null;

        public Crawler(string argUrl) {
            TheWebCrawler = GetManuallyConfiguredWebCrawler();

            TheWebCrawler.PageCrawlCompleted += PageCrawlCompletedEvent;
            TheWebCrawler.PageCrawlDisallowed += PageCrawlDisallowedEvent;
            TheWebCrawler.PageCrawlStarting += PageCrawlStartingEvent;
            TheWebCrawler.PageLinksCrawlDisallowed += PageLinksCrawlDisallowedEvent;

            var crawlResult = TheWebCrawler.Crawl(new Uri(argUrl));
        }

        private void PageLinksCrawlDisallowedEvent(object sender, PageLinksCrawlDisallowedArgs e) {

        }

        private void PageCrawlStartingEvent(object sender, PageCrawlStartingArgs e) {

        }

        private void PageCrawlDisallowedEvent(object sender, PageCrawlDisallowedArgs e) {

        }

        private void PageCrawlCompletedEvent(object sender, PageCrawlCompletedArgs e) {

        }

        private static IWebCrawler GetDefaultWebCrawler() { return new PoliteWebCrawler(); }

        private static IWebCrawler GetManuallyConfiguredWebCrawler() {
            var crawlConfiguration = new CrawlConfiguration();

            crawlConfiguration.CrawlTimeoutSeconds = 0;
            crawlConfiguration.DownloadableContentTypes = "text/html, text/plain";
            crawlConfiguration.IsExternalPageCrawlingEnabled = false;
            crawlConfiguration.IsExternalPageLinksCrawlingEnabled = false;
            crawlConfiguration.IsRespectRobotsDotTextEnabled = false;
            crawlConfiguration.IsUriRecrawlingEnabled = false;
            crawlConfiguration.MaxConcurrentThreads = 10;
            crawlConfiguration.MaxPagesToCrawl = 10;
            crawlConfiguration.MaxPagesToCrawlPerDomain = 0;
            crawlConfiguration.MinCrawlDelayPerDomainMilliSeconds = 1000;

            crawlConfiguration.ConfigurationExtensions.Add("Somekey1", "SomeValue1");
            crawlConfiguration.ConfigurationExtensions.Add("Somekey2", "SomeValue2");

            return new PoliteWebCrawler(crawlConfiguration, null, null, null, null, null, null, null, null);
        }

        private static IWebCrawler GetCustomBehaviorUsingLambdaWebCrawler() {
            var crawler = GetDefaultWebCrawler();

            //Register a lambda expression that will make Abot not crawl any url that has the word "ghost" in it.
            //For example http://a.com/ghost, would not get crawled if the link were found during the crawl.
            //If you set the log4net log level to "DEBUG" you will see a log message when any page is not allowed to be crawled.
            //NOTE: This is lambda is run after the regular ICrawlDecsionMaker.ShouldCrawlPage method is run.
            crawler.ShouldCrawlPage((pageToCrawl, crawlContext) => {
                if (pageToCrawl.Uri.AbsoluteUri.Contains("ghost"))
                    return new CrawlDecision { Allow = false, Reason = "Scared of ghosts" };
                return new CrawlDecision { Allow = true };
            });

            //Register a lambda expression that will tell Abot to not download the page content for any page after 5th.
            //Abot will still make the http request but will not read the raw content from the stream
            //NOTE: This lambda is run after the regular ICrawlDecsionMaker.ShouldDownloadPageContent method is run
            crawler.ShouldDownloadPageContent((crawledPage, crawlContext) => {
                if (crawlContext.CrawledCount >= 5)
                    return new CrawlDecision { Allow = false, Reason = "We already downloaded the raw page content for 5 pages" };
                return new CrawlDecision { Allow = true };
            });

            //Register a lambda expression that will tell Abot to not crawl links on any page that is not internal to the root uri.
            //NOTE: This lambda is run after the regular ICrawlDecsionMaker.ShouldCrawlPageLinks method is run
            crawler.ShouldCrawlPageLinks((crawledPage, crawlContext) => {
                if (!crawledPage.IsInternal)
                    return new CrawlDecision { Allow = false, Reason = "We dont crawl links of external pages" };
                return new CrawlDecision { Allow = true };
            });

            return crawler;
        }

        private static Uri GetSiteToCrawl(string[] args) {
            string userInput = "";
            if (args.Length < 1) {
                System.Console.WriteLine("Please enter ABSOLUTE url to crawl:");
                userInput = System.Console.ReadLine();
            } else {
                userInput = args[0];
            }

            if (string.IsNullOrWhiteSpace(userInput))
                throw new ApplicationException("Site url to crawl is as a required parameter");

            return new Uri(userInput);
        }
    }
}
