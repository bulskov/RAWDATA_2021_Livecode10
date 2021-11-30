define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {
        let categories = ko.observableArray([]);

        let del = category => {
            categories.remove(category);
            ds.deleteCategory(category);
        }

        let create = () => postman.publish("changeView", "add-category");

        ds.getCategories(categories);

        postman.subscribe("newCategory", category => {
            ds.createCategory(category, newCategory => {
                categories.push(newCategory);
            });
        }, "list-categories");

        return {
            categories,
            del,
            create
        };
    };
});
