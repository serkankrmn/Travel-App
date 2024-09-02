// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function showLoading() {
    $("#loadingdiv").remove();
    var loadingDiv = "<div id='loadingdiv' style='background: #6d6d6d45;position: fixed;top:0;left:0;width:100%;height:100%;z-index:999999999999;display:flex;justify-content:center;align-items:center;'><div class='spinner-border' role='status'><span class='sr-only'>Loading...</span></div></div>";
    $("body").append(loadingDiv);
}

function hideLoading() {

    $("#loadingdiv").remove();
}


function showError(messeage) {

    Toastify({
        text: messeage,
        duration: 5000,
        destination: "",
        newWindow: true,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        style: {
            background: "linear-gradient(to right, rgb(176 0 0), rgb(111 111 111))",
        },
        onClick: function () { } // Callback after click
    }).showToast();
}