
var counterDetial = 0;
var counterApprove = 0;
var counterRowCost = 0;
var counterRowExcution = 0;


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
        var currentStatus = parseInt($('#currentStatus').val());
        var statusNoClass = parseInt($('#statusNoClass').val());
        var statusHasClassNoSession = parseInt($('#statusHasClassNoSession').val());
        var messageHasClassNoSession = 'برای این برنامه ریزی کلاس تعیین شده است آیا از حذف کردن مطمئن هستید!';
        var messageNoClass = 'آیا از حذف کردن این برنامه آموزشی مطمئن هستید؟';
        // Check if the form is valid before proceeding with submission
        if (currentStatus == statusNoClass || currentStatus == statusHasClassNoSession) {
            Swal.fire({
                title: currentStatus == statusNoClass ? messageNoClass : messageHasClassNoSession,
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: 'ادامه عملیات و حذف',
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
        
        

    });
})

function submittButton()
{


   
}


function changeTrainingNeedAssement()
{
    var requestId = $('#TrainingNeedAssessmentRequestId').val();
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetSummaryTrainingNeedAssement",
        data: { "id": requestId },
        success: function (response) {
            //console.log(response);
            var courseIds = [];
            $.each(response.trainingCourseDTOs, function (i, item) {
                $('#trainingCourseId').append($('<option>', {
                    value: item.id,
                    text: item.title
                }));
                courseIds.push(item.id);
            });
            $('#trainingCourseId').val(courseIds);

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
            max: 59
        },
        Hour: {
            min: 0,
            max: 24
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
            max: "عدد بایذ کوچکتر از 59 باشد"
        },
        Hour: {
            min: "باید عددی بزرگتر از صفر باشد",
            max: "عدد باید کوچکتر از 24 باشد"
        }
    }
})