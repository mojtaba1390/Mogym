var counterEq = 0
$(document).ready(function () {
    counterEq = $("#countEquimpment").val() - 1;
    //console.log(counterEq);
    if (counterEq == -1) {
        AddEquipment();
    }
});



function AddEquipment() {
    counterEq++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseClass/AddEquipment",
        data: { "counterEq": counterEq },
        success: function (response) {
            $("#otherInputHiddent").append('<input type="hidden" id="trainingClassEquimentDTOs_' + counterEq +
                '__Id" name ="trainingClassEquimentDTOs[' + counterEq + '].Id" class="form-control" value = "0" />');
            $("#addEq").append(response);
            $('.form-select').select2();
            if ($('#addEq tr').length <= 1) {
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
$(function () {
    $('#form-tms').on('submit', function () {
        $('input[name^=trainingClassEquimentDTOs]').each(function () {
                $(this).rules("add",
                    {
                        required: true,
                        messages: {
                            required: "پر کردن این فیلد اجباری است"
                        }
                })
        });
        $('select[name^=trainingClassEquimentDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
    })

    $('#form-tms').validate();
})

