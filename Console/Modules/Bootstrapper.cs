using log4net;
using Nancy;
using Nancy.Conventions;

namespace WebSite.Modules {
    public class Bootstrapper : DefaultNancyBootstrapper {
        #region Members
        private static readonly ILog logger = LogManager.GetLogger(typeof(Bootstrapper));
        #endregion

        protected override void ConfigureConventions(NancyConventions argNancyConventions) {
            base.ConfigureConventions(argNancyConventions);
            argNancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/Scripts"));
            argNancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/Images"));
        }
    }
}
