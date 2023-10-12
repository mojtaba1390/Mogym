var count = 0;
var accept = null;
var notAccept = null;

$('#PersonAprover').css("display", "none");
$('#InternalAprover').css("display", "none");

/*function myFunction() {
        var popup = document.getElementById("myPopup");
        popup.classList.toggle("show");
 }*/

$(document).ready(function () {

    count = $('#count').val();
    accept = $('#Accept').val();
    notAccept = $('#NotAccept').val();
    $('#state').change(function () {
        var value = $(this).val();
        if (value == 3) {
            $('#PersonAprover').css('display', 'block');
        }
        else {
            $('#PersonAprover').css('display', 'none');
        }
        for (var index = 0; index < count; index++) {
            $("#approve" + index).val(value)
        }
        if (value == 1) {
            $('.opretaion option:selected').each(function () {
                $(".opretaion option:selected").removeAttr("selected")
                $('.opretaion option[value = 1]').prop('selected', true);
            });
        } else if (value == 2) {
            $('.opretaion option:selected').each(function () {
                $(".opretaion option:selected").removeAttr("selected")
                $('.opretaion option[value = 2]').prop('selected', true);
            })
        } else {
            $('.opretaion option:selected').each(function () {
                $(".opretaion option:selected").removeAttr("selected")
                $('.opretaion option[value = -1]').prop('selected', true);
            })
        }
    });
});


function ChagnePersonAprover() {
    var value = $('#PersonAprover').val();
        for (var index = 0; index < count; index++) {
            $("#referToId" + index).val(value)
        }
}

$(document).ready(function () {
    var isRtl = $('html').attr('lang') == 'en' ? true : false;
    $('.owl-theme').owlCarousel({
        rtl: isRtl,
        items: 3,
        loop: false,
        autoplay: false,
        margin: 16,
        nav: false,
        dots: true,
        responsive: {
            0: {
                nav: false
            },
            300: {
                items: 1.2,
                nav: false,
                autoplay: false,
            },
            500: {
                items: 3,
                nav: false,
                autoplay: false,
            },
            992: {
                items: 4,
                dots: true,
            },
            1400: {
                items: 6,
                dots: true,
            }
        }
    });


});
var topItem = '50%',
   /* rightItem = '35%';*/
    popupHeight = 139;


$(".owl-carousel .item .circle").on("click", function (e) {
    var $this = $(this).parent(),
        $parent = $this.parents(".owl-carousel-nav"),
        content = $this.find('p').text(),
        $popup = $parent.find(".popup");
        topItem = $this.offset().top - $parent.offset().top + $this.height() / 2;
       /*rightItem = $this.offset().left - $parent.offset().left + $this.width() / 2;*/ 
       $popup.css({
        top: topItem,
        /*left: rightItem,*/
      });

    $popup.html(content).stop().animate(
        {
            top: -((popupHeight - $this.parent().outerHeight()) / 2),
            //left: 0,
            width: "40%",
            height: "300px",
            opacity: 1,
            "padding":"58px 28px 1px 23px"
        },
        400
    );
    $('.glass-backgrond-full-width').css('display' , 'block')
});

$(".popup").on("click", function (e) {
    $(this).stop().animate(
        {
          
            top: topItem,
            //left: rightItem,
            opacity: 0
        },
        400
    );
    $('.glass-backgrond-full-width').css('display', 'none')
});
