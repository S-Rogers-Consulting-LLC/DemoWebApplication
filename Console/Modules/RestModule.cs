namespace WebSite.Modules {
    class RestModule : Nancy.NancyModule {
        public RestModule() : base("/REST") {
            Get["/{id}"] = parameter => { return GetById(parameter.id); };
        }

        private object GetById(int id) {
            return new { Id = id, Title = "Site Administrator", Level = 2 };
        }
    }
}

