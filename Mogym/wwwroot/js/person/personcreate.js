'use strick'
var counter = 0;
$(document).ready(function () {
    initCalendar("DateOfBirth");
    initCalendar("DateOfQuit");
    addContactInfo();
    $("#IsExternal").on('change', function () {
       // console.log('tes');
        //console.log($("#IsExternal").val());
        if ($("#IsExternal").val() == 'True') {
            External();
        }
        else
        {
            Internal();
        }
    });
});
function External() {
    $('#OrganizationUnitId').val(null);
    $('#OrganizationPostId').val(null);
    $('#SecondOrganizationPostId').text(null);
    $('#JobId').val(null);
    $('#Pcode').val(null);
    $('#Workplace').text(null);
    $('#InternalNumber').text(null);
    $('#ManagerName').text(null);
    $('#SecondOrganizationPost').text(null);
    $('#SupervisorName').text(null);

    $('#OrganizationUnitId').prop('disabled', true);
    $('#OrganizationPostId').prop('disabled', true);
    $('#SecondOrganizationPostId').prop('disabled', true);
    $('#JobId').prop('disabled', true);
    $('#Pcode').prop('readonly', true);
    $('#Workplace').prop('readonly', true);
    $('#InternalNumber').prop('readonly', true);
    $('#ManagerName').prop('readonly', true);
    $('#SupervisorName').prop('readonly', true);
    $('#SecondOrganizationPost').prop('readonly', true);


}
function Internal() {
    $('#OrganizationUnitId').prop('disabled', false);
    $('#OrganizationPostId').prop('disabled', false);
    $('#SecondOrganizationPostId').prop('disabled', false);
    $('#JobId').prop('disabled', false);
    $('#Pcode').prop('readonly', false);
    $('#Workplace').prop('readonly', false);
    $('#InternalNumber').prop('readonly', false);
    $('#ManagerName').prop('readonly', false);
    $('#SupervisorName').prop('readonly', false);
    $('#SecondOrganizationPost').prop('readonly', false);
}

function addContactInfo() {
    counter++;
    $.ajax({
        type: "POST",
        url: "/Person/ContactInfo",
        data: { "counter": counter },
        success: function (response) {
            $("#contactInfo").append(response);
            if ($('#contactInfo tr.attr').length <= 1) {
                $("table .attr .remove").hide();
            }
            else {
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


function removePersonContactInfo() {

    if ($('#contactInfo tr.attr').length <= 2) {
      
            $("table .attr .remove").hide();
        }
        else {
            $("table .attr .remove").show();
        }
    
}