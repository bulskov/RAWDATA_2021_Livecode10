

var btn = document.getElementById("btn");
var input = document.getElementById("in");
var output = document.getElementById("out");
var output2 = document.getElementById("out2");

btn.addEventListener('click', function () {
    var val = input.value;
    output.innerHTML = val;
    output2.innerHTML = val;
});

function getCategories(callback) {
    fetch("api/categories", { method: 'GET' })
        .then(response => response.json())
        .then(json => {
            //console.log(json);
            callback(json);
        });
}


getCategories(function (categories) {
    console.log(categories);
});