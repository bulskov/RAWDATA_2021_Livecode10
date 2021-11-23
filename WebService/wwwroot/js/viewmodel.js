define(["knockout"], function (ko) {

    let firstName = ko.observable("Peter");
    let lastName = ko.observable("Smith");
    let names = ko.observableArray([]);

    let fullName = ko.computed(function () {
        return firstName() + " " + lastName();
    });

    let addName = function (data) {
        names.push({ firstName: data.firstName(), lastName: data.lastName() });
    }

    return {
        firstName,
        lastName,
        fullName,
        names,
        addName
    }
});