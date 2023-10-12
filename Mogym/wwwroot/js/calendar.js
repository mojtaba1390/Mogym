function initCalendar(id) {
 /*   console.log(id);*/
    $('.txt'+id).removeAttr("type").removeAttr("Id").removeAttr("name");
    $('.txt'+id).persianDatepicker({
        observer: true,
        //format: 'dddd › DD/MMMM/YYYY',
        format: 'YYYY/MM/DD',
        altField: '#' + id,
        altFormat: 'YYYY/MM/DD',
        initialValue: !($('#' + id).val() == '' || $('#' + id).val() == null),
        autoclose: true,
        altFieldFormatter: function (unix) {


            
            $.ajax({
                type: 'POST',
                url: '/DateTime/ConvertDate',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: { persianDate: $('.txt' + id).val() },
                success: function (result) {
                    $('#' + id).val(result);
                    $('#' + id).trigger('change');
                    $('.txt' + id).trigger('change');
                },
                error: function () {

                }
            })
        },
        calendar: {
            persian: {
                locale: 'fa'
            }
        }
    });

}


function CheckClear(clearId, source)
{
if($('.'+source).val().length<6)
{
$('#'+clearId).val(null);
}
}



var items = $(".calendar-events-list");

for (var i = 0; i < 35; i++) {
    var item = items[i]
   
    if (item.children.length > 2) {
        item.children[2].remove();
        item.children[item.children.length - 1].style.display = "block"


    }
}


function asyncAjax(persianIDate) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/DateTime/ConvertDate',
            type: "GET",
            dataType: "json",
            data: { persianDate: persianIDate },
            beforeSend: function () {
            },
            success: function (data) {
                resolve(data) // Resolve promise and when success
            },
            error: function (err) {
                reject(err) // Reject the promise and go to catch()
            }
        });
    });
}






  var item = $(".calendar-events-list")[4]
 function morethan3item(){
     if (item.children.length > 2) {
         item.children[2].remove();
         $(".calendar-events-list")[4].children[$(".calendar-events-list")[4].children.length - 1].style.display = "block"

    }
}


function openDateDetail(item) {
    document.addEventListener("click", function (event) {
        var obj = document.getElementById(item);
        if (!obj.contains(event.target)) {
            document.getElementById(item).children[0].classList.remove("d-block");
        }
        else {
            document.getElementById(item).children[0].classList.add("d-block");
        }
    });

   
   
}





 
