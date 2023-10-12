$(document).ready(function () {
    $('#TrainingNeedAssessmentRequestId').change(function () {
        var needAssesment = $('#TrainingNeedAssessmentRequestId').val();
       // GetSuperVisor();
        window.location = '/ReportLevel2/index?assmentId=' + needAssesment
    })

    $('#OrningzationUnitId').change(function () {
        var orginzationUnitId = $('#OrningzationUnitId').val();
        var needAssesment = $('#TrainingNeedAssessmentRequestId').val();
      //console.log(orginzationUnitId);
        //GetSuperVisor();
        window.location = '/ReportLevel2/index?assmentId=' + needAssesment + '&orginzationUnitId=' + orginzationUnitId
    })
});

function GetSuperVisor()
{
    var assesmentId = $('#TrainingNeedAssessmentRequestId').val();
    //console.log(assesmentId);
        $.ajax({
            type: "POST",
            url: "/TrainingNeedAssessmentRequest/GetOrgnizations",
            data: { "TrainingNeedAssessmentRequestId": assesmentId },
            success: function (response) {
                $("#OrningzationUnitId").empty();
                $("#OrningzationUnitId").append($('<option>', {
                    value: null,
                    text: 'واحد سازمانی را انتخاب کنید'
                }));
                $.each(response, function (i, item) {
                    $("#OrningzationUnitId").append($('<option>', {
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

