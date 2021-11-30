define(['knockout', 'dataService'], function (ko, ds) {
    return function (params) {
        let categories = ko.observableArray([]);
        let currentView = params.currentView

        let deleteCategory = category => {
            console.log(category);
            categories.remove(category);
            ds.deleteCategory(category);
        }

        let addCategoryView = () => currentView("add-category");

        ds.getCategories(data => {
            console.log(data);
            categories(data);
        });

        return {
            categories,
            deleteCategory,
            addCategoryView
        };
    };
});
