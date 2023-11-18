$(document).ready(function () {

    function setplanId(planId) {
        $("#planId").val(planId);
    }


    var workoutConts = $("#workoutConts").val();
    if (workoutConts ==0) {
        addWorkoutRow();
    }

    function addWorkoutRow() {
        workoutConts = $("#workoutConts").val();
        $.ajax({
            type: "POST",
            url: "/Workout/AddWorkoutRow",
            data: { "planId": '@Model.PlanId' },
            success: function (response) {
                $("#workoutBody").append(response);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

})




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