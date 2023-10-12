$(document).ready(function () {
    $('#TrainingNeedAssessmentRequestId').change(function () {
        var needAssesment = $('#TrainingNeedAssessmentRequestId').val();
        window.location = '/ReportLevel1/Index?assmentId=' + needAssesment
    })
});