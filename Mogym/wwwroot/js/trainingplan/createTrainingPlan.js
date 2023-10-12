'use strick'
var counterDetial = 0;
var counterApprove = 0;
var counterRowCost = 0;
var counterRowExcution = 0;

var counter = 0;
var counterTeacher = 0;
var trainingPlanId = 0;


$(document).ready(function () {
    counterDetial = $("#totaldetail").val();
    counterRowCost = $('#counterRowCost').val() -1;
    //if (counterDetial == 0) {
    //    addDetail();
    //}
    //if (counterRowCost == -1) {
    //    addCost();
    //}
    var $lastApproverRowItem = $(".mehdi:last-child");



    $(".js-example-theme-multiple").select2({
        theme: "classic"
    });


    $(".approverRowsContainer").on('click', '.attr-accept:last-child .add-accept', function () {
        // addRowCustom();
    });
    $('table').on('click', '.remove-accept', function () {

        if ($(".attr-accept").length == 1) {
            $(".attr-accept .remove-accept").hide();
        } else {
            $(this).closest('tr').remove();
        }
    })

    if ($('#TrainingNeedAssessmentRequestId').val() > 0)
    {
        changeTrainingNeedAssement();
    }

    $('.js-select-example').select2();

    counterTeacher = $("#counterTeacher").val();
    trainingPlanId = $("#trainingPlanInsertedId").val();
    if (counterTeacher-1 == -1) {
        AddTrainingSuggestTeacher();
    }


    $("#TrainingNeedAssessmentRequestId").change(function () {
        var requestId = $('#TrainingNeedAssessmentRequestId').val();
        changeTrainingNeedAssement(requestId);
    })



    var trainingNeedAssessmentRequestIdForCreateMode = $("#TrainingNeedAssessmentRequestIdForCreateMode").val();
    changeTrainingNeedAssement(trainingNeedAssessmentRequestIdForCreateMode);




    var insertedId = $("#trainingPlanInsertedId").val();
    if (insertedId != null && insertedId != undefined) {
        $("#saveButton").hide();
        $("#Title").prop('disabled', true);
        $("#TrainingNeedAssessmentRequestId").prop('disabled', true);
        $("#TrainingCourse").prop('disabled', true);
        $(".txtFromDate").prop('disabled', true);
        $(".txtToDate").prop('disabled', true);
        $("#Hour").prop('disabled', true);
        $("#Minute").prop('disabled', true);
        $("#organizationUnitId").prop('disabled', true);
        $("#personIds").prop('disabled', true);
        $("#suggestTeacherPackageDiv").show();
    }




});




function showModalCost(packageId) {
    $.ajax({
        url: "/SuggestTeacherPackageExtraCost/GetCostBySuggestTeacherPackageId?packageId=" + packageId,
        dataType: 'html',
        success: function (data) {

            $('#modal-body').html(data);
           // $('#costModal').modal('show');
        }
    });
}

function showModalDetail(packageId) {
    $.ajax({
        url: "/SuggestTeacherPackageDetail/GetDetailBySuggestTeacherPackageId?packageId=" + packageId,
        dataType: 'html',
        success: function (data) {

            $('#modal-body2').html(data);
            //$('#detailsModal').modal('show');
        }
    });
}

function addCostPartial(rowCount) {
   
    rowCount++;
    var suggestTeacherPackageId = $("#suggestTeacherPackageId").val();
    $.ajax({
        type: "POST",
        url: "/SuggestTeacherPackageExtraCost/AddCost",
        data: { "counter": rowCount, "suggestTeacherPackageId": suggestTeacherPackageId,"mode":"add" },
        success: function (response) {
            $("#InputHidden").append('<input type="hidden" id=SuggestTeacherPackageExtraCostDTO_"' + counterRowCost +
                '__Id" name ="SuggestTeacherPackageExtraCostDTO[' + counterRowCost + '].Id" class="form-control" value = "0" />');
            $("#otherCost").append(response);

            if ($("#otherCost tr.attr").length <= 1) {
                $("table tr.attr .remove").hide();
            } else {
                $("table tr.attr .remove").show();
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


function addSuggestTeacherPackageDetail(rowCount) {
 
    rowCount++;
    var suggestTeacherPackageId = $("#suggestTeacherPackageId").val();
    $.ajax({
        type: "POST",
        url: "/SuggestTeacherPackageDetail/AddDetailForm",
        data: { "counter": rowCount, "suggestTeacherPackageId": suggestTeacherPackageId, "mode": "add" },
        success: function (response) {
            $("#InputHidden").append('<input type="hidden" id=SuggestTeacherPackageDetailDTO_"' + counterRowDetail +
                '__Id" name ="SuggestTeacherPackageDetailDTO[' + counterRowDetail + '].Id" class="form-control" value = "0" />');
            $("#otherDetail").append(response);
       
            if ($("#otherDetail tr.attr").length <= 1) {
                $("table tr.attr .remove").hide();
            } else {
                $("table tr.attr .remove").show();
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





function AddTrainingSuggestTeacher() {
    counterTeacher = $("#counterTeacher").val();
    $.ajax({
        type: "POST",
        url: "/SuggestTeacherPackage/TrainingPlanSuggestTeacherPackage",
        data: { "counter": counterTeacher, "trainingPlanId": trainingPlanId },
        success: function (response) {
            $("#InputHidden").append('<input type="hidden" id="CreateSuggestTeacherPackageDTOs_' + counterTeacher +
                '__Id" name ="CreateSuggestTeacherPackageDTOs[' + counterTeacher + '].Id" class="form-control" value = "0" />');
            $("#teaherAcadamy").append(response);
            $('.js-select-example').select2();
            initCalendar('CreateSuggestTeacherPackageDTOs_' + counterTeacher + '__FromDate');
            initCalendar('CreateSuggestTeacherPackageDTOs_' + counterTeacher + '__ToDate');
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



function changeAcademyTeacherOld(row) {

    var strTeacher = "UpdateTrainingPlanSuggestTeacherDTO_" + row + "__PersonId";
    var strAcadamy = "UpdateTrainingPlanSuggestTeacherDTO_" + row + "__AcademyId";

    $.ajax({
        type: "POST",
        url: "/TrainingPlan/GetPersonByAcadmy",
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


function changeAcademyTeacher(row) {

    var strTeacher = "CreateSuggestTeacherPackageDTOs_" + row + "__PersonId";
    var strAcadamy = "CreateSuggestTeacherPackageDTOs_" + row + "__AcademyId";

    $.ajax({
        type: "POST",
        url: "/TrainingPlan/GetPersonByAcadmy",
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






/***************************** attributes 2 ****************************************/

$(document).ready(function () {

    $(".approverRowsContainer").on('click', '.attr-details:last-child .add-details', function () {
        //addRowCustom();
    });
    $('table').on('click', '.remove-details', function () {
        if ($(".attr-details").length == 1) {
            $(".attr-details .remove-details").hide();
        } else {
            $(this).closest('tr').remove();
        }
    })
});
/***************************** attributes -modal 3 ****************************************/

$(document).ready(function () {
    $(".approverRowsContainer-modal").on('click', '.attr-details-modal:last-child .add-details-modal', function () {
        addRowCustom();
    });
    $('table').on('click', '.remove-details-modal', function () {
        if ($(".attr-details-modal").length == 1) {
            $(".attr-details-modal .remove-details-modal").hide();
        } else {
            $(this).closest('tr').remove();
        }
    })
});

function addDetail() {
    counterDetial++;
    $.ajax({
        type: "POST",
        url: "/TrainingPlan/Detail",
        data: { "counter": counterDetial },
        success: function (response) {
            $("#detils").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function separateNum(id) {

    //console.log($("#txt" + id).val());
    var len = $("#txt" + id).val().length;
    //console.log('par' + $("#txt" + id).val().substring(0, len)); 
    if ($("#txt" + id).val() !== undefined) {
        var nStr = $("#txt" + id).val() + '';
        nStr = nStr.replace(/\,/g, "");
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        $("#txt" + id).val(x1 + x2);
        $("#" + id).val((x1 + x2).replace(/,\s?/g, ""));

    }

}

function preventchar(evt) {

    if (evt.which < 48 || evt.which > 57) {
        evt.preventDefault();
    }

}

function addApprover() {
    counterApprove++;
    $.ajax({
        type: "POST",
        url: "/TrainingPlan/Approver",
        data: { "counter": counterApprove },
        success: function (response) {
            $("#approverRowsContainer").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function AddRow() {
    callApprovement(counter);
}
function chagnedepartementPartial(counterselect) {
    //console.log(counterselect);
    //console.log($("#OrganizationUnitId" + counterselect).val());
    $.ajax({
        type: 'POST',
        url: '/TrainingNeedAssessmentRequest/GetPersonByUnitId/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { id: $("#OrganizationUnitId" + counterselect).val() },
        success: function (result) {
           // console.log(result);
            $("#persons" + counterselect).empty();
            $.each(result, function (i, item) {
                $("#persons" + counterselect).append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
            });
        },
        error: function () {

        }
    })
}

function addCost() {
    counterRowCost++;
    $.ajax({
        type: "POST",
        url: "/TrainingPlan/AddCost",
        data: { "counter": counterRowCost},
        success: function (response) {
            $("#InputHidden").append('<input type="hidden" id="TrainingPlanCostDTOs_' + counterRowCost +
                '__Id" name ="TrainingPlanCostDTOs[' + counterRowCost + '].Id" class="form-control" value = "0" />');
            $("#otherCost").append(response);

            if ($("#otherCost tr.attr").length <= 1) {
                $("table tr.attr .remove").hide();
            } else {
                $("table tr.attr .remove").show();
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

function chnageRequestId() {
    var requestId = $('#TrainingNeedAssessmentRequestId').val();
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetPersonWithUnitId",
        data: { "id": requestId },
        success: function (response) {
            $("#personAndUnit").empty();
            $("#personAndUnit").append(response);
           // console.log(response);
            
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function changeTrainingNeedAssement(requestId) {

    if (requestId != null && requestId != undefined) {
        $.ajax({
            type: "POST",
            url: "/TrainingNeedAssessmentRequest/GetSummaryTrainingNeedAssement",
            data: { "id": requestId },
            success: function (response) {
                //console.log(response);

                var courseIds = [];
                //$('#trainingCourseId').empty();
                $.each(response.trainingCourseDTOs, function (i, item) {
                    //$('#trainingCourseId').append($('<option>', {
                    //    value: item.id,
                    //    text: item.title
                    //}));
                    courseIds.push(item.id);
                });
                $('#TrainingCourse').val(courseIds[0]);
                $('#TrainingCourse').trigger('change');
                var personIds = [];
                $('#personIds').empty();
                $.each(response.persons, function (i, item) {
                    $('#personIds').append($('<option>', {
                        value: item.id,
                        text: item.fullName
                    }));
                    personIds.push(item.id);
                });
                $('#personIds').val(personIds);

                var organizationIds = [];
                $('#organizationUnitId').empty();
                $.each(response.organizationUnitDTOs, function (i, item) {
                    $('#organizationUnitId').append($('<option>', {
                        value: item.id,
                        text: item.title
                    }));
                    organizationIds.push(item.id);
                });

                $('#organizationUnitId').val(organizationIds);

                $('#TrainingNeedAssessmentRequestId').empty();
                $('#TrainingNeedAssessmentRequestId').append($('<option>', {
                    value: response.trainingNeedAssessmentRequestDto.id,
                    text: response.trainingNeedAssessmentRequestDto.title
                }));
                
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
$(".js-example-theme-multiple").select2({
    theme: "classic"
});


initCalendar('FromDate');
initCalendar('ToDate');


$(function () {

    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg !== value;
    }, 'کامل کردن این فیلد اجباری است.')

    $("#form-tms").validate({
        rules: {
            Title: {
                required: true,
                maxlength: 64
            },
            FromDate: {
                required: true

            },
            ToDate: {
                required: true

            },
            Minute: {
                min: 0,
                max: 59,
                required: true
            },
            Hour: {
                min: 0,
                required: true
            },
            TrainingNeedAssessmentRequestId: {
                valueNotEquals: "-1"
            },
            TrainingCourse: {
                valueNotEquals: ""
            }
        },
        messages: {
            Title: {
                required: "پر کردن این فرم الزامی است",
                maxlength: "عنوان بیشتر از 64 کارکتر میباشد.",
            },
            TrainingNeedAssessmentRequestId: {
                valueNotEquals: "کامل کردن این فیلد الزامی است."
            },
            EventPlaceId: {
                valueNotEquals: "کامل کردن این فیلد الزامی است."
            },
            FromDate: {
                required: "کامل کردن این فیلد الزامی است."

            },
            ToDate: {
                required: "کامل کردن این فیلد الزامی است."

            },
            Minute: {
                min: "باید عددی بزرگتر از صفر باشد",
                max: "عدد باید کوچکتر از 59 باشد",
                required: "کامل کردن این فیلد الزامی است."
            },
            Hour: {
                min: "باید عددی بزرگتر از صفر باشد",
                required: "کامل کردن این فیلد الزامی است."
            },
            TrainingCourse: {
                required: "کامل کردن این فیلد الزامی است."
            }
        }
    })


    $('#SuggestTeacherPackageForm').on('submit', function () {

        // adding rules for inputs with class 'comment'
        $('input[name$=ForecastCost]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('select[name$=AcademyId]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[name$=FromDate]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[name$=ToDate]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });

    });
    $('#SuggestTeacherPackageForm').validate();




})



