'use strick'

$(document).ready(function () {
    $('#TrainingNeedAssessmentRequestId').change(function () {
        changeTrainingNeedAssement();
    });
});

function changeTrainingNeedAssement() {
    var requestId = $('#TrainingNeedAssessmentRequestId').val();
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetSummaryTrainingNeedAssement",
        data: { "id": requestId },
        success: function (response) {
           // console.log(response);
            var courseIds = [];
            $('#trainingCourseId').empty();
            $.each(response.trainingCourseDTOs, function (i, item) {
                $('#trainingCourseId').append($('<option>', {
                    value: item.id,
                    text: item.title
                }));
                courseIds.push(item.id);
            });
            $('#trainingCourseId').val(courseIds);

            var personIds = [];
            $('#personIds').empty();
            $.each(response.persons, function (i, item) {
                $('#personIds').append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
                personIds.push(item.id);
            });
            $('#personIds').val(personIds);

            var organizationIds = [];
            $('#organizationUnitId').empty();
            $.each(response.organizationUnitDTOs, function (i, item) {
                $('#organizationUnitId').append($('<option>', {
                    value: item.id,
                    text: item.title
                }));
                organizationIds.push(item.id);
            });
            
            $('#organizationUnitId').val(organizationIds);

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}