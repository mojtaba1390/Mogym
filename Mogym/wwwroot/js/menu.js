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

    $("#hasParentInPermission").on('change', function () {
        var isActiveValue = $("#hasParentInPermission").val();
        if (isActiveValue == 1) {
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