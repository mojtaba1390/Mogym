function setclick(id) {
    //console.log($(`input[name=present${id}]:checked`).val());
    if ($(`input[name=present${id}]:checked`).val()) {
        $(`#AttendanceList_${id}__PresentsWithDelay`).val($(`#Delay${id}`).is(":checked"));
        $(`#AttendanceList_${id}__ExitsInClassTime`).val($(`#Exits${id}`).is(":checked"));
        $(`#AttendanceList_${id}__LeavesClassEarlier`).val($(`#Leaves${id}`).is(":checked"));
    } else {

        setvalue(id, false);
    }
    //console.log(id, $(`#Delay${id}`).is(":checked"), $(`#Exits${id}`).is(":checked"), $(`#Leaves${id}`).is(":checked"));
}

function setcheckbox(id, att, set) {
    $(`#Delay${id}`).prop(att, set);
    $(`#Exits${id}`).prop(att, set);
    $(`#Leaves${id}`).prop(att, set);
}
function setvalue(id, set) {
    $(`#AttendanceList_${id}__PresentsWithDelay`).val(set);
    $(`#AttendanceList_${id}__ExitsInClassTime`).val(set);
    $(`#AttendanceList_${id}__LeavesClassEarlier`).val(set);
}

function setpresent(id) {
    let present = $(`input[name=present${id}]:checked`).val() == 'true';
    $(`#AttendanceList_${id}__Present`).val(present);
    setcheckbox(id, "disabled", !present);
    if (!present) {
        setvalue(id, present);
        setcheckbox(id, "checked", present);
        $('#Score' + id).prop('disabled', true);
        $('#Score' + id).val(null);
        $(`#AttendanceList_${id}__Score`).val(null);
    }
    else
    {
        $('#Score' + id).removeAttr('disabled');
    }
}

function setMark(id) {
    
    var valueScore = $(`#Score${id}`).val();
    //console.log(valueScore);
    $(`#AttendanceList_${id}__Score`).val(valueScore);

}

$(function () {

    $("#example").DataTable({

        "pageLength": 25,
        "ordering": false,
        language: {
            processing: "لطفا صبر کabsentنید ...",
            search: "جستجو ",
            lengthMenu: "نمایش _MENU_ آیتم",
            info: "نمایش آیتم _START_ تا _END_ از مجموع _TOTAL_ آیتم",
            infoEmpty: "نمایش آیتم 0 تا 0 از مجموع 0 آیتم",
            infoFiltered: "(فیلتر شده از مجموع _MAX_ آیتم)",
            infoPostFix: "",
            loadingRecords: "لطفا صبر کنید ...",
            zeroRecords: "هیچ آیتمی برای نمایش وجود ندارد",
            emptyTable: "هیچ آیتمی برای نمایش وجود ندارد",
            paginate: {
                first: "اولین",
                previous: "قبلی",
                next: "بعدی",
                last: "آخرین"
            },
            aria: {
                sortAscending: ": مرتب کردن ستون به ترتیب صعودی",
                sortDescending: ": مرتب کردن ستون به ترتیب نزولی"
            }
        }
    });
});
