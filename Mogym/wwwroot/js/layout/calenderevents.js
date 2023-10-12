$(document).ready(function () {
    $("#link-educational-calendar").click(function () {
       // console.log('runs');
        $.ajax({
            type: "Get",
            url: "/Home/EventWeek",
            success: function (response) {
               // console.log(response)
                $("#educational-calendar").find(".modal-body").html(response);
                $("#educational-calendar").modal('show');
            },
            failure: function (response) {
                alert('failure');
            },
            error: function (response) {
                alert('error');
            }
        });
    });

});


$(function () {
    
});