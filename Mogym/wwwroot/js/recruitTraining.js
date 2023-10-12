'use strick'

$(document).ready(function () {
    function initializeSelect2(selectElementObj) {
        selectElementObj.select2({
            theme: "classic"
        });
    }
    function addRowCustom() {
        var $item = $('.attr-recruit:last-child');
        var $newRow = $(' <tr class="attr-recruit form-group"><td width ="15%">\
            <div class="form-group">\
            <select class="form-select form-control" aria-label="Default select exampleFormControlSelect1">\
            <option selected>??????</option>\
            <option value="one">First</option>\
            <option value="three">Third</option>\
            </select>\
            </div>\
            </td >\
            <td width="10%"><input id="date" class="initial-value-example form-control datepick" name="date[]" type="text" /></td>\
            <td>\
            <div class="form-group">\
            <select class="js-example-theme-multiple" multiple="multiple" style="width: 75%" required>\
            <option> ???? </option>\
            <option> ?????? </option>\
            <option> ?????? </option>\
            <option> ?? ???? </option>\
            <option> ???????? </option>\
            <option> ??? ????  </option>\
            </select>\
            </div>\
            </td>\
            <td width="15%">\
            <div class="d-flex">\
            <input type="text" class="form-control" placeholder="minuts" />:\
            <input type="text" class="form-control" placeholder="hour" />\
            </div>\
            </td>\
            <td>\
            <div class="form-group">\
            <select class="form-select form-control" aria-label="Default select exampleFormControlSelect1">\
            <option selected>??????</option>\
            <option value="one">First</option>\
            <option value="three">Third</option>\
            </select>\
            </div>\
            </td>\
            <td width="10%">\
            <div class="form-group">\
            <select class="form-select form-control" aria-label="Default select exampleFormControlSelect1">\
            <option selected disabled>?????? ????</option>\
            <option value="one">??? A</option>\
            <option value="three">??? B</option>\
            <option value="three">??? C</option>\
            </select>\
            </div>\
            </td>\
            <td width="10%">\
            <button class="btn remove-recruit mx-5 text-danger tooltip" type="button"><i class="fas fa-times"></i> <span class="tooltiptext tooltip-top">??? ????</span></button>\
            <button class="btn btn-large add-recruit text-success tooltip" type="button"><i class="fas fa-plus"></i><span class="tooltiptext tooltip-top">????? ????</span></button>\
            </td>\
            </tr>');
        $item.after($newRow);
        initializeSelect2($newRow.find('.js-example-theme-multiple'));
        $('.initial-value-example').persianDatepicker({
            observer: true,
            format: 'YYYY/MM/DD',
            altField: '.observer-example-alt',
            initialValue: false,
        });
    }

    $(".approverRowsContainer").on('click', '.attr-recruit:last-child .add-recruit', function () {
        addRowCustom();
    });
    $('table').on('click', '.remove-recruit', function () {

        if ($(".attr-recruit").length == 1) {
            $(".attr-recruit .remove-recruit").hide();
        } else {
            $(this).closest('tr').remove();
        }
    })
});
/***************************** attributes 2 ****************************************/
/*$(document).ready(function () {
    
    var row = $(".attr-recruit");

    function addRow() {
        row.clone(true, true).appendTo("#attributes-recruit-training table tbody");
    }

    function removeRow(button) {
        button.closest(".attr-recruit").remove();
    }

    $('.attr-recruit:first-child').find('.attr-recruit .remove-recruit').hide();

    *//* Doc ready *//*
    $(".attr-recruit .add-recruit").on('click', function () {
        addRow();
        if ($(".attr-details").length > 1) {
            //alert("Can't remove row.");
            $(".attr-recruit .remove-recruit").show();
            select.value = '';
        }

    });
    $('table').on('click', '.remove-recruit', function () {

        if ($(".attr-recruit").length == 1) {
            $(".attr-recruit .remove-recruit").hide();
        } else {
            $(this).closest('tr').remove();
        }
    })
})*/
$(document).ready(function () {
    function initializeSelect2(selectElementObj) {
        selectElementObj.select2({
            theme: "classic"
        });
    }
    function addRowCustom() {
        var $item = $('.attr-accept:last-child');
        var $newRow = $(' <tr class="attr-accept form-group">\
            <td width = "20%" >\
            <select class="form-select form-control" aria-label="Default select exampleFormControlSelect1">\
                <option selected>??????</option>\
                <option value="one">First</option>\
                <option value="three">Third</option>\
            </select>\
            </td >\
            <td width="30%">\
                <div class="form-group">\
                    <select class="js-example-theme-multiple" multiple="multiple" style="width: 75%" required>\
                        <option> ??? 1 </option>\
                        <option> ??? 2 </option>\
                        <option> ??? 3 </option>\
                    </select>\
                </div>\
            </td>\
            <td width="10%">\
                <button class="btn remove-accept mx-5 text-danger tooltip" type="button"><i class="fas fa-times"></i> <span class="tooltiptext tooltip-top">??? ????</span></button>\
                <button class="btn btn-large add-accept text-success tooltip" type="button"><i class="fas fa-plus"></i><span class="tooltiptext tooltip-top">????? ????</span></button>\
                                            </td>\
                                        </tr > ');
        $item.after($newRow);
        initializeSelect2($newRow.find('.js-example-theme-multiple'));
    }

    $(".approverRowsContainer").on('click', '.attr-accept:last-child .add-accept', function () {
        addRowCustom();
    });
    $('table').on('click', '.remove-accept', function () {

        if ($(".attr-accept").length == 1) {
            $(".attr-accept .remove-accept").hide();
        } else {
            $(this).closest('tr').remove();
        }
    })
});
$('.js-example-theme-multiple').on("select2:select", function (e) {
    var data = e.params.data.text;
    if (data == 'all') {
        $(".js-example-theme-multiple > option").prop("selected", "selected");
        $(".js-example-theme-multiple").trigger("change");
    }
});