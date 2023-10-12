var counterEvaluation = 0;
$(document).ready(function () {
    counterEvaluation = $("#countEvaluation").val() - 1;
    if (counterEvaluation == -1) {
        addEvaluation();
    }
    
});




function addEvaluation() {
    counterEvaluation++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseClass/AddEvaluation",
        data: { counter: counterEvaluation },
        success: function (response) {
            $("#otherInputHiddent").append('<input type="hidden" id="UpdateTrainingNeedAssessmentRequestEvaluationDTOs_' + counterEvaluation +
                '__Id" name ="UpdateTrainingNeedAssessmentRequestEvaluationDTOs[' + counterEvaluation + '].Id" class="form-control" value = "0" />');
            $("#evaluation").append(response);

            if ($('.add-row tr.attr').length <= 1) {
                $("table .attr .remove").hide();

            } else {
                $("table .attr .remove").show();
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
/*function removeRow(id, idInput) {
    console.log(idInput);
    $("#" + id).remove();
    $("#" + idInput).val(-1);

   
    console.log(lengthp);
    if (lengthp <= 2) {
       $('#' + p + ' ' + 'tr.attr').find('.remove').hide();
    } else {
        $('#' + p + ' ' + 'tr.attr').find('.remove').show();
    }

    //console.log(id);
  
    
}*/



$(document).ready(function () {

    $("#form-tms").on('submit', function () {
        $('select[name^=UpdateTrainingNeedAssessmentRequestEvaluationDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[name^=UpdateTrainingNeedAssessmentRequestEvaluationDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        })
    })
    $("#form-tms").validate();
})