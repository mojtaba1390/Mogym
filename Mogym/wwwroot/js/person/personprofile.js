'use strick'
var counter = 0;
var counterLan = -1;
var countCer = -1;
var countWork = -1;
var countSkill = -1;
var counterEducationDegree = -1;
$(document).ready(function () {
   // console.log('ready');
    counterLan = $("#counterLan").val() - 1;
    countCer = $("#countCer").val() - 1;
    countWork = $("#countWork").val() - 1;;
    counterEducationDegree = $("#countEducationDegree").val() - 1;;

    if (counterLan < 0)
        AddLan();
    if (countCer < 0)
        AddCertificate();
    if (countWork < 0)
        AddWorkExperience();
    if (countSkill < 0)
        AddSkill();
    if (counterEducationDegree < 0)
        AddEducationDegree();


    

    initCalendar("DateOfBirth");
    initCalendar("DateOfEmployeement");

    for (var index = 0; index < countCer+1; index++)
    {
        initCalendar("certificates_"+index+"__ExecutionTime");
    }

    for (var index = 0; index < countWork + 1; index++) {
        initCalendar("experinces_" + index + "__StartedAt");
    }
    for (var index = 0; index < countWork + 1; index++) {
        initCalendar("experinces_" + index + "__FinishedAt");
    }
});


function addContactInfo() {
    counter++;
    $.ajax({
        type: "POST",
        url: "/Person/ContactInfo",
        data: { "counter": counter },
        success: function (response) {
            $("#contactInfo").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function AddLan() {
    counterLan++;
    $.ajax({
        type: "POST",
        url: "/Person/AddLan",
        data: { "counterLan": counterLan },
        success: function (response) {
            $("#foreignLan").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function AddCertificate() {
    countCer++;
    $.ajax({
        type: "POST",
        url: "/Person/AddCertificate",
        data: { "countCer": countCer },
        success: function (response) {
            $("#certificate").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function AddWorkExperience() {
    countWork++;
    $.ajax({
        type: "POST",
        url: "/Person/AddWorkExperience",
        data: { "countWork": countWork },
        success: function (response) {
            $("#workExperience").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function AddSkill() {
    countSkill++;
    $.ajax({
        type: "POST",
        url: "/Person/AddSkill",
        data: { "countSkill": countSkill },
        success: function (response) {
            $("#professionalSkill").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function AddEducationDegree() {
    counterEducationDegree++;
    $.ajax({
        type: "POST",
        url: "/Person/AddEducationDegree",
        data: { "counterEducationDegree": counterEducationDegree },
        success: function (response) {
            $("#eDucationDegreePerson").append(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}



