$(document).ready(function () {
    $('#TrainingNeedAssessmentRequestId').change(function () {
        var needAssesment = $('#TrainingNeedAssessmentRequestId').val();
        window.location = '/ReportPresenceAndAbsence/index?needAssesmentId=' + needAssesment
    })
});