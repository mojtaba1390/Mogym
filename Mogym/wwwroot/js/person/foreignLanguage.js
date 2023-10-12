var counterLanguge = 0;
$(document).ready(function () {
    counterLanguge = $("#ForeignLanguageMasteryPersonCount").val();
    counterLanguge = counterLanguge - 1;    
    if (counterLanguge == -1)
    {
        AddLan()
    }
    for (var index = 0; index < $("#PersonEducationDegreeCount").val(); index++) {
        initCalendar("PersonEducationDegreeDTOs_" + index + "__DateReceived");
    }
});

function AddLan() {
    counterLanguge++;
    $.ajax({
        type: "POST",
        url: "/Person/AddLan",
        data: { "counterLan": counterLanguge },
        success: function (response) {
        
            $("#InputHiddent").append('<input type="hidden" id="ForeignLanguageMasteryPersonDTOs_' + counterLanguge +
                '__Id" name ="ForeignLanguageMasteryPersonDTOs[' + counterLanguge + '].Id" class="form-control" value = "0" />');
            $("#foreignLan").append(response);
            if ($('#foreignLan tr.attr').length <= 1) {
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

    $('#ForeignLanguage-form').on('submit', function () {
        // adding rules for inputs with class 'comment'

        $('select[name^=ForeignLanguageMasteryPersonDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
    })
    $('#ForeignLanguage-form').validate();

})

/*$(document).ready(function () {
    if ($('#foreignLan tr.attr').length <= 2 || $('#foreignLan tr.attr').length <= 1) {
        $("table .attr .remove").hide();

    } else {
        $("table .attr .remove").show();
    }
})*/

/*function onclickList() {
   // alert('t')   
    console.log($('#foreignLan tr.attr').length);
    if ($('#foreignLan tr.attr').length <= 2 || $('#foreignLan tr.attr').length <= 1) {
        $("table .attr .remove").hide();

    } else {
        $("table .attr .remove").show();
    }
}*/