using System;
using System.Globalization;
using System.Linq;
using log4net;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace WebSite.Modules {
    public class Bootstrapper : DefaultNancyBootstrapper {
        #region Members
        private static readonly ILog logger = LogManager.GetLogger(typeof(Bootstrapper));
        #endregion

        protected override void ConfigureConventions(NancyConventions argNancyConventions) {
            argNancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/Scripts"));
            argNancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/Images"));
            argNancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/Fonts"));

            base.ConfigureConventions(argNancyConventions);
        }

        protected override void ApplicationStartup(TinyIoCContainer argTinyIoCContainer, IPipelines argPipelines) {
            argPipelines.AfterRequest += ModifyModel;
            base.ApplicationStartup(argTinyIoCContainer, argPipelines);
        }

        private void ModifyModel(NancyContext argNancyContext) {
            CheckForIfNonMatch(argNancyContext);
            CheckForIfModifiedSince(argNancyContext);
        }

        private void CheckForIfNonMatch(NancyContext argNancyContext) {
            var request = argNancyContext.Request;
            var response = argNancyContext.Response;

            var responseETag = string.Empty;
            if (!response.Headers.TryGetValue("ETag", out responseETag))
                return;

            if (request.Headers.IfNoneMatch.Contains(responseETag))
                argNancyContext.Response = HttpStatusCode.NotModified;
        }

        private void CheckForIfModifiedSince(NancyContext argNancyContext) {
            var request = argNancyContext.Request;
            var response = argNancyContext.Response;

            string responseLastModified;
            if (!response.Headers.TryGetValue("Last-Modified", out responseLastModified))
                return;

            DateTime lastModified;
            if (!request.Headers.IfModifiedSince.HasValue || !DateTime.TryParseExact(responseLastModified, "R", CultureInfo.InvariantCulture, DateTimeStyles.None, out lastModified))
                return;

            if (lastModified <= request.Headers.IfModifiedSince.Value)
                argNancyContext.Response = HttpStatusCode.NotModified;
        }
    }
}
