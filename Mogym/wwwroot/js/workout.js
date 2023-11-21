$(document).ready(function () {
    $('.js-select-example').select2();
    //$(".js-example-theme-multiple").select2({
    //    theme: "classic"
    //});

    var exerciseCnt = $("#exerciseCounts").val();
    if (exerciseCnt == 0)
        addExerciseRow();



})

function addExerciseRow() {
    var exerciseCnt = $("#exerciseCounts").val();
    $.ajax({
        type: "POST",
        url: "/Exercise/AddExerciseRow",
        data: { "counter": exerciseCnt, "workoutId": $("#workoutId").val() },
        success: function (response) {
            $("#exerciseBody").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function viewVideoDetails(videoId) {
    $.ajax({
        url: "/ExerciseVideo/GetVideoDetails?videoId=" + videoId,
        dataType: 'html',
        success: function (data) {
            $('#modal-viewDetails').html(data);
        }
    });
}



$(function () {

    $('#workoutForm').on('submit', function () {


        $('input[name$=Title]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });


    });
    $('#workoutForm').validate();




})