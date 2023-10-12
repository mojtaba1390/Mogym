'use strick'

function createCalss(id) {
    $.ajax({
        type: 'POST',
        url: '/TrainingCourseClass/CreateMeeting/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { id: id },
        success: function (result) {
           // console.log(result);
            alert('کلاس با موفقیت ایجاد شد');
        },
        error: function () {

        }
    })
}

function connectAdmin(id) {
    $.ajax({
        type: 'POST',
        url: '/TrainingCourseClass/ConnectAdminMeeting/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { id: id },
        success: function (result) {
            location.href = result;
        },
        error: function () {

        }
    })
}

function connectUser(id) {
    $.ajax({
        type: 'POST',
        url: '/TrainingCourseClass/ConnectUserMeeting/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { id: id },
        success: function (result) {
            location.href = result;
        },
        error: function () {

        }
    })
}
function disconnect(id) {
    $.ajax({
        type: 'POST',
        url: '/TrainingCourseClass/EndClass/',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { id: id },
        success: function (result) {
            if (result.returncode == 0)
            {
                alert('قطع کلاس با موفقیت انجام شد')
            }
            if (result.returncode == 1) {
                alert('کلاس موجود نمی باشد')
            }
        },
        error: function () {

        }
    })
}
