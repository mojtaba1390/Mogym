'use strick'
var counter = 0;
$(document).ready(function () {

  
    if ($("#counter") >= 0) {
        counter = $("#counter").value();
    }
    else {
        callContactInfo();
    }
    /* attributes 1 */
 /*   var row = $(".attr-academy");

    function addRow() {
        row.clone(true, true).appendTo("#attributes-academy table tbody");
    }

    function removeRow(button) {
        button.closest(".attr-academy").remove();
    }

    $('.attr-academy:first-child').find('.attr-academy .remove-button').hide();

    *//* Doc ready *//*
    $(".attr-academy .add-button").on('click', function () {
       *//* addRow();
        if ($(".attr-academy").length > 1) {
            //alert("Can't remove row.");
            $(".attr-academy .remove-button").show();
            select.value = '';
        }*//*

    });*/
    $('table').on('click', '.remove-button', function () {
        if ($(".attr-academy").length == 1) {
            $(".attr-academy .remove-button").hide();
        } else {
            $(this).closest('tr').remove();
        }
        if ($(".attr-academy").length == 1) {
            $(".attr-academy .remove-button").hide();
        }
    });
})


function callContactInfo() {
    counter++;
    $.ajax({
        type: "POST",
        url: "/Academy/ContactInfo",
        data: { "counter": counter },
        success: function (response) {
            $("#contactInfo").append(response);
            if ($('#contactInfo tr.attr').length <= 1) {
                $("table .attr .remove").hide();

            } else {
                $("table .attr .remove").show();
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
 
$(document).ready(function () {


    $("#form-tms").validate({
        rules: {
            Title: {
                required: true,
                maxlength: 64
            },
        },
        messages: {
            Title: {
                required: "پر کردن این فرم الزامی است",
                maxlength: "عنوان بیشتر از 64 کارکتر میباشد.",
            },
        }
    })
})