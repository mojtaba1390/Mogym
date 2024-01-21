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
        public CommentService(IUnitOfWork unitOfWork,
            IMapper mapper, IHttpContextAccessor accessor, IPlanService planService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
            _planService = planService;
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
