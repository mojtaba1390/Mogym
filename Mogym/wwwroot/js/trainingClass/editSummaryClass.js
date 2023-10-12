






$(document).ready(function () {

   /* initCalendar("SinceDate");
    initCalendar("UntilDate");*/
    changeTrainingPlan();
});


function changeTrainingPlan() {
    var trainingPlaneId = $("#TrainingPlanId").val();
    /*  console.log(trainingPlaneId);*/
    if (trainingPlaneId > 0) {
        $.ajax({
            type: "POST",
            url: "/TrainingNeedAssessmentRequest/GetSummaryWithTrainingPlanId",
            data: { "id": trainingPlaneId },
            success: function (response) {
                /*  console.log(response);*/
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
                //console.log(personIds);
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

                $('#classTypeId').append($('<option>', {
                    value: response.classTypeDTO.id,
                    text: response.classTypeDTO.title
                }));
                $('#classTypeId').val(response.classTypeDTO.id);
                //console.log(response);
                $('.txtSinceDate').val(response.shamsiSinceDate);
                $('.txtUntilDate').val(response.shamsiUntilDate);
                $('#SinceDate').val(response.sinceDate);
                $('#UntilDate').val(response.untilDate);  
                //  checkInputOnline(response.classTypeDTO.classTypeCategory);


            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    }



}

