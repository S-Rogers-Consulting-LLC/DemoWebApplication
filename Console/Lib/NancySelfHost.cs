using System;
using log4net;
using Nancy.Hosting.Self;

namespace WebSite.Lib {
    public class NancySelfHost {
        #region Members
        private static readonly ILog logger = LogManager.GetLogger(typeof(NancySelfHost));

        public static readonly Int32 MachineDomainPortNumber = 5010;
        public static readonly String MachineDomainName = Environment.MachineName;
        public static readonly Int32 LocalhostDomainPortNumber = 5011;
        public static readonly String LocalhostDomainName = @"localhost";
        public static readonly String DomainSchema = @"http";

        private NancyHost TheNancyHost;
        #endregion

        public NancySelfHost() { }

        public void Start() {
            var uriText = DomainSchema + "://" + LocalhostDomainName + ":" + MachineDomainPortNumber;
            TheNancyHost = new NancyHost(new Uri(uriText));
            TheNancyHost.Start();
        }

        public void Stop() {
            if (null == TheNancyHost)
                return;
            TheNancyHost.Stop();
            TheNancyHost = null;
        }

        public void Pause() {
            if (null == TheNancyHost)
                return;
            TheNancyHost.Stop();
        }

        public void Continue() {
            if (null == TheNancyHost)
                return;
            TheNancyHost.Start();
        }

        public void Shutdown() {
            if (null == TheNancyHost)
                return;
            TheNancyHost.Stop();
            TheNancyHost = null;
        }
    }
}

