using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Common;
using Mogym.Common.ModelExtended;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IPlanService _planService;
        private readonly IEmailSender _emailSender;
        public CommentService(IUnitOfWork unitOfWork,
            IMapper mapper, IHttpContextAccessor accessor, IPlanService planService, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
            _planService = planService;
            _emailSender = emailSender;
        }
        public async Task AddCommentAndRate(int planId, string review, int userRating)
        {
            try
            {
                var currentUser = _accessor.GetUser();
                var plan = await _unitOfWork.PlanRepository.Find(x => x.Id == planId).FirstOrDefaultAsync();

                if (plan.PlanStatus != EnumPlanStatus.Sent)
                    throw new Exception("وضعیت برنامه باید ارسال شده باشد");



                var comment = new Comment()
                {
                    PlanId = planId,
                    Review = review,
                    UserId = currentUser,
                    TrainerId = plan.TrainerId,
                    Rate = userRating,
                    CommentStatus = EnumCommentStatus.UnderConsideration
                };
                await _unitOfWork.CommentRepository.AddAsync(comment);

                var email = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                    $"کامنت جدید",
                    $"کامنت");


                await _emailSender.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
