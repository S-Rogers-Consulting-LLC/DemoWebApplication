using log4net;

namespace WebSite.Modules {
   public class RestModule : Nancy.NancyModule {
        #region Members
        private static readonly ILog logger = LogManager.GetLogger(typeof(RestModule));
        #endregion

        public RestModule() : base("/REST") {
            Get["/{id}"] = parameter => { return GetById(parameter.id); };
        }

        private object GetById(int id) {
            return new { Id = id, Title = "Site Administrator", Level = 2 };
        }
    }
}

