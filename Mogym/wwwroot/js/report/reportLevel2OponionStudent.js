$(document).ready(function () {
    $('#TrainingNeedAssessmentRequestId').change(function () {
        var needAssesment = $('#TrainingNeedAssessmentRequestId').val();
        window.location = '/ReportLevel2OponionStudent/index?needAssesmentId=' + needAssesment
    })
});