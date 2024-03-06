using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.ExerciseVideo;
using Mogym.Application.Records.Profile;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    [DisplayName("ویدئو تمرینات")]
    [Authorize]
    public class ExerciseVideoController : Controller
    {
        private readonly IExerciseVideoService _exerciseVideoService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExerciseVideoController(IExerciseVideoService exerciseVideoService, IWebHostEnvironment webHostEnvironment)
        {
            _exerciseVideoService = exerciseVideoService;
            _webHostEnvironment = webHostEnvironment;
        }



        [DisplayName("لیست ویدئو ها")]
        public async Task<IActionResult> Index()
        {
            var exerciseVideoRecords = await _exerciseVideoService.GetAllExerciseVideo();

            return View(exerciseVideoRecords);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExerciseVideoRecord createExerciseVideoRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createExerciseVideoRecord.ExerciseVideo != null && Path.GetExtension(createExerciseVideoRecord.ExerciseVideo.FileName).ToLower() != ".mp4")
                    {
                        TempData["errormessage"] = "فرمت ویدئو باید mp4 باشد";
                        return RedirectToAction(nameof(Create));
                    }




                    //var ffProbe = new NReco.VideoInfo.FFProbe();
                    //var videoInfo = ffProbe.GetMediaInfo(path);
                    //if (videoInfo.Duration.TotalSeconds > 20)
                    //{
                    //    ViewData["errormessage"] = "ویدئو از 20 ثانیه بیشتر نباشد";
                    //    return RedirectToAction(nameof(MyExerciseVideo));
                    //}


                    if (createExerciseVideoRecord.ExerciseVideo.Length > 5 * 1024 * 1024)
                    {
                        TempData["errormessage"] = "ویدئو از 5 مگا بایت بیشتر نباشد";
                        return RedirectToAction(nameof(Create));
                    }


                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "ExerciseVideo", createExerciseVideoRecord.ExerciseVideo.FileName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await createExerciseVideoRecord.ExerciseVideo.CopyToAsync(stream);
                        stream.Close();
                    }



                    await _exerciseVideoService.AddAsync(createExerciseVideoRecord);

                }
            }
            catch (Exception e)
            {
                TempData["errormessage"] = e.InnerException.Message.ToString();

            }


            return RedirectToAction(nameof(MyExerciseVideo));
        }


        public async Task<IActionResult> GetVideoDetails(int videoId)
        {

            var record = await _exerciseVideoService.GetEntityByIdAsync(videoId);
            return PartialView("_ExerciseDetails", record);


        }

        [DisplayName("ویدئو های من")]
        public async Task<IActionResult> MyExerciseVideo()
        {
            var exerciseVideoRecords = await _exerciseVideoService.GetMyExerciseVideo();

            return View(exerciseVideoRecords);
        }
        public async Task<IActionResult> Delete(int id)
        {
              _exerciseVideoService.Delete(id);

             return RedirectToAction(nameof(MyExerciseVideo));
        }



    }


}
