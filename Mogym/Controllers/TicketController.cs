using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Common;

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

            var result = await _ticketService.CreateOrGetTickets(planId);

            ViewData["TicketId"] = result.Item1;
            return View("Index",result.Item2);
        }


        public async Task<IActionResult> Send(string message, int ticketId)
        {
            try
            {
                await _ticketService.SendMessage(message, ticketId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

    }
}
