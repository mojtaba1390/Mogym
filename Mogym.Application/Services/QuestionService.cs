using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Question;
using Mogym.Common;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;
using Newtonsoft.Json;

namespace Mogym.Application.Services
{
    public class QuestionService:IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger,IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }
        public async Task AddQuestion(CreateQuestionRecord createQuestionRecord)
        {
            try
            {
                var userId = _accessor.GetUser();
                var entity = _mapper.Map<Question>(createQuestionRecord);
                var question = await _unitOfWork.QuestionRepository.AddAsync(entity);
                var plan = new Plan()
                {
                    AnserQuestionId = question.Id,
                    UserId = userId,
                    TrainerId = createQuestionRecord.TrainerId,
                    PlanStatus = EnumPlanStatus.Registered
                };
                await _unitOfWork.PlanRepository.AddAsync(plan);

            }
            catch (Exception ex)
            {
                var message = $"AddQuestion in QuestionService,obj="+JsonConvert.SerializeObject(createQuestionRecord);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }
    }
}
