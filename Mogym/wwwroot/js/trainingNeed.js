'use strick'

var counter = 0;
$(document).ready(function () {
    addCost();
});

function changeAcademy()
{
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetPersonsByAcadmies",
        data: { "acadmyIds": $("#AccademyIds").val()},
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
    //console.log($("#personIds").val());
    var old = $("#personIds").val();
    $.ajax({
        type: 'POST',
        url: '/Person/GetPersonByUnitIds/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { ids: $("#OrganizationUnitId").val() },
        success: function (result) {
            $("#personIds").empty();
            $('#personIds').append($('<option>', {
                value: 'all',
                text: 'انتخاب همه'
            }));
            $.each(result, function (i, item) {
                $('#personIds').append($('<option>', {
                    value: item.id,
                    text: item.fullName
                }));
            });
            $("#personIds").val(old);
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






