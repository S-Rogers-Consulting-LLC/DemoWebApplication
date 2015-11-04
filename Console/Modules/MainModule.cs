using log4net;
using Nancy;

namespace WebSite.Modules {
    public class MainModule : NancyModule {
        #region Members
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainModule));
        #endregion

        public MainModule() {
            Get[@"/"] = parameters => { return View[@"main.html"]; };
        }
    }
}