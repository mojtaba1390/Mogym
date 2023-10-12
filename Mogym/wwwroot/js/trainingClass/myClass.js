'use strick'

function changeClassType() {
    //console.log($('#offline').val());
    if ($('#FitlerTrainingClass_ClassTypeCategory').val() == $('#offline').val()) {
        $('#FitlerTrainingClass_ClassState').prop('disabled', true);
    }
    else
        $('#FitlerTrainingClass_ClassState').prop('disabled', false);

}


function connectUser(id) {
    $.ajax({
        type: 'POST',
        url: '/TrainingCourseClass/ConnectUserMeeting/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { id: id },
        success: function (result) {
            location.href = result;
        },
        error: function () {

        }
    })
}