using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TicketDetail;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using Mogym.Application.Records.Meal;
using Mogym.Domain.Entities;
using Newtonsoft.Json;
using System.Numerics;
using Mogym.Application.Records.Ticket;
using Mogym.Common;

namespace Mogym.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly ISmsService _smsService;
        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor, ISmsService smsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
            _smsService = smsService;
        }
        public async Task<int> CreateOrGetTickets(int planId)
        {
            var currentUserId = _accessor.GetUser();
            int ticketCode = 0;
            try
            {
                var ticketForThisPlanId =
                    await _unitOfWork.TicketRepository.Where(x => x.PlanId == planId)
                        .Include(x => x.User_Creator)
                        .Include(x => x.User_Assign)
                        .FirstOrDefaultAsync();


                if (ticketForThisPlanId is null)
                {
                    var plan = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                        .Include(x => x.TrainerProfile_Plan)
                        .ThenInclude(x => x.User)
                        .FirstOrDefaultAsync();

                    var athletUserId = plan.UserId;
                    var trainerUserId = plan.TrainerProfile_Plan.User.Id;


                    var assignUserId = athletUserId == currentUserId ? trainerUserId : athletUserId;

                    var ticket = new Ticket()
                    {
                        PlanId = planId,
                        CreatorId = currentUserId,
                        AssignId = assignUserId,
                        CreatorStatus = EnumTicketStatus.Creat,
                        AssignStatus = EnumTicketStatus.Creat,
                        CreatorLastSeen = DateTime.Now,
                        TicketCode = Random.Shared.Next(100000, 999999),
                        IsSentSmsToCreator = EnumYesNo.No,
                        IsSentSmsToAssign = EnumYesNo.No
                    };

                    var insertTicket = await _unitOfWork.TicketRepository.AddAsync(ticket);

                    ticketCode = insertTicket.TicketCode;
                }
                else
                    ticketCode = ticketForThisPlanId.TicketCode;




            }
            catch (Exception ex)
            {
                var message = $"CreateOrGetTickets in TicketService,planId=" + planId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return ticketCode;
        }

        public async Task SendMessage(string message, int ticketCode)
        {
            var currentUserId = _accessor.GetUser();
            try
            {
                var ticket = await _unitOfWork.TicketRepository.Where(x => x.TicketCode == ticketCode)
                    .Include(x => x.User_Creator)
                    .Include(x => x.User_Assign)
                    .FirstOrDefaultAsync();

                var ticketDetail = new TicketDetail()
                {
                    Message = message,
                    TicketId = ticket.Id,
                    UserId = currentUserId
                };

                await _unitOfWork.TicketDetailRepository.AddAsync(ticketDetail);



                if (ticket.CreatorId == currentUserId)
                {

                    if (ticket.IsSentSmsToAssign == EnumYesNo.No)
                        await _smsService.SendSms(ticket.User_Assign.Mobile,
                            $"شما ۱ گفتگو خوانده نشده جدید دارید");


                    ticket.CreatorStatus = EnumTicketStatus.Read;
                    ticket.CreatorLastSeen = DateTime.Now;
                    ticket.AssignStatus = EnumTicketStatus.NotRead;
                    ticket.IsSentSmsToAssign = EnumYesNo.Yes;
                    ticket.IsSentSmsToCreator = EnumYesNo.No;
                }
                else
                {
                    if (ticket.IsSentSmsToCreator == EnumYesNo.No)
                        await _smsService.SendSms(ticket.User_Creator.Mobile,
                            $"شما ۱ گفتگو خوانده نشده جدید دارید");

                    ticket.AssignStatus = EnumTicketStatus.Read;
                    ticket.AssignLastSeen = DateTime.Now;
                    ticket.CreatorStatus = EnumTicketStatus.NotRead;
                    ticket.IsSentSmsToCreator = EnumYesNo.Yes;
                    ticket.IsSentSmsToAssign = EnumYesNo.No;

                }

                await _unitOfWork.TicketRepository.UpdateAsync(ticket);



            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<MyTicketRecord>> MyTickets()
        {
            try
            {
                var currentUser = _accessor.GetUser();

                var myTickets = await _unitOfWork.TicketRepository
                    .Where(x => (x.CreatorId == currentUser && x.CreatorStatus != EnumTicketStatus.Read) ||
                                (x.AssignId == currentUser && x.AssignStatus != EnumTicketStatus.Read))
                    .ToListAsync();

                return _mapper.Map<List<MyTicketRecord>>(myTickets);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public async Task<List<TicketDetailRecord>> ViewTicketDetail(int ticketCode)
        {
            try
            {
                var currentUser = _accessor.GetUser();

                var ticket = await _unitOfWork.TicketRepository
                    .Where(x => x.TicketCode == ticketCode && (x.AssignId == currentUser || x.CreatorId == currentUser))
                    .Include(x => x.TicketDetails)
                    .Include(x => x.User_Creator)
                    .Include(x => x.User_Assign)
                    .FirstOrDefaultAsync();

                if (ticket is not null)
                {
                    bool isChangeTicket = false;

                    if (ticket.CreatorId == currentUser)
                    {
                        if (ticket.CreatorStatus != EnumTicketStatus.Read)
                        {
                            ticket.CreatorStatus = EnumTicketStatus.Read;
                            ticket.CreatorLastSeen = DateTime.Now;
                            ticket.IsSentSmsToCreator = EnumYesNo.No;
                            isChangeTicket = true;
                        }

                    }
                    else
                    {
                        if (ticket.AssignStatus != EnumTicketStatus.Read)
                        {
                            ticket.AssignStatus = EnumTicketStatus.Read;
                            ticket.AssignLastSeen = DateTime.Now;
                            isChangeTicket = true;
                        }

                    }

                    if (isChangeTicket)
                        await _unitOfWork.TicketRepository.UpdateAsync(ticket);


                    return _mapper.Map<List<TicketDetailRecord>>(ticket.TicketDetails);

                }



            }
            catch (Exception ex)
            {
                throw ex;

            }

            return null;
        }

        public async Task<int> GetMyUnreadTicketsCount()
        {
            var currentUser = _accessor.GetUser();

            return await _unitOfWork.TicketRepository
                .Where(x => (x.CreatorId == currentUser && x.CreatorStatus != EnumTicketStatus.Read) ||
                            (x.AssignId == currentUser && x.AssignStatus != EnumTicketStatus.Read))
                .CountAsync();

        }
    }
}
