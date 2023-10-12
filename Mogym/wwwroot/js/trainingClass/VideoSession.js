
function regPlayer(event) {
    // Get the current playback duration (currentTime) of the video
    let videoPlayer = event.target;
    var Player = videojs(event.target.id)
    const currentTime = parseInt(videoPlayer.currentTime, 10);
    const duration = parseInt(videoPlayer.duration, 10);
    var currentSrcElement = Player.currentSource().src.split("/").pop();

    // Get the filename from the src attribute
 

    // Send the currentTime to the server-side using AJAX
    const data = {
        FileName: currentSrcElement,
        CurrentTime: parseInt(videoPlayer.currentTime, 10),
        TotalTime: parseInt(videoPlayer.duration, 10),
    };
  //  console.log(videoPlayer.currentTime, " / ", videoPlayer.duration);
    if (currentTime % 5 === 0) {
        $.ajax({
            url: "/Video/PlayTime/TrackPlaybackTime",
            method: "POST",
            //contentType: "application/json",
            data: data, //JSON.stringify(data),
            success: function (response) {
                // Request succeeded, do something if needed
                //console.log(response);
                //  console.log("Playback time sent to the server successfully.");
            },
            error: function () {
                // Request failed, handle the error if needed
              //  console.error("Failed to send playback time to the server.");
            }
        });
    }
};

