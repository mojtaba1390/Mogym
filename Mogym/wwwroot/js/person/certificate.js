var counterCourseCertificate = 0;
$(document).ready(function () {
    counterCourseCertificate = $("#CourseCertificatePersonCount").val() - 1;
    if (counterCourseCertificate == -1) {
        AddCertificate();
    }

    for (var index = 0; index < $("#CourseCertificatePersonCount").val(); index++) {
        initCalendar("courseCertificatePersonDTOs_" + index + "__ExecutionTime");
    }

});

function AddCertificate() {
    counterCourseCertificate++;
    $.ajax({
        type: "POST",
        url: "/Person/AddCertificate",
        data: { "countCer": counterCourseCertificate  },
        success: function (response) {
            $("#InputHidden").append('<input type="hidden" id="courseCertificatePersonDTOs_' + counterCourseCertificate +
                '__Id" name ="courseCertificatePersonDTOs_[' + counterCourseCertificate + '].Id" class="form-control" value = "0" />');
            $("#certificate").append(response);
            if ($('#certificate tr.attr').length <= 1) {
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
$(function () {


$('#personCertificate-form').on('submit', function () {
    // adding rules for inputs with class 'comment'
    $('input[name^=courseCertificatePersonDTOs]').each(function () {
        $(this).rules("add",
            {
                required: true,
                messages: {
                    required: "پر کردن این فیلد اجباری است"
                }
            })
    });
    $('input[class^=txtcourseCertificatePersonDTOs]').each(function () {
        $(this).rules("add",
            {
                required: true,
                messages: {
                    required: "پر کردن این فیلد اجباری است"
                }
            })
    });
    $('select[name^=courseCertificatePersonDTOs]').each(function () {
        $(this).rules("add",
            {
                required: true,
                messages: {
                    required: "پر کردن این فیلد اجباری است"
                }
            })
    })
})
$('#personCertificate-form').validate();
})