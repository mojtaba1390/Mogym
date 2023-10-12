
$(document).ready(function () {
  
    $('#form-tms').on('submit', function () {
        // adding rules for inputs with class 'comment'

        $('select').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('select[name^=InsertOrUpdateTrainingCourseExamQuestionDTOs]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        let durationInput = Number($('input[name=Duration]').val());
        let delayInput = Number($('input[name=Delay]').val());

        if (delayInput > durationInput) {
            $('.error-delay').text('این مقدار باید در بازه مدت دوره باشد ')
        } else {
            $('.error-delay').text('')
        }

        if (durationInput < 0) {
            $('.error-duration').text('مقدار ساعت پایان باید بزرگتر از شروع باشد')
        } else {
            $('.error-duration').text('');
        };


    });


    $("#form-tms").validate({
        rules: {
            Title: {
                required: true,
                maxlength: 500
            },
            TrainingNeedAssessmentRequestId: {
                valueNotEquals: "-1"
            },
            trainingCourseId: {
                required: true
            },
            ExamTypeId: {
                valueNotEquals: "-1"
            },
            FromMinute: {
                required: true,
                number: true,
                min: 0,
                max: 59,
            },
            FromHour: {
                required: true,
                number: true,
                min: 0,
                max: 24,
            },
            ToMinute: {
                required: true,
                number: true,
                min: 0,
                max: 59,
            },
            ToHour: {
                required: true,
                number: true,
                min: 0,
                max: 24,
            },
            PassingMark: {
                required: true,
                number: true,
                min: 0,
                max: 100
            },
            Duration: {
                number: true,
                min: 0,
            },
            Delay: {
                required: true,
                number: true,
                min: 0,
            }
        },
        messages: {
            Title: {
                required: "پر کردن این فرم الزامی است",
                maxlength: "عنوان بیشتر از 500 کارکتر میباشد.",
            },
            TrainingNeedAssessmentRequestId: {
                valueNotEquals: "کامل کردن این فرم الزامی است",
            },
            trainingCourseId: {
                required: "کامل کردن این فرم الزامی است",
            },
            ExamTypeId: {
                valueNotEquals: "کامل کردن این فرم الزامی است",
            },
            FromMinute: {
                required: "کامل کردن این فرم الزامی است",
                number: 'لطفا مقدار عددی وارد کنید',
                min: "مقدار وارد شده باید عددی بزرگتر از 1 باشد",
                max: "مقدار باید کمتر از 59 باشد",
            },
            FromHour: {
                required: "کامل کردن این فرم الزامی است",
                number: 'لطفا مقدار عددی وارد کنید',
                min: "مقدار وارد شده باید عددی بزرگتر از 1 باشد",
                max: "مقدار باید کمتر از 24 باشد",
            },
            ToMinute: {
                required: "کامل کردن این فرم الزامی است",
                number: 'لطفا مقدار عددی وارد کنید',
                min: "مقدار وارد شده باید عددی بزرگتر از 1 باشد",
                max: "مقدار باید کمتر از 59 باشد",
            },
            ToHour: {
                required: "کامل کردن این فرم الزامی است",
                number: 'لطفا مقدار عددی وارد کنید',
                min: "مقدار وارد شده باید عددی بزرگتر از 1 باشد",
                max: "مقدار باید کمتر از 24 باشد",
            }
            ,
            PassingMark: {
                required: "کامل کردن این فرم الزامی است",
                number: 'لطفا مقدار عددی وارد کنید',
                min: "عدد وارد شده کمتر از صفر نباشد.",
                max: "نمره باید کمتر از 100 باشد"
            },
            Duration: {
                number: 'لطفا مقدار عددی وارد کنید',
                min: "عدد وارد شده کمتر از صفر نباشد."
            },
            Delay: {
                required: 'حداقل وارد کردن مقدار 0 الزامی است ',
                number: 'لطفا مقدار عددی وارد کنید',
                min: "عدد وارد شده کمتر از صفر نباشد."
            }
        }
    })
})
var counter = 0;
var courseTitles = [];
$(document).ready(function () {
    counter = $('#totalQuestion').val() - 1;
    if (counter == - 1)
    {
        callQuestion();
    }
    initCalendar('PerformSince');

    $('#TrainingNeedAssessmentRequestId').change(function () {
        changeTrainingNeedAssement();
    });
    changeTrainingNeedAssement();

    $('#FromHour').change(function () {
        changeHourAndMinute();
    });

    $('#FromMinute').change(function () {
        changeHourAndMinute();
    });

    $('#ToHour').change(function () {
        changeHourAndMinute();
    });

    $('#ToMinute').change(function () {
        changeHourAndMinute();
    });

    if ($('#Duration').val() < 1) {
        changeHourAndMinute();
    }

})

function setTitleExam() {
    var length = courseTitles.length;
    var concatTile = ' آزمون ';
    //console.log(courseTitles);
    if (length == 1) {
        concatTile += courseTitles[0];
    }
    else if (length == 2) {
        concatTile += courseTitles[0] + ' و ' + courseTitles[1];
       // console.log(concatTile);
    }
    else if (length > 2) {
        $.each(courseTitles, function (i, item) {
            concatTile += item;
            if (i < length - 2) {
                concatTile += '،'
            }
            else if (i == length - 2) {
                concatTile += ' و ';
            }
        });
    }
   // console.log('courseTitles' + courseTitles)
    //console.log(concatTile)
    $('#Title').val(concatTile);
}

function callQuestion() {
    /*var trainingCouresId = $("#TrainingCourseId").val();*/
    var trainingNeedAssessmentRequestId = $("#TrainingNeedAssessmentRequestId").val();
    counter++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseExam/Question",
        data: { "counter": counter, "trainingNeedAssessmentRequestId": trainingNeedAssessmentRequestId },
        success: function (response) {
            $("#inputHidden").append('<input type="hidden" id="InsertOrUpdateTrainingCourseExamQuestionDTOs_' + counter +
                '__Id" name ="InsertOrUpdateTrainingCourseExamQuestionDTOs[' + counter + '].Id" class="form-control" value = "0" />');
            $("#sectionQuestion").append(response);
            if ($("#sectionQuestion tr.attr").length <= 1) {
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

$('table').on('click', '.remove-Exam', function () {

    if ($(".attr-Exam").length == 1) {
        $(".attr-Exam .remove-Exam").hide();
    } else {
        $(this).closest('tr').remove();
    }
})

function changeTrainingNeedAssement() {
    var requestId = $('#TrainingNeedAssessmentRequestId').val();
    $.ajax({
        type: "POST",
        url: "/TrainingNeedAssessmentRequest/GetSummaryTrainingNeedAssement",
        data: { "id": requestId },
        success: function (response) {
           // console.log(response);
            var courseIds = [];
            $('#trainingCourseId').empty();
            courseTitles = [];
            $.each(response.trainingCourseDTOs, function (i, item) {
                courseTitles.push(item.title);
                $('#trainingCourseId').append($('<option>', {
                    value: item.id,
                    text: item.title
                }));
                courseIds.push(item.id);
            });
            $('#trainingCourseId').val(courseIds);
            
            setTitleExam();
           

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}



function changeHourAndMinute() {
    var fromHour = $('#FromHour').val();
    var fromMinute = $('#FromMinute').val();
    var toHour = $('#ToHour').val();
    var toMinute = $('#ToMinute').val();

    if (fromHour > -1 && fromMinute > -1 && toHour > -1 && toMinute > -1) {
        fromHour = parseInt(fromHour);
        fromMinute = parseInt(fromMinute);
        toHour = parseInt(toHour);
        toMinute = parseInt(toMinute);
        var duration = ((toHour * 60) + toMinute) - ((fromHour * 60) + fromMinute);
        $('#Duration').val(duration);
    }
}

