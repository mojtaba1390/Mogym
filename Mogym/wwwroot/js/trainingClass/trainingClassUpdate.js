'use strick'

function chagneUnitId() {
    /*console.log($("#OrganizationUnitId").val());*/
    $.ajax({
        type: 'POST',
        url: '/TrainingCourseClass/GetPersonByUnitId/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { ids: $("#OrganizationUnitId").val() },
        success: function (result) {
/*            console.log(result);*/
            $("#personIds").empty();
           /* console.log(result);*/
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
        },
        error: function () {

        }
    })
}
function addEvaluation() {
    counterEvaluation++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseClass/AddEvaluation",
        data: { counter: counterEvaluation },
        success: function (response) {
            $("#otherInputHiddent").append('<input type="hidden" id="UpdateTrainingNeedAssessmentRequestEvaluationDTOs' + counterEvaluation +
                '__Id" name ="UpdateTrainingNeedAssessmentRequestEvaluationDTOs[' + counterEvaluation + '].Id" class="form-control" value = "0" />');
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

function removeFile(id)
{
    $("#" + id).remove();
}

var counter = 0;
var counterEq = 0;
var counterCost = 0;
var counterEvaluation = 0;

$(document).ready(function () {
    addFile();
    initCalender();
    addDateTime();
    initCalendar("SinceDate");
    initCalendar("UntilDate");
    //checkIsOnline();
    AddEquipment();
    changeTrainingPlan();
    $('#swicth-check .end-of-class input.checkbox').change(function () {

        if (this.checked) {
            $("#input-end-class").attr('disabled', false);
            $('.end-of-class').css('text-decoration', 'none')
        }
        else {
            $("#input-end-class").attr('disabled', true);
            $('.end-of-class').css('text-decoration', 'line-through');
            $('#input-end-class').val(null);
        }
    })

    counterCost = $('#countCost').val() - 1;
    counterEvaluation = $('#countEvaluation').val() - 1
    if (counterCost == -1) {
        addCost();
    }

    if (counterEvaluation == -1) {
        addEvaluation();
    }

   
});

function addCost() {
    counterCost++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseClass/AddCost",
        data: { "counter": counterCost },
        success: function (response) {
            $("#otherInputHiddent").append('<input type="hidden" id="TrainingClassCostDTOs_' + counterCost +
                '__Id" name ="TrainingClassCostDTOs[' + counterCost + '].Id" class="form-control" value = "0" />');
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

function AddEquipment() {
    counterEq++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseClass/AddEquipment",
        data: { "counterEq": counterEq },
        success: function (response) {
            $("#addEq").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function addFile() {
    counter++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseClass/AddFile",
        data: { "counter": counter },
        success: function (response) {
            $("#files").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function checkInputOnline(id) {
   /* console.log($('#classTypeCategory').val());
    console.log(id);*/
    if (id == $('#classTypeCategory').val()) {
        $("#SinceDateBlock").css("display", "block");
        $("#UnitleDateBlock").css("display", "block");
        
        $("#swicth-check").css("display", "block");
        $("#content").css("display", "none");
        $("#dateTimeblock").css("display", "block");
    }
    else {
        $("#SinceDateBlock").css("display", "none");
        $("#UnitleDateBlock").css("display", "none");
       
        $("#swicth-check").css("display", "none");
        $("#content").css("display", "block");
        $("#dateTimeblock").css("display", "none");
    }
}

function checkIsOnline()
{
   /* console.log($('#ClassTypeId').val());*/
    $.ajax({
        type: "POST",
        url: "/ClassType/CheckOnline",
        data: { "id": $('#ClassTypeId').val() },
        success: function (response) {
           /* console.log(response);*/
            if (response == true) {
                $("#SinceDateBlock").css("display", "block");
                $("#UnitleDateBlock").css("display", "block");
                $("#swicth-check").css("display", "block");
                $("#content").css("display", "none");
                
                //online
            }
            if (response == false) {
                $("#SinceDateBlock").css("display", "none");
                $("#UnitleDateBlock").css("display", "none");
                $("#swicth-check").css("display", "none");
                $("#content").css("display", "block");
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

function changeTrainingPlan() {
    var trainingPlaneId = $("#TrainingPlanId").val();
  /*  console.log(trainingPlaneId);*/
    if (trainingPlaneId > 0) {
        $.ajax({
            type: "POST",
            url: "/TrainingNeedAssessmentRequest/GetSummaryWithTrainingPlanId",
            data: { "id": trainingPlaneId },
            success: function (response) {
              /*  console.log(response);*/
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

                $('#classTypeId').append($('<option>', {
                    value: response.classTypeDTO.id,
                    text: response.classTypeDTO.title
                }));
                $('#classTypeId').val(response.classTypeDTO.id);

              //  checkInputOnline(response.classTypeDTO.classTypeCategory);


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

function initCalender() {
    var counterTrainingPlanRow = $('#counterUpdate').val();
    for (var index = 0; index < counterTrainingPlanRow; index++) {

        initCalendar('UpdateTrainingClassDateTimeDTOs_' + index + '__StartDate');

        //$('.txtUpdateTrainingClassDateTimeDTOs_' + index + '__StartDate').removeAttr("type").removeAttr("Id").removeAttr("name");

        //$('.txtUpdateTrainingClassDateTimeDTOs_' + index + '__StartDate').persianDatepicker({
        //    observer: true,
        //    format: 'dddd › DD/MMMM/YYYY',
        //    altField: '#UpdateTrainingClassDateTimeDTOs_' + index + '__StartDate',
        //    altFormat: 'YYYY/MM/DD',
        //    initialValue: true,
        //    autoclose: true,
        //    altFieldFormatter: function (unix) {
        //        return (new Date(new persianDate(unix).valueOf())).toISOString().substring(0, 10);
        //    },
        //    calendar: {
        //        persian: {
        //            locale: 'fa'
        //        }
        //    }
        //})


    }

}
var counterDateTime = 0;
function addDateTime() {
    counterDateTime++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseClass/AddDateTime",
        data: { "counter": counterDateTime },
        success: function (response) {
            $("#datetime").append(response);
            initCalendar('startDateExecution' + counterDateTime);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}