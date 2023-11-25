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

function ingridientDetails(mealId) {
    $.ajax({
        url: "/MealIngridient/GetMealIngridient?mealId=" + mealId,
        dataType: 'html',
        success: function (data) {
            $('#modal-ingridientDetails').html(data);
        }
    });
}


function addMealIngridientRow(row) {
    $.ajax({
        type: "POST",
        url: "/MealIngridient/AddMealIngridientRow",
        data: { "counter": row, "mealId": $("#mealId").val() },
        success: function (response) {
            $("#mealIngridientBody").append(response);
            if ($('#mealIngridientBody tr.attr').length < 2) {
                $('#mealIngridientBody .attr .remove').hide();
            }
            else {
                $('#mealIngridientBody .attr .remove').show();
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function removeRow(id, idInput) {
    //let p = $("#" + id).parent().attr('id')
    $('#' + id + ' input#' + idInput).val(-1)

    //$("#" + idInput).val(-1);
    $("#" + id).hide();
    //let lengthp = $('#' + p + ' ' + 'tr.attr').length;
    //if (lengthp < 2) {
    //    $('#' + p + ' ' + 'tr.attr').hide();
    //} else {
    //    $('#' + p + ' ' + 'tr.attr').show();
    //}

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