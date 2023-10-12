function radioChagne(index, correctans) {
    $('#QustionExamDTOs_'+index+'__CorrectAns').val(correctans)
}
/*function makeTimer() {

	//		var endTime = new Date("29 April 2018 9:56:00 GMT+01:00");	
	var minuts = 3;
	*//*endTime = (Date.parse(endTime) / 1000);*//*
*//*
	var now = new Date();
	now = (Date.parse(now) / 1000);

	var timeLeft = endTime - now;*//*

	let hour = 3;
	let minuts = 59;
	let second = 00
	var timeLefthour = hour - 1;
	var timeLeftminuts = minuts - 1;
	var timeLeftsecond = second - 1;

*//*	var days = Math.floor(timeLeft / 86400);*//*
*//*	var hours = Math.floor((timeLeft - (days * 86400)) / 3600);
	var minutes = Math.floor((timeLeft - (days * 86400) - (hours * 3600)) / 60);
	var seconds = Math.floor((timeLeft - (days * 86400) - (hours * 3600) - (minutes * 60)));*//*
	var hours = Math.floor((timeLefthour - (days * 86400)) / 3600);
	var minutes = Math.floor((timeLeftminuts - (days * 86400) - (hours * 3600)) / 60);
	var seconds = Math.floor((timeLeftsecond - (days * 86400) - (hours * 3600) - (minutes * 60)))

	if (hours < "10") { hours = "0" + hours; }
	if (minutes < "10") { minutes = "0" + minutes; }
	if (seconds < "10") { seconds = "0" + seconds; }

	$("#hours").html(hours );
	$("#minutes").html(minutes);
	$("#seconds").html(seconds)

}

setInterval(function () { makeTimer(); }, 1000);*/

$(document).ready(function () {

	var hour = parseInt($("#Hour").val());
	var minutes = parseInt($("#Minute").val());
	var seconds = 01;
	//console.log(hour);
	//console.log(minutes);
	var interval = setInterval(function () {
		--seconds;
		if (seconds < 0) {
			minutes--;
			seconds = 59;
			if (minutes < 0) {
				hour--;
				minutes = 59;

			}
		}
		if (seconds == 0 && minutes == 0 && hour == 0) {
			hour = 00;
			minutes = 00;
			seconds = 00;
			clearInterval(interval)

			$('#sumittbtn').click();
		}


		var timer = String(hour).padStart(2, '0') + ':' + String(minutes).padStart(2, '0') + ':' + String(seconds).padStart(2, '0');
		$('.countdown').html(timer);
	}, 1000);
});
