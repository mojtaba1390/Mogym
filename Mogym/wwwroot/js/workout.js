$(document).ready(function () {
    $('.js-select-example').select2();
    //$(".js-example-theme-multiple").select2({
    //    theme: "classic"
    //});

    var exerciseCnt = $("#exerciseCounts").val();
    if (exerciseCnt == 0)
        addExerciseRow();



})

function deleteExercise(id,workoutId) {
    $.ajax({
        type: "POST",
        url: "/Exercise/Delete",
        data: { "id": id,"workoutId":workoutId },
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

function addUpdateExerciseSets(exerciseId) {
    $.ajax({
        url: "/ExerciseSet/ExerciseSetDetail?exerciseId=" + exerciseId,
        dataType: 'html',
        success: function (data) {
            $('#modal-viewAddUpdateExerciseSets').html(data);
        }
    });
}




function addExerciseSetRow(row) {
    $.ajax({
        type: "POST",
        url: "/ExerciseSet/AddExerciseSetRow",
        data: { "counter": row, "exerciseId": $("#exerciseId").val() },
        success: function (response) {
            $("#exerciseSetBody").append(response);
            if ($('#exerciseSetBody tr.attr').length < 2) {
                $('#exerciseSetBody .attr .remove').hide();
            }
            else {
                $('#exerciseSetBody .attr .remove').show();
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