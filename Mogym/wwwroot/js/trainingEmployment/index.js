$(document).ready(function () {

    $('.js-select-example').select2();

    $('#personId').change(function () {
        var personId = $('#personId').val();
        window.location = '/TrainingEmployment/index?personId=' + personId
    })
});
$(document).ready(function () {

    $('.js-select-example').select2();

    $('#UnitId').change(function () {
        var unitId = $('#UnitId').val();
        window.location = '/TrainingEmployment/index?unitId=' + unitId
    })
});


function fromDateFilter() {
    if ($('.txtfilter_FromDate').val().length == 0) {
        $('#filter_FromDate').val(null);
    }
}


function toDateFilter() {
    if ($('.txtfilter_TillDate').val().length == 0) {
        $('#filter_TillDate').val(null);
    }
}