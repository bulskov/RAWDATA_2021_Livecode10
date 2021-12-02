/// <reference path="lib/jquery/dist/jquery.min.js" />
/// <reference path="lib/requirejs/text.js" />
/// <reference path="lib/knockout/build/output/knockout-latest.debug.js" />
/// <reference path="../css/lib/bootstrap/dist/js/bootstrap.bundle.min.js" />


require.config({
    baseUrl: 'js',
    paths: {
        text: "lib/requirejs/text",
        jquery: "lib/jquery/dist/jquery.min",
        knockout: "lib/knockout/build/output/knockout-latest.debug",
        bootstrap: "../css/lib/bootstrap/dist/js/bootstrap.bundle.min",
        dataService: "services/dataService",
        postman: "services/postman"
    }
});

// component registration
require(['knockout'], (ko) => {
    ko.components.register("add-category", {
        viewModel: { require: "components/categories/addCategory" },
        template: { require: "text!components/categories/addCategory.html" }
    });
    ko.components.register("list-categories", {
        viewModel: { require: "components/categories/listCategories" },
        template: { require: "text!components/categories/listCategories.html" }
    });

    ko.bindingHandlers.currency = {
        update: function (element, valueAccessor, allBindings) {
            // First get the latest data that we're bound to
            var value = valueAccessor();

            // Next, whether or not the supplied model property is observable, get its current value
            var valueUnwrapped = ko.unwrap(value);

            element.innerText = "$" + Number(valueUnwrapped).toFixed(2);

 
        }
    };
});

require(["knockout", "viewmodel", "bootstrap"], function (ko, vm) {
    //console.log(vm.firstName);

    ko.applyBindings(vm);

});