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
using Mogym.Common.ModelExtended;
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
        private readonly IEmailSender _emailSender;
        private readonly ITrainerProfileService _trainerProfileService;


        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger,IHttpContextAccessor accessor, IEmailSender emailSender, ITrainerProfileService trainerProfileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
            _emailSender = emailSender;
            _trainerProfileService = trainerProfileService;
        }
        public async Task AddQuestion(CreateQuestionRecord createQuestionRecord)
        {
            try
            {
                var userId = _accessor.GetUser();
                var entity = _mapper.Map<Question>(createQuestionRecord);
                var question = await _unitOfWork.QuestionRepository.AddAsync(entity);

                //TODO:چون قبلا بر اساس otp لاگین می شد شماره موبایل رو داشتیم ولی الان که لاگین عوض شده شماره موبایل رو دستی ست کردم.بعدا این مورد اصلاح بشه.توی auttomapper
                var user = _unitOfWork.UserRepository.GetById(userId);
                user.Mobile = createQuestionRecord.Mobile;
                await _unitOfWork.UserRepository.UpdateAsync(user);


                var plan = new Plan()
                {
                    AnserQuestionId = question.Id,
                    UserId = userId,
                    TrainerId = createQuestionRecord.TrainerId,
                    PlanStatus = EnumPlanStatus.Registered,
                    TrackingCode = Random.Shared.Next(1000,9999)
                };
                await _unitOfWork.PlanRepository.AddAsync(plan);


                var confirmAnswerQuestion = await _trainerProfileService.GetConfirmAnswerQuestion(createQuestionRecord.TrainerId, createQuestionRecord.TrainerPlanId);

               
                var message = new Message(new string[] {"ramezannia.mojtaba@gmail.com"},
                    $"پر کردن پرسشنامه-{createQuestionRecord.Mobile}",
                    $"ورزشکار عزیز;درخواست شما با کد پیگیری {plan.TrackingCode}"+
                    $" برنامه {confirmAnswerQuestion.PlanName}"+
                    $" با هزینه {confirmAnswerQuestion.PlanCost.ToString("N0")} ریال " +
                    $" برای آقا/خانم {createQuestionRecord.TrainerFullName}"+
                    $" ثبت گردید.لطفا هزینه را به شماره کارت {confirmAnswerQuestion.CartNumber}"+
                    $" به نام {confirmAnswerQuestion.CartOwner}"+
                    $"واریز کرده و تصویر رسید را در پنل پرداخت نشده خود بارگزاری نمایید - موجیم"
                );

                await _emailSender.SendEmailAsync(message);


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
