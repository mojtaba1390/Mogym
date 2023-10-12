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