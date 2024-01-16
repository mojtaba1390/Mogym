using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Common;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("گفتگو")]
    public class TicketController : Controller
    {
        private readonly IPlanService _planService;
        private readonly ITicketService _ticketService;
        public TicketController(IPlanService planService,ITicketService ticketService)
        {
            _planService = planService;
            _ticketService = ticketService;
        }
        [DisplayName("لیست گفتگو")]
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> PlanTicket(int planId)
        {
            bool isThisPlanStatusIsSent =
                await _planService.IsThereAnyPlanWithStatus(planId, EnumPlanStatus.Sent);
            if (!isThisPlanStatusIsSent)
            {
                TempData["errormessage"] = "وضعیت برنامه درخواست شده ارسال شده مربی نمی باشد";
                return View("IlegalRequest");
            }

            var code=await _ticketService.CreateOrGetTickets(planId);

            return RedirectToAction(nameof(TicketDetail),new{ticketCode=code});
        }


        public async Task<IActionResult> Send(string message, int code)
        {
            try
            {
                await _ticketService.SendMessage(message, code);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction(nameof(TicketDetail), new { ticketCode = code });
        }


        [DisplayName("گفتگوهای من")]
        public async Task<IActionResult> MyTickets()
        {

            var tickets = await _ticketService.MyTickets();
            return View(tickets);
        }


        public async Task<IActionResult> TicketDetail(int ticketCode)
        {
            try
            {
                var ticketDetails = await _ticketService.ViewTicketDetail(ticketCode);

                if (ticketDetails is null)
                {
                    TempData["errormessage"] = "یا کد تیکت اشتباه می باشد و یا این تیکت مربوط به شما نیست";
                    return View("IlegalRequest");
                }

                ViewData["TicketCode"] = ticketCode;
                return View(ticketDetails);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

    }
}
