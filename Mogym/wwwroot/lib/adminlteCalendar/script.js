
function toggleSideBar() {
    if ($('.adminLTEeventCard').hasClass('activeSide')) {
        deactivesidebar()
    } else activesidebar()


    $('.cardToggler').toggleClass('cardtoggleractive')
}


function deactivesidebar() {
    $('.adminLTEeventCard').removeClass('activeSide')
    $('.calendarBody').animate({
        width: "100%"
    }, 500)
}

function activesidebar() {
    $('.adminLTEeventCard').addClass('activeSide')
    $('.calendarBody').animate({
        width: "75%"
    }, 200)
}

