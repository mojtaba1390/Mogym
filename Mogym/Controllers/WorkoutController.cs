﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Workout;
using Mogym.Application.Services;
using Mogym.Domain.Entities;


namespace Mogym.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;
        private readonly IExerciseVideoService _exerciseVideoService;
        private readonly IExerciseservice _exerciseservice;
        private readonly IPlanService _planService;

        public WorkoutController(IWorkoutService workoutService, IExerciseVideoService exerciseVideoService,IExerciseservice exerciseservice, IPlanService planService)
        {
            _workoutService = workoutService;
            _exerciseVideoService = exerciseVideoService;
            _exerciseservice = exerciseservice;
            _planService = planService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //TODO : اگه وقت شد این قسمت دو تا مورد رو از هم جدا کنم. چون ویرایش با اجکس داره انجام میشه،میشه اد هم همین کارو کرد
        public async Task<IActionResult> AddWorkout(List<WorkoutRecord> workoutRecords) 
        {
            try
            {
                await _workoutService.AddOrUpdate(workoutRecords);
                return RedirectToAction("PlanDetails", "Plan", new { planId = workoutRecords.FirstOrDefault().PlanId });
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");

        }


        [HttpPost]
        public async Task<IActionResult> AddWorkoutRow(int counter, int planId)
        {
            return PartialView("_WorkoutRow",new Tuple<int,int>(counter,planId));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, string title)
        {
            try
            {
                await _workoutService.Edit(id,title);
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");
        }


        public async Task<IActionResult> WorkoutDetail(int id)
        {
            try
            {

                var workout = await _workoutService.GetByIdAsync(id);
                if (workout is null)
                {
                    TempData["errormessage"] = "تمرین مورد نظر یافت نشد";
                    return View("NotFound");
                }

                bool isThisWorkoutForThisTrainer = await _planService.IsThisPlanIdForThisTrainer(workout.PlanId);
                if (!isThisWorkoutForThisTrainer)
                {
                    TempData["errormessage"] = "برنامه درخواست شده مربوط به شما نمی باشد";
                    return View("IlegalRequest");
                }

                var workoutDetails = await _workoutService.GetWorkoutDetails(id);
                var superSets = await _workoutService.GetSuperSetExercises(id);
                var exerciseVideos = await _exerciseVideoService.GetAllExerciseVideo();
                var superSetsIds = await _exerciseservice.GetWorkoutSuperSetIds(id);


                ViewData["ExersiceVideo"] = new SelectList(exerciseVideos, "Id", "Title");
                ViewData["SuperSets"] = new SelectList(superSets, "Id", "Title");

                ViewBag.WorkoutId=id;
                ViewBag.SuperSetsIds = superSetsIds;
                ViewBag.PlanId = workout.PlanId;

                return View(workoutDetails);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id,int planId)
        {
            try
            {
                bool isThisWorkoutForThisTrainer = await _planService.IsThisPlanIdForThisTrainer(planId);
                if (!isThisWorkoutForThisTrainer)
                {
                    TempData["errormessage"] = "برنامه درخواست شده مربوط به شما نمی باشد";
                    return View("IlegalRequest");
                }

                bool isAnyExerciseExist = await _exerciseservice.IsAnyExcerciseExistByWorkoutId(id);
                return PartialView("_DeleteConfirmation",new Tuple<bool, int,int, string>(isAnyExerciseExist,id,planId,"Workout"));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int deleteId,int planId)
        {
            try
            {

                 await _workoutService.Delete(deleteId);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return RedirectToAction("PlanDetails","Plan",new{ planId =planId});
        }

        public async Task<IActionResult> SentWorkoutDetail(int planId)
        {
            try
            {
                var sentWorkoutDetail = await _workoutService.GetSentWorkoutDetail(planId);
                return View(sentWorkoutDetail);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }


        public async Task<IActionResult> PrintWorkout(int workoutId)
        {
            try
            {
                var workoutDetail = await _workoutService.GetPrintWorkoutDetail(workoutId);

                return View(workoutDetail);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }


    }
}
