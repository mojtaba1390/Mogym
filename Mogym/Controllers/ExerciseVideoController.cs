﻿using System.ComponentModel;
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


            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GetVideoDetails(int videoId)
        {

            var record = await _exerciseVideoService.GetEntityByIdAsync(videoId);
                return PartialView("_ExerciseDetails", record);


        }

    }
}
