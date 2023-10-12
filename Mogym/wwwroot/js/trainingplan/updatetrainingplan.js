
var counterDetial = 0;
var counterApprove = 0;
var counterRowCost = 0;
var counterRowExcution = 0;
var counter = 0;
var counterTeacher = 0;
var trainingPlanId = 0;

$(document).ready(function () {
    //chnageRequestId();
    changeTrainingNeedAssement();
    initCalendar("FromDate");
    initCalendar("ToDate");
    counterRowCost = $('#counterRowCost').val() - 1;
    if (counterRowCost == -1) {
        addCost();
    }

    $('#form-tms').submit(function (event) {
        // Prevent the default form submission behavior
        event.preventDefault();
        var currentValue = $('#currentStatus').val();
        var noClassValue = $('#noClassValue').val();
        // Check if the form is valid before proceeding with submission
        if (currentValue != noClassValue) {
            Swal.fire({
                title: 'برای این برنامه ریزی کلاس ثبت شده است لطفا چک کنید!',
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: 'ادامه عملیات و ذخیره',
                denyButtonText: `انصراف`,
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    this.submit();
                } else if (result.isDenied) {
                }
            })

        }
        else {
            if ($("#form-tms").valid()) {
                this.submit();
            }

        }



        //counterTeacher = $("#counterTeacher").val() - 1;
        //if (counterTeacher == -1) {
        //    AddTrainingNeedAssessmentSuggestTeacher();
        //}

    });
    $('.js-select-example').select2();
    trainingPlanId = $("#trainingPlanInsertedId").val();
    var counterTeacher = $("#counterTeacher").val();

    if (counterTeacher - 1 == -1) {
        AddTrainingSuggestTeacher();
    }



    if (counterTeacher > 0) {
        for (let i = 0; i < counterTeacher; i++) {
            initCalendar("CreateSuggestTeacherPackageDTOs_" + i + "__FromDate");
            initCalendar('CreateSuggestTeacherPackageDTOs_' + i + '__ToDate');
        }
    }

})


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


function changeAcademyTeacherOld(row) {

    var strTeacher = "UpdateTrainingPlanSuggestTeacherDTOs_" + row + "__PersonId";
    var strAcadamy = "UpdateTrainingPlanSuggestTeacherDTOs_" + row + "__AcademyId";

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


function removeSuggestTeacherPackage(id) {
    $.ajax({
        type: "POST",
        url: "/SuggestTeacherPackage/Delete",
        data: { "id": id },
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

function removeSuggestTeacherPackageExtraCost(id) {
    $.ajax({
        type: "POST",
        url: "/SuggestTeacherPackageExtraCost/Delete",
        data: { "id": id },
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

function removeSuggestTeacherPackageDetail(id) {
    $.ajax({
        type: "POST",
        url: "/SuggestTeacherPackageDetail/Delete",
        data: { "id": id },
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



function changeTrainingNeedAssement() {
    var requestId = $("#TrainingNeedAssessmentRequestIdForEditMode").val();
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetSummaryTrainingNeedAssement",
        data: { "id": requestId },
        success: function (response) {
            //console.log(response);
            var courseIds = [];
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
            $.each(response.persons, function (i, item) {
                $('#personIds').append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
                personIds.push(item.id);
            });
            $('#personIds').val(personIds);

            var organizationIds = [];
            $.each(response.organizationUnitDTOs, function (i, item) {
                $('#organizationUnitId').append($('<option>', {
                    value: item.id,
                    text: item.title
                }));
                organizationIds.push(item.id);
            });
            $('#organizationUnitId').val(organizationIds);

            $('#TrainingNeedAssessmentRequestId').append($('<option>', {
                value: response.trainingNeedAssessmentRequestDto.id,
                text: response.trainingNeedAssessmentRequestDto.title,
                selected: true
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

function addCost() {
    counterRowCost++;
    $.ajax({
        type: "POST",
        url: "/TrainingPlan/AddCost",
        data: { "counter": counterRowCost },
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


function showModalEditCost(packageId) {
    $.ajax({
        url: "/SuggestTeacherPackageExtraCost/GetCostBySuggestTeacherPackageId?packageId=" + packageId + "&mode=edit",
        dataType: 'html',
        success: function (data) {

            $('#modal-body').html(data);
            // $('#costModal').modal('show');
        }
    });
}

function showModalEditDetail(packageId) {
    $.ajax({
        url: "/SuggestTeacherPackageDetail/GetDetailBySuggestTeacherPackageId?packageId=" + packageId + "&mode=edit",
        dataType: 'html',
        success: function (data) {

            $('#modal-body2').html(data);
            //$('#detailsModal').modal('show');
        }
    });
}

function addCostPartial(rowCount) {

    var suggestTeacherPackageId = $("#suggestTeacherPackageId").val();
    $.ajax({
        type: "POST",
        url: "/SuggestTeacherPackageExtraCost/AddCost",
        data: { "counter": rowCount, "suggestTeacherPackageId": suggestTeacherPackageId },
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

    var suggestTeacherPackageId = $("#suggestTeacherPackageId").val();
    $.ajax({
        type: "POST",
        url: "/SuggestTeacherPackageDetail/AddDetailForm",
        data: { "counter": rowCount, "suggestTeacherPackageId": suggestTeacherPackageId },
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
            EventPlaceId: {
                valueNotEquals: "-1"
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
            }
        }
    })


    $('#SuggestTeacherPackageEditForm').on('submit', function () {

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
    $('#SuggestTeacherPackageEditForm').validate();




})