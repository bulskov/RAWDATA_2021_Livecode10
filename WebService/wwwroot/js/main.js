/// <reference path="lib/jquery/dist/jquery.min.js" />
/// <reference path="lib/requirejs/text.js" />
/// <reference path="lib/knockout/build/output/knockout-latest.debug.js" />


require.config({
    baseUrl: 'js',
    paths: {
        text: "lib/requirejs/text",
        jquery: "lib/jquery/dist/jquery.min",
        knockout: "lib/knockout/build/output/knockout-latest.debug",
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
});

require(["knockout", "viewmodel"], function (ko, vm) {
    //console.log(vm.firstName);

    ko.applyBindings(vm);

});