$(document).ready(function () {
    $('#TrainingNeedAssessmentRequestId').change(function () {
       // GetParticiPants();
        var needAssesment =  $('#TrainingNeedAssessmentRequestId').val();
        window.location = '/ReportAcdemyAndTeacherEvaluation/Index?assmentId=' + needAssesment ;
    })

    $('#ParticipantId').change(function () {
        var participantId = $("#ParticipantId").val();
        var needAssesment =  $('#TrainingNeedAssessmentRequestId').val();
        window.location = '/ReportAcdemyAndTeacherEvaluation/Index?assmentId=' + needAssesment + '&participantId=' + participantId;

    })

});

function GetParticiPants() {
    $.ajax({
        type: 'POST',
        url: '/TrainingNeedAssessmentRequest/GetPersonAssinged/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { trainingNeedAssessmentRequest: $("#TrainingNeedAssessmentRequestId").val() },
        success: function (result) {
           // console.log(result);
            $("#ParticipantId").empty();
            $("#ParticipantId").append($('<option>', {
                value: null,
                text: 'شرکت کننده را انتخاب کنید'
            }));
            $.each(result, function (i, item) {
                $("#ParticipantId").append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
            });
        },
        error: function () {

        }
    })
}



