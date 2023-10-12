'use strick'


/*console.log($('#TrainingCourseIds'))*/


var counter = 0;
var counterEvaluation = 0;
var counterTeacher = 0;
$(document).ready(function () {
    $('.js-select-example').select2();
    counterTeacher = $("#counterTeacher").val() - 1;
    //console.log(counterTeacher)
    if (counterTeacher == -1) {
        AddTrainingNeedAssessmentSuggestTeacher();
    }
    //addCost();
    //addEvaluation();
});

function AddTrainingNeedAssessmentSuggestTeacher() {
    counterTeacher++;
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/TrainingNeedAssessmentSuggestTeacher",
        data: { "counter": counterTeacher },
        success: function (response) {
            $("#InputHidden").append('<input type="hidden" id="UpdateTrainingNeedAssessmentSuggestTeacherDTOs_' + counterTeacher +
                '__Id" name ="UpdateTrainingNeedAssessmentSuggestTeacherDTOs[' + counterTeacher + '].Id" class="form-control" value = "0" />');
            $("#teaherAcadamy").append(response);
            $('.js-select-example').select2();
            if ($('.approverRowsContainer tr.attr').length <= 1) {
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
function addEvaluation()
{
    counterEvaluation++;
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/AddEvaluation",
        data: { "counter": counterEvaluation },
        success: function (response) {
            $("#evaluation").append(response);

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function changeAcademy()
{
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetByAcadmiesWithDismissed",
        data: { acadmyIds: $("#AccademyIds").val()},
        success: function (response) {
            var currentTeacherIds = $("#TeacherIds").val();
            $("#TeacherIds").empty();
            $.each(response, function (i, item) {
                $("#TeacherIds").append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
            });
            $("#TeacherIds").val(currentTeacherIds);
          
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function addCost()
{
    counter++;
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/AddCost",
        data: { "counter": counter },
        success: function (response) {
            $("#otherCost").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function chagnedepartement() {
    ///console.log($("#personIds").val());
    var old = $("#PersonIds").val();
    $.ajax({
        type: 'POST',
        url: '/Person/GetPersonsByUnitIdsWithOldPersonIds/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { unitIds: $("#OrganizationUnitId").val() },
        success: function (result) {
            $("#PersonIds").empty();
            $('#PersonIds').append($('<option>', {
                value: 'all',
                text: 'انتخاب همه'
            }));
            $.each(result, function (i, item) {
                $('#PersonIds').append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
            });
            $("#PersonIds").val(old);
        },
        error: function () {

        }
    })
}
function changeClassTypeCategory()
{
    //console.log('changeClassTypeCategory');
    $.ajax({
        type: 'POST',
        url: '/ClassType/GetWithCategory/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { categoryId: $("#CategoryId").val() },
        success: function (result) {
            $("#ClassTypeId").empty();
            $("#ClassTypeId").append($('<option>', {
                value: null,
                text: 'شیوه اجرا دوره را انتخاب کنید'
            }));
            $.each(result, function (i, item) {
                $("#ClassTypeId").append($('<option>', {
                    value: item.id,
                    text: item.title
                }));
            });
        },
        error: function () {

        }
    })
}

function chagnedepartementPartial(counterselect) {
    
   // console.log(counterselect);
   // console.log('chagnedepartementPartial');
    $.ajax({
        type: 'POST',
        url: '/TrainingNeedAssessmentRequest/GetPersonByUnitId/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { id: $("#OrganizationUnitId" + counterselect).val() },
        success: function (result) {
            //console.log(result);
            $("#approvementperson" + counterselect).empty();
          //  console.log(result);
            $("#approvementperson" + counterselect).append($('<option>', {
                value: 'all',
                text: 'انتخاب همه'
            }));

            $.each(result, function (i, item) {
                $("#approvementperson" + counterselect).append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
            });
        },
        error: function () {

        }
    })
}



function changeAcademyTeacher(row) {

    var strTeacher = "UpdateTrainingNeedAssessmentSuggestTeacherDTOs_" + row + "__PersonId";
    var strAcadamy = "UpdateTrainingNeedAssessmentSuggestTeacherDTOs_" + row + "__AcademyId";
    //console.log($("#" + strAcadamy).val());

    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetPersonByAcadmy",
        data: { accademyId: $("#" + strAcadamy).val() },
        success: function (response) {

            $("#" + strTeacher).empty();
            $.each(response, function (i, item) {
                $("#" + strTeacher).append($('<option>', {
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

$(document).ready(function () {
    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg !== value;
    }, 'کامل کردن این فیلد اجباری است.')

    $("#form-tms").validate({
        rules: {
            Title: {
                required: true,
                maxlength: 64
            },

            CategoryId: {
                valueNotEquals: "-1"
            },
            TrainingCourseTypeId: {
                valueNotEquals: "-1"
            },
            ClassTypeId: {
                valueNotEquals: "-1"
            },
            TrainingFormatId: {
                valueNotEquals: "-1"
            },
            QuarterYear: {
                valueNotEquals: "-1"
            },
            OrganizationUnitId: {
                required: true
            },
            PersonIds: {
                required: true
            },
            Cost: {
                min: 0
            },
            Reason: {
                required: true
            },
            Topic: {
                required: true
            },
            Goals: {
                required: true
            },
        },
        messages: {
            Title: {
                required: "پر کردن این فرم الزامی است",
                maxlength: "عنوان بیشتر از 64 کارکتر میباشد."
            },

            CategoryId: {
                valueNotEquals: "تکمیل کردن این فیلد الزامی است"
            },
            TrainingCourseTypeId: {
                valueNotEquals: "تکمیل کردن این فیلد الزامی است"
            },
            ClassTypeId: {
                valueNotEquals: "تکمیل کردن این فیلد الزامی است"
            },
            TrainingFormatId: {
                valueNotEquals: "تکمیل کردن این فیلد الزامی است"
            },
            QuarterYear: {
                valueNotEquals: "تکمیل کردن این فیلد الزامی است"
            },
            OrganizationUnitId: {
                required: "تکمیل کردن این فیلد الزامی است"
            },
            PersonIds: {
                required: "تکمیل کردن این فیلد الزامی است"
            },
            Cost: {
                min: "لطفا عددی بالاتر از صفر وارد کنید"

            },
            Reason: {
                required: "تکمیل کردن این فیلد الزامی است",
                maxlength: "علت نباید بیشتر از 2000 کارکتر باشد."
            },

            Topic: {
                required: "تکمیل کردن این فیلد الزامی است",
                maxlength: "سرفصل نباید بیشتر از 1000 کارکتر باشد."
            },

            Goals: {
                required: "تکمیل کردن این فیلد الزامی است",
                maxlength: "هدف نباید بیشتر از 2000 کارکتر باشد."
            },
        }
    })
})

