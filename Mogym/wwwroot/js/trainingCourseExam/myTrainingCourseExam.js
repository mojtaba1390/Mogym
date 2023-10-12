$(document).ready(function () {
    var countRow = $('#countRow').val();
    var currentsecond = $('#currentTime').val();
    for (var index = 0; index < countRow; index++) {
        var performSinceseconds = $('#PerformSince' + index).val();
        var untileSeconds = $('#Untile' + index).val();
        ////console.log('performSinceseconds====' + performSinceseconds);
        //console.log('untileSeconds====' + untileSeconds);
        //console.log('currentsecond====' + currentsecond);
        if (currentsecond > performSinceseconds && currentsecond < untileSeconds) {
            var interval = untileSeconds - currentsecond;
            //console.log('index' + index);
            secondInterval(index, interval);
        }
    }

    async function secondInterval(index, duration) {
        var intervalId = setInterval(function () {
            duration--;
           // console.log('index==' + index);
            //console.log('interval===' + duration);
            if (duration < 0) {
                clearInterval(intervalId);
            }


        }, 1000);
    }
})


