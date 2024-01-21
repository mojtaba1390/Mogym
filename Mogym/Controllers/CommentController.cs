using System.ComponentModel;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("نظرات")]
    public class CommentController : Controller
    {
        private readonly IPlanService _planService;
        private readonly ICommentService _commentService;
        public CommentController(IPlanService planService,ICommentService commentService)
        {
            _planService = planService;
            _commentService = commentService;
        }


        public async Task<IActionResult> GetReviewForm(int planId)
        {
            try
            {
                var planComment = await _planService.GetPlanDetailsForComment(planId);

                return PartialView("_CommentForm", planComment);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = e.Message;
                return PartialView("IlegalRequest");


            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentAndRate(int planId, string review, int userRating)
        {
            try
            {
                await _commentService.AddCommentAndRate(planId, review, userRating);
            }
            catch (Exception e)
            {
                return Forbid(e.Message );
            }

            return Ok("ok");
        }


    }
}
