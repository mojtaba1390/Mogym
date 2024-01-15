using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private readonly ISmsService _smsService;


        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger,IHttpContextAccessor accessor, IEmailSender emailSender, ITrainerProfileService trainerProfileService, ISmsService smsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
            _emailSender = emailSender;
            _trainerProfileService = trainerProfileService;
            _smsService = smsService;
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

                var message =
                    $"ورزشکار عزیز;درخواست شما با کد پیگیری {plan.TrackingCode}" +
                    $" {confirmAnswerQuestion.PlanName}" +
                    $" با هزینه {confirmAnswerQuestion.PlanCost.ToString("N0")} ریال " +
                    $" برای آقا/خانم {confirmAnswerQuestion.CartOwner}" +
                    $" ثبت گردید.لطفا هزینه را به شماره کارت {confirmAnswerQuestion.CartNumber}" +
                    $" به نام {confirmAnswerQuestion.CartOwner}" +
                    $"واریز کرده و تصویر رسید را در پنل پرداخت نشده خود بارگزاری نمایید - موجیم ";

                var email = new Message(new string[] {"ramezannia.mojtaba@gmail.com"},
                    $"پر کردن پرسشنامه-{createQuestionRecord.Mobile}",
                message);

                await _emailSender.SendEmailAsync(email);

                await _smsService.SendSms(createQuestionRecord.Mobile, message);


            }
            catch (Exception ex)
            {
                var message = $"AddQuestion in QuestionService,obj="+JsonConvert.SerializeObject(createQuestionRecord);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<CreateAttendanceClientQuestionRecord> GetQuestionWithCode(string code)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository
                    .Find(x => x.Code == code.Trim())
                    .Include(x=>x.Plans)
                    .ThenInclude(x=>x.User_Plan)
                    .Include(x=>x.Plans)
                    .ThenInclude(x=>x.TrainerProfile_Plan)
                    .ThenInclude(x=>x.User)
                    .Include(x => x.Plans)
                    .ThenInclude(x => x.TrainerProfile_Plan)
                    .ThenInclude(x=>x.TrainerPlanCosts)
                    .FirstOrDefaultAsync();

                if (question.Plans.FirstOrDefault().PlanStatus !=
                    EnumPlanStatus.WaitForCompleteAnswerProcessByAttendanceClient)
                    throw new Exception("02");

                if (question is not null)
                    return _mapper.Map<CreateAttendanceClientQuestionRecord>(question);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public async Task UpdateQuestion(CreateAttendanceClientQuestionRecord createAttendanceClientQuestionRecord)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository.GetByIdAsync(createAttendanceClientQuestionRecord.QuestionId);

                var updateQuestion = _mapper.Map(createAttendanceClientQuestionRecord, question);
                await _unitOfWork.QuestionRepository.UpdateAsync(updateQuestion);

                var plan =await  _unitOfWork.PlanRepository.GetByIdAsync(createAttendanceClientQuestionRecord.PlanId);
                plan.PlanStatus = EnumPlanStatus.Registered;
                await _unitOfWork.PlanRepository.UpdateAsync(plan);


                var confirmAnswerQuestion = await _trainerProfileService.GetConfirmAnswerQuestion(createAttendanceClientQuestionRecord.TrainerId, createAttendanceClientQuestionRecord.TrainerPlanId);

                var message =
                    $"ورزشکار عزیز;درخواست شما با کد پیگیری {plan.TrackingCode}" +
                    $" {confirmAnswerQuestion.PlanName}" +
                    $" با هزینه {confirmAnswerQuestion.PlanCost.ToString("N0")} ریال " +
                    $" برای آقا/خانم {confirmAnswerQuestion.CartOwner}" +
                    $" ثبت گردید.لطفا هزینه را به شماره کارت {confirmAnswerQuestion.CartNumber}" +
                    $" به نام {confirmAnswerQuestion.CartOwner}" +
                    $"واریز کرده و تصویر رسید را در پنل پرداخت نشده خود بارگزاری نمایید - موجیم ";


                var email = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                    $"پر کردن پرسشنامه-{createAttendanceClientQuestionRecord.Mobile}",message
                );

                await _emailSender.SendEmailAsync(email);

                await _smsService.SendSms(createAttendanceClientQuestionRecord.Mobile, message);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
