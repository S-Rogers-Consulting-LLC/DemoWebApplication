using Nancy;
using Nancy.Conventions;

namespace WebSite.Modules {
    public class Bootstrapper : DefaultNancyBootstrapper {
        protected override void ConfigureConventions(NancyConventions argNancyConventions) {
            base.ConfigureConventions(argNancyConventions);
            argNancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/Scripts"));
            argNancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/Images"));
        }
    }
}
