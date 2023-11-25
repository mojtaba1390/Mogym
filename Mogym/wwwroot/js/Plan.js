$(document).ready(function () {


    workoutConts = $("#workoutConts").val();
    mealCnt = $("#mealCount").val();

    if (workoutConts == 0)
        addWorkoutRow();

    if (mealCnt == 0)
        addMealRow();



})

function addWorkoutRow() {
    workoutConts = $("#mealCount").val();
    $.ajax({
        type: "POST",
        url: "/Workout/AddWorkoutRow",
        data: { "counter": workoutConts, "planId": $("#planId").val() },
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
function addMealRow() {
    mealCnt = $("#mealCount").val();
    $.ajax({
        type: "POST",
        url: "/Meal/AddMealRow",
        data: { "counter": mealCnt, "planId": $("#planId").val() },
        success: function (response) {
            $("#dietBody").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function editWorkout(index, workoutId) {
    var title = $("#WorkoutRecords_" + index + "__Title").val();
    $.ajax({
        type: "POST",
        url: "/Workout/Edit",
        data: { "id": workoutId, "title": title },
        success: function (response) {
            window.location.reload();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function editMeal(index, mealId) {
    var title = $("#MealRecords_" + index + "__Title").val();
    $.ajax({
        type: "POST",
        url: "/Meal/Edit",
        data: { "id": mealId, "title": title },
        success: function (response) {
            window.location.reload();
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