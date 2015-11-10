function loadCss(url) { var link = document.createElement("link"); link.type = "text/css"; link.rel = "stylesheet"; link.href = url; document.getElementsByTagName("head")[0].appendChild(link); }
function loadIcon(url) { var link = document.createElement("link"); link.rel = "icon"; link.href = url; document.getElementsByTagName("head")[0].appendChild(link); }

loadIcon('/favicon.ico');
loadCss('Content/bootstrap.css');
loadCss('Content/bootstrap-theme.css');

requirejs(["Scripts/jquery-2.1.4.js", "Scripts/bootstrap.js", "Scripts/knockout-3.3.0.js"], function (jquery, bootstrap, ko) {
    var ViewModel = function (first, last) {
        this.firstName = ko.observable(first);
        this.lastName = ko.observable(last);
        this.fullName = ko.pureComputed(function () { return this.firstName() + " " + this.lastName(); }, this);
    };

    ko.applyBindings(new ViewModel("Stewart", "Rogers"));
});
