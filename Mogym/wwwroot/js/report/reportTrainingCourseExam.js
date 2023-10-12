$(document).ready(function () {
    $('#TrainingNeedAssessmentRequestId').change(function () {
        var needAssesment = $('#TrainingNeedAssessmentRequestId').val();
        window.location = '/ReportTrainingCourseExam/index?needAssesmentId=' + needAssesment
    })
});