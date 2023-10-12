// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


'use strict'

/*window.addEventListener('load', (event) => {
$('.preloader').hide();
})*/
$(document).ready(function () {

    if ($(window).width() <= 992) {
        $('body').addClass("sidebar-collapse");
    }
    $('#sidebar-overlay').click((e) => {
        $('body').removeClass('sidebar-open');
        $(this).css('display', 'none');
    });

    $('#example_filter label input').attr("placeholder", "جستجوکنید").css({ "width": "192%", "border-radius": "6px" });


    var $disabledResults = $(".js-example-disabled-results");
    $disabledResults.select2();
    /* *//* alert success*//**/
    $(".js-example-theme-multiple").select2({
        theme: "classic"
    });

    /*   var $disabledResults = $(".js-example-disabled-results");
       $disabledResults.select2();
   */

    /********* DATE PICKER ***********/

    $('.initial-value-example').persianDatepicker({
        observer: true,
        format: 'YYYY/MM/DD',
        altField: '.observer-example-alt',
        initialValue: false,
    });
    $('.initial-value-example1').persianDatepicker({
        observer: true,
        format: 'YYYY/MM/DD',
        altField: '.observer-example-alt',
        initialValue: false,
    });

    /********* SELECT2 ***********/
    $(".js-example-theme-multiple").select2({
        theme: "classic"
    });


    $('.js-example-theme-multiple').on("select2:select", function (e) {
        var data = e.params.data.text;
       // console.log(e.params.data);
      //  console.log(data);
       // console.log(e.params.data.text);
        var $this = $(this);
        if (data == 'انتخاب همه') {
            var $options = $this.find("option[value!=all]");
            $options.prop("selected", "selected");
            var $allOption = $this.find("option[value=all]");
            $allOption.prop("selected", false);
            $this.trigger("change");
        }
    });


    /********* education calendar ***********/
    /*    mobiscroll.setOptions({
            locale: mobiscroll.localeDe,
            theme: 'ios',
    
            themeVariant: 'light',
            clickToCreate: false,
            dragToCreate: false,
            dragToMove: false,
            dragToResize: false,
            eventDelete: false,
    
        });
    */
    /* $(function () {
         var calInst = $('#demo-localization').mobiscroll().eventcalendar().mobiscroll('getInst');
         calInst.setOptions({
             locale: mobiscroll.locale['fa']
         });
         $.getJSON('https://trial.mobiscroll.com/events/?vers=5&callback=?', function (events) {
             calInst.setEvents(events);
         }, 'jsonp');
 
     });*/
    window.setTimeout(function () {
        $(".alert-success ").hide();
    }, 3000);
    window.setTimeout(function () {
        $(".alert-error ").hide();
    }, 4000);


    /* change of icon sidebar */
    $('li.nav-item a.nav-link.active ').find('i.fa-circle').removeClass('fa-circle').addClass('fa-check-circle').parent().addClass('green');

    /* tooltip */
    $('[data-bs-toggle="tooltip"]').tooltip();

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })
    /* end tooltip */
    var navPosition = $('.nav_primary').offset();


    //STICKY MENU
    $(window).scroll(function () {
        var navTop = $(window).scrollTop();
        if (navPosition < navTop) {
            $('.nav_primary').addClass('fixed');
        }
        else {
            $('.nav_primary').removeClass('fixed');
        }
    });

    //TRANSITION MENU
    $('.nav_primary ul a').click(function () {
        var lienHref = $(this).attr('href');
        var positionHrefTop = $(lienHref).offset().top;
        $('.nav_primary > ul').slideUp('fast', function () {
            // Animation complete.
            $('.nav_primary > ul').removeClass('show').removeAttr('style');
        });
        $('html,body').animate({ scrollTop: positionHrefTop - 150 }, 1000);
        return false;
    });


    $('.nav_primary_btn').click(function () {
        var mobileNav = $(this).next('.nav_primary > ul');
        if ($(mobileNav).is(':hidden')) {
            $(mobileNav).slideDown('fast', function () {
                // Animation complete.
                $(mobileNav).addClass('show').removeAttr('style');
            });
        }
        else {
            $(mobileNav).slideUp('fast', function () {
                // Animation complete.
                $(mobileNav).removeClass('show').removeAttr('style');
            });
        }
    });

})
$(document).ready(function () {
    if ($("table tr.attr").length <= 1) {
        $("table .attr .remove").hide();
    } else {
        $(this).closest('tr').remove();
        //   $(this).closest('tr').css('display' , 'none');
    }

    $('table').on('click', '.remove', function () {
        if ($("tr.attr").length <= 1) {
            $(".attr .remove").hide();
        } else {
            $(this).closest('tr').remove();
            //   $(this).closest('tr').css('display' , 'none');
        }

    });

});
/*$(document).ready(function () {

let lengthp = $('table tr.attr').length;
if (lengthp <= 1) {
    $('table tr.attr').find('.remove').hide();
} else {
    $('table tr.attr').find('.remove').show();
}
})*/

function removeRow(id, idInput) {
    //console.log(id);
    debugger;
    let p = $("#" + id).parent().attr('id')
    let lengthp = $('#' + p + ' ' + 'tr.attr').length;
    //console.log(idInput);
    $("#" + id).remove();
    $("#" + idInput).val(-1);


    if (lengthp <= 2) {
        $('#' + p + ' ' + 'tr.attr').find('.remove').hide();
    } else {
        $('#' + p + ' ' + 'tr.attr').find('.remove').show();
    }
   // updateElementIds(idInput.split('_')[0]);
    //console.log(id);


}

function removeRowTest() {
   // console.log('removeRowTest');
}
$(document).ready(function () {
    $('ul.nav-pills li ul li.menu-open').parent('li').addClass('menu-open')
});


$(document).ready(function () {
    $("#confirm_password").bind('keyup change', function () {
        check_Password($("#password").val(), $("#confirm_password").val())
    })

    $("#sign_in_btn").click(function () {
        check_Password($("#password").val(), $("#confirm_password").val())
    })
})

var $password = $("#password");
var $confirmPassword = $("#confirm_password");
//Uppercase and number check variables
var upperCase = new RegExp('[A-Z]');
var numbers = new RegExp('[0-9]');

$('.validation').hide();
$('.validation1').hide();
//Password rules
function isPasswordValid() {
    //Check length and then check that password has an uppercase
    return $password.val().length > 6 &&
        $password.val().match(upperCase) &&
        $password.val().match(numbers);
}

function arePasswordsMatching() {
    return $password.val() === $confirmPassword.val();
}

//Can submit
function canSubmit() {
    return isPasswordValid() && arePasswordsMatching();
}

//Check password is over 8 characters
function passwordEvent() {

    if (isPasswordValid()) {
        $password.next().hide();
    } else {
        $password.next().show();
    }
}

//Check Passwords match
function confirmPasswordEvent() {

    if (arePasswordsMatching()) {
        $confirmPassword.next().hide();
    } else {
        $confirmPassword.next().show();
    }
}
//Enable or disablethe submit button 
function enableSubmitEvent() {
    $('input[type=submit]').prop("disabled", !canSubmit());
}

//Run passwords length check
$password.focus(passwordEvent).keyup(passwordEvent).keyup(confirmPasswordEvent).keyup(enableSubmitEvent);

//Run passwords match check
$confirmPassword.focus(confirmPasswordEvent).keyup(confirmPasswordEvent).keyup(enableSubmitEvent);


$(".toggle-password").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $($(this).attr("toggle"));
    if ($("#password").attr("type") == "password") {
        $("#password").attr("type", "text");
    } else {
        $("#password").attr("type", "password");
    }
});

$(function () {

    if ($('.reports-table tr th').length <= 4) {
        $('.reports-table tr th').addClass('rotate2')
        $('.reports-table tr th').parents('.reports-table').css('display', '-webkit-box');
        $('.reports-table tr td').css('padding', '15px 90px')
    }



})

function updateElementIds(prefix) {
    const pattern = new RegExp(`${prefix}_(\\d+)__`);
    const uniqueNumbers = [];

    const elements = document.querySelectorAll(`[id^="${prefix}_"]`);

    elements.forEach(element => {
        const match = element.id.match(pattern);

        if (match && match.length > 1) {
            const numberStr = match[1];
            const number = parseInt(numberStr, 10);

            if (!isNaN(number) && !uniqueNumbers.includes(number)) {
                uniqueNumbers.push(number);
            }
        }
    });

    // Sort the uniqueNumbers array in ascending order
    uniqueNumbers.sort((a, b) => a - b);

    const oldPrefix = `${prefix}_{uniqueNumbers[i]}`;
    const newPrefix = `${prefix}_{i}`;

    uniqueNumbers.forEach((number, i) => {
        const oldId = `${prefix}_${number}__`;
        const newId = `${prefix}_${i}__`;
        const oldName = `${prefix}[${number}].`;
        const newName = `${prefix}[${i}].`;

        const matchingElements = document.querySelectorAll('[id^="' + oldId + '"]');
        matchingElements.forEach(element => {
            element.className = element.className.replace(number, i);
            const oldOnchange = element.getAttribute("onChange");
            if (oldOnchange !== null) {
                const newOnchange = oldOnchange.replace(number, i);
                element.setAttribute("onChange", newOnchange);
            }
            const spanElement = element.nextElementSibling;
             
            if (spanElement && spanElement.tagName === "SPAN" && spanElement.name !== null && spanElement.name !== undefined) {
                spanElement.name = spanElement.name.replace(number, i);
            }
            element.id = element.id.replace(oldId, newId);
            element.name = element.name.replace(oldName, newName);
           // console.log(`Updated ID ${oldId} to ${newId} and Name ${oldName} to ${newName}`);
        });
    });
    uniqueNumbers.forEach((number, i) => {
        const oldId = `${prefix}_${number}__`;
        const newId = `${prefix}_${i}__`;
        const oldName = `${prefix}[${number}].`;
        const newName = `${prefix}[${i}].`;

        const matchingElements = document.querySelectorAll('[name^="' + oldId + '"]');
        matchingElements.forEach(element => {
            element.className = element.className.replace("_"+number+"_", "_"+i+"_");
            const oldOnchange = element.getAttribute("onChange");
            if (oldOnchange !== null) {
                const newOnchange = oldOnchange.replace(number, i);
                element.setAttribute("onChange", newOnchange);
            }
            const spanElement = element.nextElementSibling;
           // console.log('spanElement', spanElement, number, i);
            if (spanElement && spanElement.tagName === "SPAN" && spanElement.name !== null) {
                spanElement.name = spanElement.name.replace(number, i);
            }
            element.id = element.id.replace(oldId, newId);
            element.name = element.name.replace(oldName, newName);
           // console.log(`Updated ID ${oldId} to ${newId} and Name ${oldName} to ${newName}`);
        });
    });

}
const myPromise = new Promise((resolve, reject) => {
    setTimeout(() => {
        resolve("Hello from the promise!");
    }, 2000);
});