﻿var counterCost = -1;
var counter = -1;

$(document).ready(function () {

    $('.js-select-example').select2();
    $(".js-example-theme-multiple").select2({
        theme: "classic"
    });

    counter = $("#countTrainingCourse").val() - 1;
    counterCost = $("#counterCost").val() - 1;
    addCost();

    for (var index = 0; index < $("#countTrainingCourse").val(); index++) {
        initCalendar('TrainingEmploymentCourseDTOs_' + index + '__StartDate');
        initCalendar('TrainingEmploymentCourseDTOs_' + index + '__UntilDate');
    }
    if ($("#countTrainingCourse").val() == 0) {
        trainingCourseCreate();
    }
});



$(document).ready(function () {

    $('#form-tms').on('submit', function () {
      
        // adding rules for inputs with class 'comment'
        $('input[name=Title]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('select[name=OrganizationUnitIds]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('select[name=PersonIds]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[name^=TrainingEmploymentCourseDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است",

                    }
                })
        });
        $('select[name^=TrainingEmploymentCourseDTOs]').each(function () {
            $(this).rules("add",

                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });

        $('input[name^=StartDate]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[name^=UntilDate]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
     
       /* $('input[name^=TrainingEmploymentCourseDTOs]').each(function () {
            if ($(':input[name^=TrainingEmploymentCourseDTOs]').val() <= 0 && $(':input[name^=TrainingEmploymentCourseDTOs]').val().length > 0 ) {
                $(':input[name^=TrainingEmploymentCourseDTOs]').closest('tr').find('.endDtate-td span').text('مقدار ساعت پایان نباید کوچکتر از ساعت شروع باشد')
                return false;
            } else {
                $(':input[name^=TrainingEmploymentCourseDTOs]').closest('tr').find('.endDtate-td span').text('')
                return true;
            }
        });*/


        // prevent default submit action
       
});
    // initialize the validator
    $('#form-tms').validate();

});

async  function  timeChagned(counter)
{

    var strStartTime = "TrainingEmploymentCourseDTOs_" + counter + "__StartTime";
    var strTime = "TrainingEmploymentCourseDTOs_" + counter + "__EndTime";
    var strStartDate = "TrainingEmploymentCourseDTOs_" + counter + "__StartDate";
    var strUntilDate = "TrainingEmploymentCourseDTOs_" + counter + "__UntilDate";
    var strWeekDay = "TrainingEmploymentCourseDTOs_" + counter + "__WeekDays"; 
    var strTotalDuration = "TrainingEmploymentCourseDTOs_"+counter+"__TotalDuration"

    //console.log("#" + strStartDate);
    var valueStartDate = $("#" + strStartDate).val();
    var valueUntilDate = $("#" + strUntilDate).val();

/*    console.log(valueStartDate);
    console.log(valueUntilDate);
    console.log(strWeekDay);*/

    
    
    if (valueStartDate != '' && valueUntilDate != '')
    {
        await $.ajax({
            type: "POST",
            url: "/DateTime/GetDayOfWeek",
            data: { "FromDate": valueStartDate, "ToDate": valueUntilDate },
            success: function (response) {
                //console.log(response);
                oldWeekDays = $("#" + strWeekDay).val();
                $("#" + strWeekDay).empty();
                $.each(response, function (i, item) {
                    $("#" + strWeekDay).empty();
                    $.each(response, function (i, item) {
                        $("#" + strWeekDay).append($('<option>', {
                            value: item.id,
                            text: item.title
                        }));
                    });
                });
                $("#" + strWeekDay).val(oldWeekDays);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

        var valueStartTime = $("#" + strStartTime).val();
        var valueEndTime = $("#" + strTime).val();
        var valueWeekDays = $("#" + strWeekDay).val();

        if (valueStartTime != '' && valueEndTime != '' && valueWeekDays != '')
        {

            await $.ajax({
                type: "POST",
                url: "/DateTime/GetCalCulateHour",
                data: { "FromDate": valueStartDate, "ToDate": valueUntilDate, "StartTime": valueStartTime, "EndTime": valueEndTime, "WeekDays": valueWeekDays },
                success: function (response) {
                     $("#" + strTotalDuration).val(response);
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
    if ($('#' + strTotalDuration).val().length > 1 && $('#' + strTotalDuration).val() <= 0) {
        $('#' + strTime).val(null);
    /*    $("span[name ='TrainingEmploymentCourseDTOs' + counter + '.EndTime']").text('مقدار ساعت پایان نباید کوچکتر از ساعت شروع باشد');*/
        $('#' + strTotalDuration).closest('tr').find('.endDtate-td span').text('مقدار ساعت پایان نباید کوچکتر از ساعت شروع باشد')

    } else {
        /*    $(`span[name="${strTime}"]`).text(' ');*/
        $('#' + strTotalDuration).closest('tr').find('.endDtate-td span').text(' ')
    }


}
   

function errorMessage() {
    $('.errorMessageModal').css("display", "none");
    $('.fiter-message').css("display", "none");
}

function trainingCourseCreate() {
    if ($("#OrganizationUnitId").val() != null && $("#OrganizationUnitId").val().length > 0) {
        counter++;
        $.ajax({
            type: "POST",
            url: "/TrainingEmployment/trainingCourseCreate",
            data: { "counter": counter, "unitIds": $("#OrganizationUnitId").val() },
            success: function (response) {
                $("#trainingCourse").append(response);
                $("#trainingCourseInputHiddent").append('<input type="hidden" id="TrainingEmploymentCourseDTOs_' + counter +
                    '__Id" name ="TrainingEmploymentCourseDTOs[' + counter + '].Id" class="form-control" value = "0"/>');

                if ($(".approverRowsContainer tr.attr").length <= 1) {
                    $(".approverRowsContainer .attr .remove").hide();
                } else {
                    $(".approverRowsContainer .attr .remove").show();
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

}

function chagneUnitIds(event) {
    counter = -1;
    if ($("#OrganizationUnitId").val() != 'all') {
        $.ajax({
            type: 'POST',
            url: '/Person/GetPersonsByUnitIdsWithOldPersonIds',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: { unitIds: $("#OrganizationUnitId").val() },
            success: function (result) {
                var oldValue = $("#personIds").val();

                $("#personIds").empty();
                $("#personIds").append($('<option>', {
                    value: 'all',
                    text: 'انتخاب همه'
                }));

                $.each(result, function (i, item) {
                    $("#personIds").append($('<option>', {
                        value: item.id,
                        text: item.fullName
                    }));
                });

                $("#personIds").val(oldValue);
            },
            error: function () {

            }
        })

        if ($("#OrganizationUnitId").val() != null && $("#OrganizationUnitId").val().length > 0) {
            trainingCourseCreate();
        }
        $("#trainingCourse").empty();
    }
}
function changRecruitTrainingCategory(counterCourse) {
    $.ajax({
        type: "POST",
        url: "/TrainingEmployment/GetByGetRecruitTrainingUnitIds",
        data: {
            "recruitTrainingCategoryId": $("#RecruitTrainingCategoryId" + counterCourse).val(),
            "unitIds": $("#OrganizationUnitId").val()
        },
        success: function (response) {
        
            $("#RecruitTrainingIdCreate" + counterCourse).empty();
           /* $("#RecruitTrainingIdCreate" + counterCourse).append($('<option>', {
                value: -1,
                text: 'استاد را انتخاب کنید'
            }));*/

            $.each(response, function (i, item) {
                $("#RecruitTrainingIdCreate" + counterCourse).append($('<option>', {
                    value: item.id,
                    text: item.title
                }));
            });

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function GetDayOfWeek(counterCourse)
{
   
    var strFromDate = 'TrainingEmploymentCourseDTOs_' + counterCourse + '__StartDate';
    var strUntilDate = 'TrainingEmploymentCourseDTOs_' + counterCourse + '__UntilDate';
    var strWeekDay = 'TrainingEmploymentCourseDTOs_' + counterCourse + '__WeekDays'; 
    if ($("#" + strFromDate).val() != null && $("#" + strUntilDate).val() != null)
    {
        $.ajax({
            type: "POST",
            url: "/DateTime/GetDayOfWeek",
            data: { "FromDate": ("#" + strFromDate).val(), "ToDate": ("#" + strUntilDate).val() },
            success: function (response) {

                $.each(response, function (i, item) {
                    $("#" + strWeekDay).append($('<option>', {
                        value: item.id,
                        text: item.title,
                    }));
                });

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

function addCost() {
    counterCost++;

    $.ajax({
        type: "POST",
        url: "/TrainingEmployment/AddCost",
        data: { "counter": counterCost },
        success: function (response) {
            $("#trainingCourseInputHiddent").append('<input type="hidden" id="TrainingEmploymentCostDTOs_' + counterCost +
                '__Id" name ="TrainingEmploymentCostDTOs[' + counterCost + '].Id" class="form-control" value = "0" />');
            $("#otherCost").append(response);
            if ($(".approverRowsContainer-modal tr.attr").length <= 1) {
                $(".approverRowsContainer-modal .attr .remove").hide();
            } else {
                $(".approverRowsContainer-modal .attr .remove").show();
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

function changeAcademy(counterCourse) {
  
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetByAcadmiesWithDismissed",
        data: { "acadmyIds": $("#AcademyIdCreate" + counterCourse).val() },
        success: function (response) {
          
            $("#TeacerIdCreate" + counterCourse).empty();

            $("#TeacerIdCreate" + counterCourse).append($('<option>', {
                value: -1,
                text: 'استاد را انتخاب کنید'
            }));

            $.each(response, function (i, item) {
                $("#TeacerIdCreate" + counterCourse).append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
            });

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
