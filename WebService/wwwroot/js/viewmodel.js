define(["knockout", "postman"], function (ko, postman) {

    let amount = ko.observable(123.4567);

    let menuItems = [
        { title: "List", component: "list-categories" },
        { title: "Add", component: "add-category" }
    ];

    let currentView = ko.observable(menuItems[0].component);

    let changeContent = menuItem => {
        console.log(menuItem);
        currentView(menuItem.component)
    };

    let isActive = menuItem => {
        return menuItem.component === currentView() ? "active" : "";
    }

    postman.subscribe("changeView", function (data) {
        currentView(data);
    });
    
    return {
        currentView,
        menuItems,
        changeContent,
        isActive,
        amount
    }
});