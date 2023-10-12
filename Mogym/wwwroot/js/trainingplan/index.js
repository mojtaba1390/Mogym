$(document).ready(function () {

    $('.js-select-example').select2();

    initCalendar("FromDate");
initCalendar("TillDate");

});

function fromDateFilter() {
    if ($('.txtFromDate').val().length == 0) {
        $('#FromDate').val(null);
    }
}


function toDateFilter() {
    if ($('.txtTillDate').val().length == 0) {
        $('#TillDate').val(null);
    }
}