

var counterExperince = 0;
function SetDifference(row) {
    //console.log(row);
    var valstartDate = $("#workExperiencePersonDTOs_" + row + "__StartedAt").val();
    var valfinishDate = $("#workExperiencePersonDTOs_" + row + "__FinishedAt").val();

    if (valstartDate != '' && valfinishDate != '')
    {
/*      const startArray = valstartDate.split('-').map(Number);
        const finishArray = valfinishDate.split('-').map(Number);*/

        //start comment mojtaba
        /* var a = moment(valstartDate);
        var b = moment(valfinishDate);
        var milliseconds = b.diff(a);
        var years = b.diff(a, 'year');
        a.add(years, 'years');

        var months = b.diff(a, 'months');
        a.add(months, 'months');

        var days = b.diff(a, 'days');

        //console.log(years + ' years ' + months + ' months ' + days + ' days');
        $("#milliseconds-" + row).val(milliseconds);
        $("#workExperiencePersonDTOs_" + row + "__Year").val(years);
        $("#workExperiencePersonDTOs_" + row + "__Month").val(months);
        $("#workExperiencePersonDTOs_" + row + "__Day").val(days);
        var Duration = '';
        if (years != 0)
        {
            Duration += years + ' سال' + '-';
        }
        if (months != 0)
        {
            Duration += months + ' ماه' + '-';
        }
        if (days != 0) {
            Duration += days + ' روز';
        }*/
        //end comment mojtaba

        $.ajax({
            type: "POST",
            url: "/Person/GetDatesDiffrence",
            data: { "startDate": valstartDate, "endDate": valfinishDate },
            success: function (response) {
                debugger;
                $("#workExperiencePersonDTOs_" + row + "__EmploymentPeriod").val(response)
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
     
        if (milliseconds <= 0) {
            $("#workExperiencePersonDTOs_" + row + "__EmploymentPeriod").val(null);
            $('.startDate-input label.error-label-' + row).text('تاریخ شروع نباید بزرگتر از تاریخ پایان باشد');
        }
        else {
            $('.startDate-input label.error-label-' + row).text(' ');
        }
    }
}

$(document).ready(function () {
    counterExperince = $("#WorkExperiencePersonCount").val() - 1;

    if (counterExperince == -1) {
        AddWorkExperience()
    }

    for (var index = 0; index < $("#WorkExperiencePersonCount").val(); index++) {
        initCalendar("workExperiencePersonDTOs_" + index + "__StartedAt");
        initCalendar("workExperiencePersonDTOs_" + index + "__FinishedAt");

        //console.log($("#workExperiencePersonDTOs_" + index + "__StartedAt").val());
        //SetDifference(index);

    }


    /*for (var index = 0; index < $("#WorkExperiencePersonCount").val(); index++) {
        if ($("#workExperiencePersonDTOs_" + index + "__EmploymentPeriod").val() != null)
        {
            console.log('before');
             //sleep(2000)
            setTimeout(SetDifference(index), 2000);
            //console.log('after');
        }
        
    }*/



});



function AddWorkExperience() {
    counterExperince++;
    $.ajax({
        type: "POST",
        url: "/Person/AddWorkExperience",
        data: { "countWork": counterExperince },
        success: function (response) {
            $("#InputHiddent").append('<input type="hidden" id="workExperiencePersonDTOs_' + counterExperince +
                '__Id" name ="workExperiencePersonDTOs[' + counterExperince + '].Id" class="form-control" value = "0" />');
            $("#InputHiddent").append('<input type="hidden" id="workExperiencePersonDTOs_' + counterExperince +
                '__Year" name ="workExperiencePersonDTOs[' + counterExperince + '].Year" class="form-control" />');
            $("#InputHiddent").append('<input type="hidden" id="workExperiencePersonDTOs_' + counterExperince +
                '__Month" name ="workExperiencePersonDTOs[' + counterExperince + '].Month" class="form-control" />');
            $("#InputHiddent").append('<input type="hidden" id="workExperiencePersonDTOs_' + counterExperince +
                '__Day" name ="workExperiencePersonDTOs[' + counterExperince + '].Day" class="form-control" />');
            $("#workExperience").append(response);

            if ($('table tr.attr').length <= 1 ) {
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



async function changeDateExperince(row) {

    var strStartDate = "workExperiencePersonDTOs_" + counter + "__StartedAt";
    var strFininshAt = "workExperiencePersonDTOs_" + counter + "__FinishedAt";
    var strDuration = "workExperiencePersonDTOs_" + counter + "__EmploymentPeriod";

    var valueStartDate = $("#" + strStartDate).val();
    var valueFininshAt = $("#" + strFininshAt).val();

    if (valueStartDate != '' && valueFininshAt != '') {
        await $.ajax({
            type: "POST",
            url: "/DateTime/GetCalCulateMonth",
            data: { "FromDate": valueStartDate, "ToDate": valueFininshAt },
            success: function (response) {
               // console.log(response);
                $('#' + strDuration).val(response);

            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

}
$(function () {
    $('#Experinces-form').on('submit', function () {
        // adding rules for inputs with class 'comment'
   
        $('input[name^=workExperiencePersonDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[id^=txtworkExperiencePersonDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[type="checkbox"]').each(function () {
            $(this).rules("add",
                {
                    required: false
                   
                })
        });
       
    })
    $('#Experinces-form').validate();
})