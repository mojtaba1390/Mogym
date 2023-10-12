var counter = 0;
var counterCost = 0;
$(document).ready(function () {
    $('.js-select-example').select2();
    var counterrow = $("#counterrow").val();
    counter = $('#counterrow').val() - 1;
    if (counterrow < 1) {
        trainingCourseCreate();
    }
    for (var index = 0; index < counterrow; index++) {
        initCalendar('TrainingEmploymentCourseDTOs_' + index + '__StartDate');
        initCalendar('TrainingEmploymentCourseDTOs_' + index + '__UntilDate');
    }
    counterCost = $('#counterCostRow').val() -1;
    addCost();
})
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
                        required: "پر کردن این فیلد اجباری است"
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

        $('input[name^=TrainingEmploymentCourseDTOs]').each(function () {
            if ($(':input[name^=TrainingEmploymentCourseDTOs]').val() <= 0 && $(':input[name^=TrainingEmploymentCourseDTOs]').val().length > 0) {
                $(':input[name^=TrainingEmploymentCourseDTOs]').closest('tr').find('.endDtate-td span').text('مقدار ساعت پایان نباید کوچکتر از ساعت شروع باشد')
                return false;
            } else {
                $(':input[name^=TrainingEmploymentCourseDTOs]').closest('tr').find('.endDtate-td span').text('')
                return true;
            }
        });
        // prevent default submit action         
    });
  
    // initialize the validator
    $('#form-tms').validate();

});
function errorMessage() {
    $('.errorMessageModal').css("display", "none");
    $('.fiter-message').css("display", "none");
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
            //console.log(response);
            $("#RecruitTrainingIdCreate" + counterCourse).empty();
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

function changeAcademyEdit(counterCourse) {
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetPersonsByAcadmies",
        data: { "acadmyIds": $("#AcademyId" + counterCourse).val() },
        success: function (response) {
         /*   console.log(response);*/
            $("#TeacerId" + counterCourse).empty();

            $("#TeacerId" + counterCourse).append($('<option>', {
                value: -1,
                text: 'استاد را انتخاب کنید'
            }));

            $.each(response, function (i, item) {
                $("#TeacerId" + counterCourse).append($('<option>', {
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

function trainingCourseCreate() {

    if ($("#OrganizationUnitId").val() != null && $("#OrganizationUnitId").val().length > 0) {
        counter++;
        $.ajax({
            type: "POST",
            url: "/TrainingEmployment/trainingCourseCreate",
            data: { "counter": counter, "unitIds": $("#OrganizationUnitId").val() },
            success: function (response) {
                $("#trainingCourse").append(response);

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


async function timeChagned(counter) {

    var strStartTime = "TrainingEmploymentCourseDTOs_" + counter + "__StartTime";
    var strTime = "TrainingEmploymentCourseDTOs_" + counter + "__EndTime";
    var strStartDate = "TrainingEmploymentCourseDTOs_" + counter + "__StartDate";
    var strUntilDate = "TrainingEmploymentCourseDTOs_" + counter + "__UntilDate";
    var strWeekDay = "TrainingEmploymentCourseDTOs_" + counter + "__WeekDays";
    var strTotalDuration = "TrainingEmploymentCourseDTOs_" + counter + "__TotalDuration"

/*    console.log("#" + strStartDate);*/
    var valueStartDate = $("#" + strStartDate).val();
    var valueUntilDate = $("#" + strUntilDate).val();
/*    console.log(valueStartDate);
    console.log(valueUntilDate);
    console.log(strWeekDay);*/


    if (valueStartDate != '' && valueUntilDate != '') {
        await $.ajax({
            type: "POST",
            url: "/DateTime/GetDayOfWeek",
            data: { "FromDate": valueStartDate, "ToDate": valueUntilDate },
            success: function (response) {
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

        if (valueStartTime != '' && valueEndTime != '' && valueWeekDays != '') {

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
function chagneUnitIds() {
    $.ajax({
        type: 'POST',
        url: '/Person/GetPersonsByUnitIdsWithOldPersonIds/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { unitIds: $("#OrganizationUnitId").val(), personIds: $("#personIds").val() },
        success: function (result) {
            var oldValue = $("#personIds").val();
            //console.log(result);
            $("#personIds").empty();
            //console.log(result);
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
   // console.log(counterCourse);
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetByAcadmiesWithDismissed",
        data: { "acadmyIds": $("#AcademyIdCreate" + counterCourse).val() },
        success: function (response) {
            //console.log(response);
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