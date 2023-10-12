$(document).ready(function () {
    $('#TrainingNeedAssessmentRequestId').change(function () {
        var needAssesment = $('#TrainingNeedAssessmentRequestId').val();
        window.location = '/ReportLevel3/index?assmentId=' + needAssesment
    })
});