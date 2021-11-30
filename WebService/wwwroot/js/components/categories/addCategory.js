define(['knockout', 'dataService'], function (ko, ds) {
    return function (params) {

        let currentView = params.currentView;
        let selectName = ko.observable();
        let selectDescription = ko.observable();

        let cancelAddCategory = () => currentView("list-categories");

        let addCategory = () => {
            console.log("addCategory");
            let category = { name: selectName(), description: selectDescription() };
            ds.createCategory(category, newCategory => {
                //categories.push(newCategory);
            });
            currentView("list-categories");
            selectName("");
            selectDescription("");
        }

        return {
            selectName,
            selectDescription,
            addCategory,
            cancelAddCategory
        }

    };
});