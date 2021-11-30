define(['knockout', 'postman'], function (ko, postman) {
    return function (params) {

        let name = ko.observable();
        let description = ko.observable();

        let cancel = () => {
            postman.publish("changeView", "list-categories");
        }

        let add = () => {
            postman.publish("newCategory", { name: name(), description: description() });
            postman.publish("changeView", "list-categories");
        }

        return {
            name,
            description,
            add,
            cancel
        }
    };
});