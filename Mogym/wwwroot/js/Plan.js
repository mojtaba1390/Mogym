$(document).ready(function () {


    var workoutConts = $("#workoutConts").val();
    var mealCnt = $("#mealCount").val();
    var supplimentPlanCount = $("#supplimentPlanCount").val();

    if (workoutConts == 0)
        addWorkoutRow();

    if (mealCnt == 0)
        addMealRow();

    if (supplimentPlanCount == 0)
        addSupplimentPlanRow();



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

function addSupplimentPlanRow() {
    var supplimentPlanCount = $("#supplimentPlanCount").val();

    $.ajax({
        type: "POST",
        url: "/SupplimentPlan/AddSupplimentPlanRow",
        data: { "counter": supplimentPlanCount, "planId": $("#planId").val() },
        success: function (response) {
            $("#supplimentPlanBody").append(response);
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
function supplimentDetails(supplimentPlanId) {
    $.ajax({
        url: "/SupplimentPlanDetail/GetSupplimentPlanDetail?supplimentPlanId=" + supplimentPlanId,
        dataType: 'html',
        success: function (data) {
            $('#modal-supplimentDetails').html(data);
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
function addSupplimentPlanDetailRow(row) {
    $.ajax({
        type: "POST",
        url: "/SupplimentPlanDetail/AddSupplimentPlanDetailRow",
        data: { "counter": row, "supplimentPlanId": $("#supplimentPlanId").val() },
        success: function (response) {
            $("#supplimentPlanDetailBody").append(response);
            if ($('#supplimentPlanDetailBody tr.attr').length < 2) {
                $('#supplimentPlanDetailBody .attr .remove').hide();
            }
            else {
                $('#supplimentPlanDetailBody .attr .remove').show();
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


function addDescription() {
    var planId = $("#planId").val();
    var desc = $("#Description").val();
    $.ajax({
        type: "POST",
        url: "/Plan/AddDescription",
        data: { "planId": planId, "description": desc },
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


function deleteWorkout(id) {
    $.ajax({
        type: "POST",
        url: "/Workout/Delete",
        data: { "id": id },
        success: function (response) {
            $('#modal-deleteRow').html(response);
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