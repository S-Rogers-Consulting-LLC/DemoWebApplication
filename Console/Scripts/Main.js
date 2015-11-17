function loadCss(url) { var link = document.createElement("link"); link.type = "text/css"; link.rel = "stylesheet"; link.href = url; document.getElementsByTagName("head")[0].appendChild(link); }
function loadIcon(url) { var iconLink = document.createElement("link"); iconLink.rel = "icon"; iconLink.href = url; document.getElementsByTagName("head")[0].appendChild(iconLink); }

loadIcon("Images/favicon.ico");
loadCss("Content/w2ui-1.4.3.css");

require(["Scripts/jquery-2.1.4.js", "Scripts/w2ui-1.4.3.js", "Scripts/knockout-3.3.0.debug.js"], function ($, w2, ko) {
    console.debug('start components load.');

    ko.components.register("textbox-component", {
        viewModel: function (params) {
            var self = this;
            self.id = params.initialId;           
            self.width = params.width;
            self.placeholder = params.initialPlaceholder;
            self.value = params.value;
        },
        template: '<input id="" placeholder="" data-bind="attr: {id: id, placeholder: placeholder}, value: value" style="border: 1px solid silver; padding: 3px;" type="text">'
    });

    ko.components.register("label-component", {
        viewModel: function (params) {
            var self = this;
            self.id = params.initialId;
            self.width = params.width;
            self.value = params.value;
        },
        template: '<span id="" data-bind="attr: {id: id}, text: value" style="border: 1px solid silver; padding: 3px;"></span>'
    });

    ko.components.register("button-component", {
        viewModel: function (params) {
            var self = this;
            self.id = params.initialId;
            self.width = params.width;
            self.caption = params.initialCaption;
            self.onclick = params.onclick;
        },
        template: '<input id="" data-bind="attr: {id: id, onclick: onclick}, value: caption" style="border: 1px solid silver; padding: 3px;" type="button">'
    });

    console.debug('stop components load.');

    

    var ViewModel = function (first, last) {
        var self = this;
        self.firstName = ko.observable(first);
        self.lastName = ko.observable(last);
        self.fullName = ko.pureComputed(function () { return self.firstName() + " " + self.lastName(); }, self);
    };

    ko.applyBindings(new ViewModel("a", "b"));

   
})


