using System;
using Nancy;

namespace WebSite.Modules {
    public class MainModule : NancyModule {
        public MainModule() {
            Get[@"/"] = parameters => { return View[@"main.html"]; };
        }
    }
}