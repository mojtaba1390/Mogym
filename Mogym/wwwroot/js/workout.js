$(document).ready(function () {
    $('.js-select-example').select2();
    //$(".js-example-theme-multiple").select2({
    //    theme: "classic"
    //});

    exerciseCnt = $("#exerciseCounts").val();
    if (exerciseCnt == 0)
        addExerciseRow();



})

function addExerciseRow() {
    exerciseCnt = $("#exerciseCounts").val();
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