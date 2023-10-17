$(document).ready(function () {


    $("#form-create-menu").validate({
        rules: {
            PersianName: {
                required: true
            },
            EnglishName: {
                required: true
            },

        },
        messages: {
            PersianName: {
                required: "پر کردن نام فارسی الزامی است",
            },
            EnglishName: {
                required: "پر کردن نام انگلیسی الزامی است",
            }
        }
    });

    $("#isactive").on('change', function () {
        var isActiveValue = $("#isactive").val();
        debugger;
        if (isActiveValue == 1) {
            debugger;
            $.ajax({
                url: "/Menu/GetAllPermissionListForCreateMenuParent",
                dataType: 'html',
                success: function (data) {

                    $('#permissionList').html(data);
                }
            });
        }
    })



})