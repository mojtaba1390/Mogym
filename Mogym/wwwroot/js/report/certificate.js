$(document).ready(function () {

    $('#TrainingNeedAssessmentRequestId').change(function () {
        getPersonAssinged();
    })

});

function getPersonAssinged() {
    $.ajax({
        type: 'POST',
        url: '/TrainingNeedAssessmentRequest/GetPersonAssinged/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { trainingNeedAssessmentRequest: $('#TrainingNeedAssessmentRequestId').val() },
        success: function (result) {
            $("#personId").empty();
            $("#personId").append($('<option>', {
                value: null,
                text: 'فرد را انتخاب کنید.'
            }));
            $.each(result, function (i, item) {
                $("#personId").append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
            });
        },
        error: function () {

        }
    })
}