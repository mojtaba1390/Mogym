/*function separateNum(id) {
    console.log(id);
     seprate number input 3 number 
    var nStr = $("#txt" + id).val() + '';
    nStr = nStr.replace(/\,/g, "");
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    console.log(x1 + x2);
    return x1 + x2;
}*/


function preventchar(evt) {
 
    if (evt.which < 48 || evt.which > 57) {
        evt.preventDefault();
    }

}

function separateNum(id) {
    
    //console.log($("#txt" + id).val());
    var len = $("#txt" + id).val().length;
    //console.log('par' + $("#txt" + id).val().substring(0, len)); 
    if ($("#txt" + id).val() !== undefined) {
        var nStr = $("#txt" + id).val() + '';
        nStr = nStr.replace(/\,/g, "");
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        $("#txt" + id).val(x1 + x2);
        $("#" + id).val((x1 + x2).replace(/,\s?/g, ""));
      
    }
 
}
function checkMinute(id) {
    var minute = $("#" + id).val();
    
   // console.log(parseInt(minute) > 59 && parseInt(minute) < 0);
   // console.log(parseInt(minute));
    if (parseInt(minute) > 59 || parseInt(minute) < 0) {
        $("#" + id).val(0);
        $("#txt" + id).text("   مقدار دقیقه عددی بین 0 و 59 باشد");
       // console.log("in if")
    }
    else {
        $("#txt" + id).text("");
    }
}
function checkHour(id) {
    var hour = $("#" + id).val();
    if (parseInt(hour) > 23 || parseInt(hour) < 0) {
        $("#" + id).val(0);
        $("#txt" + id).text("   مقدار ساعت عددی بین 0 و 23 باشد");
    }
    else {
        $("#txt" + id).text("");
    }
}

