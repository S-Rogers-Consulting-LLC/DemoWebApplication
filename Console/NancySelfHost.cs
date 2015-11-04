using System;
using Nancy.Hosting.Self;

namespace WebSite {
    public class NancySelfHost {
        public static readonly Int32 DomainPortNumber = 5000;
        public static readonly String MachineDomainName = Environment.MachineName;
        public static readonly String LocalhostDomainName = @"localhost";
        public static readonly String DomainSchema = @"http";

        private NancyHost TheNancyHost;

        public NancySelfHost() { }

        public void Start() {
            var uriText = DomainSchema + "://" + LocalhostDomainName + ":" + DomainPortNumber;
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

