function loadCss(url) { var link = document.createElement("link"); link.type = "text/css"; link.rel = "stylesheet"; link.href = url; document.getElementsByTagName("head")[0].appendChild(link); }
function loadIcon(url) { var iconLink = document.createElement("link"); iconLink.rel = "icon"; iconLink.href = url; document.getElementsByTagName("head")[0].appendChild(iconLink); }

loadIcon("Images/favicon.ico");
loadCss("Content/bootstrap.css");
loadCss("Content/bootstrap-theme.css");

require(["Scripts/jquery-2.1.4.js", "Scripts/bootstrap.js", "Scripts/knockout-3.3.0.js"], function ($, b, ko) {

    ko.components.register("textbox-component", {
        viewModel: function (params) {
            var self = this;
            self.caption = ko.observable(params.initialCaption);
            self.placeholder = ko.observable(params.initialPlaceholder);
            self.value = ko.observable(params.initialValue);
        },
        template: '<div class="input-group input-group-sm">'
                    + '<span class="input-group-addon" data-bind="text: caption"></span>'
                    + '<input type="text" class="form-control" placeholder="" aria-describedby="sizing-addon3" data-bind="attr: {placeholder: placeholder}, value: value">'
                  + '</div>'
    });

    var ViewModel = function (first, last) {
        var self = this;
        self.firstName = ko.observable(first);
        self.lastName = ko.observable(last);
        self.fullName = ko.pureComputed(function () { return self.firstName() + " " + self.lastName(); }, this);
    };

    ko.applyBindings(new ViewModel("Stewart", "Rogers"));
})


