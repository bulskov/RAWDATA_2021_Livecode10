define(['knockout', 'dataService', 'postman'], function (ko, ds, postman) {
    return function (params) {

        let selectName = ko.observable();
        let selectDescription = ko.observable();

        let cancelAddCategory = () => {
            //currentView("list-categories");
            postman.publish("changeView", "list-categories");
        }

        let addCategory = () => {
            console.log("addCategory");
            let category = { name: selectName(), description: selectDescription() };

            postman.publish("newCategory", category);
            postman.publish("changeView", "list-categories");

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