var counterDateTime = 0;
$(document).ready(function () {
    counterDateTime = $("#countTime").val() - 1;
    //  var countTime = $('#countTime').val();
    for (var index = 0; index < countTime; index++) {
        if ($('#TrainingClassDateTimes_' + index + '__Id').val() == -1) {
            //console.log(index);
            $('#TrainingClassDateTimes_' + index + '__Id').val(0);
        }
    }

    //console.log(new Date($('#SinceDate').val()))
    //console.log(new Date($('#UntilDate').val()))
    if (counterDateTime == -1) {
        addDateTime();
    }
    //initCalenderForm();

    for (var index = 0; index < $('#countTime').val(); index++) {
        initCalendar('TrainingClassDateTimes_' + index + '__StartDate');
    }
});



function addDateTime() {
    counterDateTime++;
    $.ajax({
        type: "POST",
        url: "/TrainingCourseClass/AddDateTime",
        data: { "counter": counterDateTime },
        success: function (response) {
            $("#otherInputHiddent").append('<input type="hidden" id="TrainingClassDateTimes_' + counterDateTime +
                '__Id" name ="TrainingClassDateTimes[' + counterDateTime + '].Id" class="form-control" value = "0" />');
            $("#datetime").append(response);
            initCalendar('TrainingClassDateTimes_' + counterDateTime + '__StartDate');
            if ($('#datetime tr.attr').length <= 1) {
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
/*$(document).ready(function () {
    $('#form-tms').on('submit', function () {
        // adding rules for inputs with class 'comment'
        $('input[name^=TrainingClassDateTimes]').each(function () {
            $(this).rules("add",
                {
                    required: true,
                    messages: {
                        required: "پر کردن این فیلد اجباری است"
                    }
                })
        });
        $('input[name$=ExternalURL]').each(function () {
            $(this).rules("add",
                {
                    required: false,

                })
        });
    });
    $('#form-tms').validate();
})*/

$(document).ready(function () {
    // Custom check function to validate the form

    $('#form-tms').submit(function (event) {
        // Prevent the default form submission behavior
        event.preventDefault();

        // Check if the form is valid before proceeding with submission

        if (isValidForm() && CheckDuration()/*&& CheckConcranci()*/) {
           // console.log('Valid');
            this.submit();
        }
        else {
            //console.log('unValid')


        }
    });



    function CheckConcranci() {

        var listStartItem = [];
        var listendItem = [];
        var result = true;
        for (var index = 0; index <= counterDateTime; index++) {
            var startDate = new Date($('#TrainingClassDateTimes_' + index + '__' + 'StartDate').val());
            var fromHour = $('#TrainingClassDateTimes_' + index + '__FromHour').val();
            var fromMinute = $('#TrainingClassDateTimes_' + index + '__FromMinute').val();
            startDate.setHours(fromHour);
            startDate.setMinutes(fromMinute);


            var TotalHour = $('#TrainingClassDateTimes_' + index + '__TotalHour').val();
            var TotalMinute = $('#TrainingClassDateTimes_' + index + '__TotalMinute').val();
            

            var endDate = new Date(startDate.getFullYear(), startDate.getMonth(), startDate.getDay(), startDate.getHours(), startDate.getMinutes(), startDate.getSeconds());
            endDate.setMinutes(endDate.getHours() + TotalHour);
            endDate.setMinutes(endDate.getMinutes() + TotalMinute);
            listStartItem.push(startDate);
            listendItem.push(endDate);
            //console.log(listStartItem);
            //console.log(listendItem);
        }

        for (var outerindex = 0; outerindex < counterDateTime; outerindex++) {
            var outerStartDate = listStartItem[outerindex];
            var outerEndtDate = listendItem[outerindex];

            for (innerIndex = 0; innerIndex < counterDateTime; innerIndex++) {

                if (outerEndtDate != innerIndex) {
                    var innerStartDate = listStartItem[innerIndex];
                    var innerEndtDate = listendItem[innerIndex];
                    if (doDurationsOverlap(outerStartDate, outerEndtDate, innerStartDate, innerEndtDate)) {
                        $('#validstartDate' + index).text('این ردیف با بقیه ردیف ها تداخل دارد');
                        result = false;
                    }
                }

            }
        }
        
        return false;
    }


    function doDurationsOverlap(start1, end1, start2, end2) {
        return start1.getTime() < end2.getTime() && start2.getTime() < end1.getTime();
    }



    function isValidForm() {
       // console.log(counterDateTime);

        var sinceDate = new Date($('#SinceDate').val());
        var untileDate = new Date($('#UntilDate').val()) ;

        var result = true;
        for (var index = 0; index <= counterDateTime; index++) {
            if ($('.txtTrainingClassDateTimes_' + index + '__' + 'StartDate').length > 0) {
                if ($('.txtTrainingClassDateTimes_' + index + '__' + 'StartDate').val().length < 2) {
                    $('#validStartDate' + index).text('زمان شروع وارد کنید');
                    result = false;
                }
                if ($('#TrainingClassDateTimes_' + index + '__TotalHour').length < 2) {
                    var hour = $('#TrainingClassDateTimes_' + index + '__TotalHour').val();
                    var minute = $('#TrainingClassDateTimes_' + index + '__TotalMinute').val();
                    if (hour * 60 + minute == 0) {
                        $('#validTotalMinute' + index).text('طول دوره اجباری می باشد');
                        $('#validTotalHour' + index).text('طول دوره اجباری می باشد');
                        result = false;
                    }
                }

                if ( $('#TrainingClassDateTimes_' + index + '__TotalHour').length > 0 && $('.txtTrainingClassDateTimes_' + index + '__' + 'StartDate').val().length > 1) {

                    var startDate = new Date($('#TrainingClassDateTimes_' + index + '__' + 'StartDate').val());
 
                    var fromHour = $('#TrainingClassDateTimes_' + index + '__FromHour').val();
                    var fromMinute = $('#TrainingClassDateTimes_' + index + '__FromMinute').val();
                    startDate.setHours(fromHour);
                    startDate.setMinutes(fromMinute);
                 
                    //console.log('startDate:' + startDate);
                    var TotalHour = $('#TrainingClassDateTimes_' + index + '__TotalHour').val();
                    var TotalMinute = $('#TrainingClassDateTimes_' + index + '__TotalMinute').val();

                    var endDate = new Date(startDate.getFullYear(), startDate.getMonth(), startDate.getDay(), startDate.getHours(), startDate.getMinutes(), startDate.getSeconds());
                    //console.log(endDate);
/*                    endDate.setHours(endDate.getHours() + TotalHour);
                    endDate.setMinutes(endDate.getMinutes() + TotalMinute);*/
                   /* console.log('startDate:' + startDate + ':' + startDate.getTime());
                    console.log('sinceDate:' + sinceDate + ':' + sinceDate.getTime());
                    console.log('untileDate:' + untileDate + ':' + untileDate.getTime());
                    console.log('endDate' + endDate + ':' + endDate.getTime())
                    console.log('startDate < sinceDate ' + startDate < sinceDate);
                    console.log('startDate > untileDate' + startDate > untileDate);*/
                    if (!(startDate.getTime() >= sinceDate.getTime() && startDate.getTime() <= untileDate.getTime())) {
                        $('#validstartDate' + index).text('زمان شروع باید در بازه مورد نظر باشد');
                        $('#faild').text('زمان شروع باید در بازه مورد نظر باشد');

                        result = false;
                    }
                   /* else
                    {
                    
                    }
                    if (endDate.getTime() > sinceDate.getTime() && endDate.getTime() < untileDate.getTime()) {
                        $('#validstartDate' + index).text('زمان شروع باید در بازه مورد نظر باشد');
                        result = false;
                        console.log('end Date invalid');
                    }*/
                    
                    

                   // console.log('result end if' + result);

                }


            }

        }


        //console.log('result' + result);
       // console.log('====================================');

        return result;
    }

    function CheckDuration()
    {
        var totalHour = parseInt($("#totalDurction").val());
        //console.log(totalHour);
        var currentTotal = 0;
        
        for (var index = 0; index <= counterDateTime; index++)
        {
            if ($('#TrainingClassDateTimes_' + index + '__TotalHour').length>0)
            {
                var totalHourRow = $('#TrainingClassDateTimes_' + index + '__TotalHour').val();
                var totalMinute = $('#TrainingClassDateTimes_' + index + '__TotalMinute').val();
                currentTotal += parseInt(totalHourRow) * 60 + parseInt(totalMinute);
            }
        }
        if (totalHour < currentTotal)
        {
            $('#faild').text('مدت زمان شروع نباید بیشتر از زمانی باشد که برنامه ریزی شد.');
        }

       // console.log(totalHour);
       // console.log(currentTotal);
       // console.log(totalHour >= currentTotal);

        return totalHour >= currentTotal;
    }

});