'use strick'
var counter = 0;
$(document).ready(function () {
    addContactInfo();

    /*function addRowCustom() {

        var $item = $('.attr-person:last-child');
        var $newRow = $('<tr class="attr-person form-group">\
            <td width = "20%">\
            <select class="form-select form-control" aria-label="Default select exampleFormControlSelect1">\
            <option selected>موبایل</option>\
            <option value="one">First</option>\
            <option value="three">Third</option>\
            </select>\
            </td>\
            <td width="30%">\
            <div class="form-group">\
            <input type="text" class="form-control" name="value" id="value" />\
            </div>\
            </td>\
            <td width="10%">\
            <button class="btn remove-person mx-5 text-danger tooltip" type="button"><i class="fas fa-times"></i> <span class="tooltiptext tooltip-top">حذف کردن</span></button>\
            <button class="btn btn-large add-person text-success tooltip" type="button"><i class="fas fa-plus"></i><span class="tooltiptext tooltip-top">اضافه کردن</span></button>\
            </td>\
            </tr> ');
        $item.after($newRow);
    }

    $(".approverRowsContainer").on('click', '.attr-person:last-child .add-person', function () {
        addRowCustom();
    });

 
*/
   $('table').on('click', '.remove-person', function () {
        if ($(".attr-person").length == 1) {
            $(".attr-person .remove-person").hide();
        } else {
            $(this).closest('tr').remove();
        }
    })
});


function addContactInfo() {
    counter++;
    $.ajax({
        type: "POST",
        url: "/Person/ContactInfo",
        data: { "counter": counter },
        success: function (response) {
            $("#contactInfo").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}