var counterDegree = 0;
$(document).ready(function () {    
    counterDegree = $("#PersonEducationDegreeCount").val();
    counterDegree = counterDegree - 1;
    if (counterDegree == -1) {
        AddEducationDegree()
    }

    for (var index = 0; index < $("#PersonEducationDegreeCount").val(); index++) {
        initCalendar("PersonEducationDegreeDTOs_" + index + "__DateReceived");
    }

});
function AddEducationDegree() {
    counterDegree++;
    $.ajax({
        type: "POST",
        url: "/Person/AddEducationDegree",
        data: { "counterEducationDegree": counterDegree },
        success: function (response) {
            $("#InputHiddent").append('<input type="hidden" id="PersonEducationDegreeDTOs_' + counterDegree +
                '__Id" name ="PersonEducationDegreeDTOs[' + counterDegree + '].Id" class="form-control" value = "0" />');
            $("#eDucationDegreePerson").append(response);
            if ($('#eDucationDegreePerson tr.attr').length <= 1) {
                $("table .attr .remove").hide();
            }
            else {
                $("table .attr .remove").show();
            }

      /*    console.log(response);*/
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


$(function (counterDegree) {
    $('#personDegree-form').on('submit', function () {

        // adding rules for inputs with class 'comment'
        $('input[name^=PersonEducationDegreeDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[class^=txtPersonEducationDegreeDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('select[name^=PersonEducationDegreeDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[name$=Average]').each(function () {
            $(this).rules("add",
                {
                    max: 20,
                    min:0,
                    messages: {
                        max: "مقدار بیشتر از 20 نباشد",
                        min: "مقدار کمتر از 0 نباشد",
                    }
                })
        });
    });
    $('#personDegree-form').validate();
})


function removeRowKeyPress() {

    if ($('#eDucationDegreePerson tr.attr').length <= 2) {
        $("table .attr .remove").hide();
    }
    else {
        $("table .attr .remove").show();
    }
}