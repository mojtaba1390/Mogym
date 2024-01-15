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
using Mogym.Common;

namespace Mogym.Application.Services
{
    public class TicketService: ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }
        public async Task<Tuple<int, List<TicketDetailRecord>>> CreateOrGetTickets(int planId)
        {
            var currentUserId = _accessor.GetUser();
            int ticketId = 0;
            try
            {
                var ticketForThisPlanId =
                    await _unitOfWork.TicketRepository.Where(x => x.PlanId == planId).FirstOrDefaultAsync();
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
                        CreatorLastSeen = DateTime.Now
                    };

                   var insertTicket= await _unitOfWork.TicketRepository.AddAsync(ticket);

                   ticketId = insertTicket.Id;
                }
                else
                {
                    ticketId = ticketForThisPlanId.Id;

                    if (ticketForThisPlanId.CreatorId == currentUserId)
                    {
                        ticketForThisPlanId.CreatorStatus = EnumTicketStatus.Read;
                        ticketForThisPlanId.CreatorLastSeen=DateTime.Now;
                    }
                    else
                    {
                        ticketForThisPlanId.AssignStatus = EnumTicketStatus.Read;
                        ticketForThisPlanId.AssignLastSeen = DateTime.Now;
                    }

                    await _unitOfWork.TicketRepository.UpdateAsync(ticketForThisPlanId);


                    var ticketDetails = _unitOfWork.TicketDetailRepository
                        .Find(x => x.TicketId == ticketForThisPlanId.Id)
                        .Include(x=>x.User_TiketDetail);

                    var ticketDetailRecords= _mapper.Map<List<TicketDetailRecord>>(ticketDetails);

                    return new Tuple<int, List<TicketDetailRecord>>(ticketId, ticketDetailRecords);
                }

            }
            catch (Exception ex)
            {
                var message = $"CreateOrGetTickets in TicketService,planId=" + planId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return new Tuple<int, List<TicketDetailRecord>>(ticketId, new List<TicketDetailRecord>());
        }

        public async Task SendMessage(string message, int ticketId)
        {
            var currentUserId = _accessor.GetUser();
            try
            {
                var ticket =await _unitOfWork.TicketRepository.GetByIdAsync(ticketId);
                var ticketDetail = new TicketDetail()
                {
                    Message = message,
                    TicketId = ticketId,
                    UserId = currentUserId
                };

                await _unitOfWork.TicketDetailRepository.AddAsync(ticketDetail);

                if (ticket.CreatorId == currentUserId)
                {
                    ticket.CreatorLastSeen = DateTime.Now;
                    ticket.AssignStatus = EnumTicketStatus.NotRead;
                }
                else
                {
                    ticket.AssignLastSeen = DateTime.Now;
                    ticket.CreatorStatus = EnumTicketStatus.NotRead;

                }
                await _unitOfWork.TicketRepository.UpdateAsync(ticket);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
