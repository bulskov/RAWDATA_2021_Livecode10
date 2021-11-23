/// <reference path="lib/jquery/dist/jquery.min.js" />
/// <reference path="lib/knockout/build/output/knockout-latest.debug.js" />


require.config({
    baseUrl: 'js',
    paths: {
        jquery: "lib/jquery/dist/jquery.min",
        knockout: "lib/knockout/build/output/knockout-latest.debug"
    }
});


require(["knockout", "viewmodel"], function (ko, vm) {
    console.log(vm.firstName);

    ko.applyBindings(vm);

});