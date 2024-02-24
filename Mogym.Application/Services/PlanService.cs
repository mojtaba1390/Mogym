using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Question;
using Mogym.Application.Records.User;
using Mogym.Application.Records.UserRole;
using Mogym.Application.Records.Workout;
using Mogym.Common;
using Mogym.Common.ModelExtended;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class PlanService:IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly ITrainerProfileService _trainerProfileService;
        private readonly IEmailSender _emailSender;
        private readonly ISmsService _smsService;
        

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger,IHttpContextAccessor accessor, ITrainerProfileService trainerProfileService, IEmailSender emailSender,ISmsService smsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
            _trainerProfileService = trainerProfileService;
            _emailSender = emailSender;
            _smsService = smsService;
        }

        public async Task<List<PlanRecord>?> MyUnPaidPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId && x.PlanStatus==EnumPlanStatus.Registered)
                    .AsNoTracking()
                    .Include(x=>x.TrainerProfile_Plan)
                    .ThenInclude(x=>x.User)
                    .OrderByDescending(x=>x.Id)
                    .ToListAsync();
                return _mapper.Map<List<PlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetMyPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

        }

        public async Task UpdatePaidPicture(int planId,string paidPictureFileName)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
                plan.PaidPicture = paidPictureFileName;
                plan.PlanStatus = EnumPlanStatus.WaitForApprovePaidPic;
                await _unitOfWork.PlanRepository.UpdateAsync(plan);

                //var trainer = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                //    .Include(x => x.TrainerProfile_Plan)
                //    .ThenInclude(x => x.User)
                //    .Select(x=>x.TrainerProfile_Plan.User)
                //    .FirstOrDefaultAsync();


                var user = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                    .Include(x => x.User_Plan)
                    .Select(x=>x.User_Plan)
                    .FirstOrDefaultAsync();
                    
                var message = $"تصویر رسید برنامه با کد پیگیری {plan.TrackingCode} در حال پردازش سیستم می باشد.نتیجه از طریق پیامک اعلام می گردد. موجیم";

                var email = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                    $"شناسایی تصویر رسید-{user.Mobile}",
                    message);   


               await  _emailSender.SendEmailAsync(email);


               await _smsService.SendOTP(user.Mobile, plan.TrackingCode.ToString(), "UpdatePaidPicture");


            }
            catch (Exception ex)
            {
                var message = $"UpdatePaidPicture in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<bool> IsThisPlanIdForThisCurrentUser(int planId)
        {
            var userId= _accessor.GetUser();
            return await _unitOfWork.PlanRepository.Find(x => x.UserId == userId && planId == planId).AnyAsync();
        }
        
        public async Task<AnswerQuestionRecord> GetAnswerQuestionWithPlanId(int planId)
        {
            //TODO: اینجا یه ایراد طراحی اتفاق افتاده و اونم اینه که نوع برنامه در کواسچن مشخص شده و هیچ رابطه ای بین اون و هزینه مربی وجود نداره در صورتی که باید این رابطه در پلن تعریف می ش.دو تا کار میشه کرد که در جدول پلن بجای ترینر با هزینه ها رابطه زد و از روی اون به مربی رسید یا هر دو رو داشته باشیم
            try
            {
                var userId = _accessor.GetUser();
                var trainerId =   _trainerProfileService.GetCurrentUserTrainer().Result?.Id;
                var plan =  _unitOfWork.PlanRepository.Find(x => x.Id == planId && (x.UserId==userId || x.TrainerId== trainerId))
                    .AsNoTracking()
                    .Include(x => x.AnsweQuestion_Plan).FirstOrDefault();
                if (plan is not null)
                {
                    var answerQuestion = plan.AnsweQuestion_Plan;

                    var aq = _mapper.Map<AnswerQuestionRecord>(answerQuestion);

                    var user = _unitOfWork.UserRepository.GetById(plan.UserId);
                    aq.Mobile = user.Mobile;


                    var cost = await _unitOfWork.TrainerPlanCostRepository.GetByIdAsync(answerQuestion.TrainerPlan);
                    aq.TrainerPlan = cost.TrainerPlan.GetEnumDescription() + "-" + cost.OriginalCost.Value.ToString("N0") + " ریال ";





                    return aq;
                }


            }
            catch (Exception ex)
            {
                var message = $"GetAnswerQuestionWithPlanId in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }

        public async Task<List<PaidPlanRecorrd>?> GetPaidPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var trainer = await _trainerProfileService.GetCurrentUserTrainer();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.TrainerId == trainer.Id && x.PlanStatus==EnumPlanStatus.Paid)
                    .AsNoTracking()
                    .Include(x => x.User_Plan)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return _mapper.Map<List<PaidPlanRecorrd>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetPaidPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }


        public async Task<List<PaidPlanRecorrd>?> CheckPaidPic()
        {
            try
            {
                var userId = _accessor.GetUser();
                var trainer = await _trainerProfileService.GetCurrentUserTrainer();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.PlanStatus==EnumPlanStatus.WaitForApprovePaidPic)
                    .AsNoTracking()
                    .Include(x => x.User_Plan)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return _mapper.Map<List<PaidPlanRecorrd>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetPaidPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task ApprovePic(int planId)
        {
            var plan = await _unitOfWork.PlanRepository.Where(x => x.Id == planId).FirstOrDefaultAsync();
            plan.PlanStatus = EnumPlanStatus.Paid;
            await _unitOfWork.PlanRepository.UpdateAsync(plan);

            var trainer = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                .Include(x => x.TrainerProfile_Plan)
                .ThenInclude(x => x.User)
                .Select(x => x.TrainerProfile_Plan.User)
                .FirstOrDefaultAsync();


            //var messageTrainer =
            //    $"مربی عزیز,برنامه ای با کد پیگیری {plan.TrackingCode} در منو پرداخت شده شما اضافه شد - موجیم ";

            //await _smsService.SendSms(trainer.Mobile, messageTrainer);

            //var messageTrainer = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
            //    $"تائید تصویر رسید مربی-{trainer.Mobile}",
            //    );

            // await _emailSender.SendEmailAsync(messageTrainer);


            await _smsService.SendOTP(trainer.Mobile, plan.TrackingCode.ToString(), "ApprovePicTrainer");

            var user = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                .Include(x => x.User_Plan)
                .Select(x => x.User_Plan)
                .FirstOrDefaultAsync();


            //var messageUser = $"تصویر رسید بارگزاری شده برنامه با کد پیگیری {plan.TrackingCode} شما تائید شد- موجیم";

            await _smsService.SendOTP(user.Mobile, plan.TrackingCode.ToString(), "ApprovePicUser");

            //var messageUser = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
            //    $"شاگرد تائید تصویر رسید-{user.Mobile}",
            //    $"تصویر رسید بارگزاری شده برنامه با کد پیگیری {plan.TrackingCode} شما تائید شد- موجیم");



            //await _emailSender.SendEmailAsync(messageUser);



        }

        public async Task IgnorePic(int planId)
        {
            var plan = await _unitOfWork.PlanRepository.Where(x => x.Id == planId).FirstOrDefaultAsync();
            plan.PlanStatus = EnumPlanStatus.Registered;
            await _unitOfWork.PlanRepository.UpdateAsync(plan);


            var user = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                .Include(x => x.User_Plan)
                .Select(x => x.User_Plan)
                .FirstOrDefaultAsync();


            //var messageUser = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
            //    $"عدم تائید تصویر رسید-{user.Mobile}",
            //    $"تصویر رسید برنامه با کد پیگیری {plan.TrackingCode} به یکی از دلایل تصویر نامتعارف،تصویر خالی،مشخص نبودن شماره پیگیری،تصویر مخدوش نامعتبر شناخته شد.لطفا در پنل پرداخت نشده تصویر مناسب بارگزاری نمایید.موجیم");



            //await _emailSender.SendEmailAsync(messageUser);

           // var msg = $"تصویر رسید برنامه با کد پیگیری {plan.TrackingCode} به یکی از دلایل تصویر نامتعارف،تصویر خالی،مشخص نبودن شماره پیگیری،تصویر مخدوش نامعتبر شناخته شد.لطفا در پنل پرداخت نشده تصویر مناسب بارگزاری نمایید.موجیم";


            //await _smsService.SendSms(user.Mobile, msg);
            await _smsService.SendOTP(user.Mobile, plan.TrackingCode.ToString(), "IgnorePic");



        }

        public async Task AddAttendancClientRequest(AttendanceClientRecord attendanceClientRecord)
        {
            try
            {
                var currentTrainer =await _trainerProfileService.GetCurrentUserTrainer();
                var mobile = attendanceClientRecord.Mobile;
                 var user = await _unitOfWork.UserRepository.Where(x => x.Mobile.Trim()==mobile).FirstOrDefaultAsync();
                if (user is null)
                {
                    //create new user

                    user = _mapper.Map<User>(attendanceClientRecord);
                     var userRoleRecord = _mapper.Map<CreateAthleteUserRoleRecord>(user);
                     var userRole = _mapper.Map<UserRole>(userRoleRecord);

                     user.UserRoles.Add(userRole);

                     await _unitOfWork.UserRepository.AddAsync(user);


                     //var insertUserMessage = $"کاربری شما با رمز عبور 123456 در پلتفرم ایجاد شد-موجیم";

                    //var insertUserMessage = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                    //     $"{attendanceClientRecord.FirstName} {attendanceClientRecord.LastName}-{attendanceClientRecord.Mobile}-کاربر جدید شاگرد حضوری",
                    //     $"{attendanceClientRecord.FirstName} {attendanceClientRecord.LastName} عزیز"+
                    //     $"کاربری شما با رمز عبور 123456 در پلتفرم ایجاد شد-موجیم");

                    // await _emailSender.SendEmailAsync(insertUserMessage);

                    //await _smsService.SendSms(attendanceClientRecord.Mobile, insertUserMessage);


                }

                //create new question entity

                var question = new Question()
                {
                    FirstName = attendanceClientRecord.FirstName,
                    LastName = attendanceClientRecord.LastName,
                    TrainerPlan = attendanceClientRecord.TrainerPlanId,
                    Code = Helper.RandomStringCode(5)
                };

                var insertedQuestion = await _unitOfWork.QuestionRepository.AddAsync(question);


                //create plan

                var plan = new Plan()
                {
                    TrainerId=currentTrainer.Id,
                    UserId=user.Id,
                    AnserQuestionId=insertedQuestion.Id,
                    PlanStatus=EnumPlanStatus.WaitForCompleteAnswerProcessByAttendanceClient,
                    TrackingCode= Random.Shared.Next(1000, 9999)
                };

                var insertedPlan= await _unitOfWork.PlanRepository.AddAsync(plan);


                //var qmessage = $"{attendanceClientRecord.FirstName} {attendanceClientRecord.LastName} عزیز" +
                //               $"فرم پذیرش برنامه ی شما توسط آقا/خانم {currentTrainer.User.FirstName} {currentTrainer.User.LastName} ایجاد شد. لطفا لینک زیر را کلیک کنید و فرآیند پذیرش خود را تکمیل کنید." +
                //               Environment.NewLine +
                //               $"https://mogym.ir/attendanceclient/{question.Code}";

                var questionPaageMessage = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                    $"ارسال فرم پرسشنامه شاگرد حضوری - {user.Mobile}",
                    $"https://mogym.ir/attendanceclient/{question.Code}");

                await _emailSender.SendEmailAsync(questionPaageMessage);


                await _smsService.SendOTP(user.Mobile,
                    $"{question.Code}", "AddAttendancClientRequest") ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public async Task<List<PaidPlanRecorrd>> GetAttendanceClientRequests()
        {
            var trainer = await _trainerProfileService.GetCurrentUserTrainer();
            var plans = await _unitOfWork.PlanRepository
                .Find(x => x.TrainerId == trainer.Id && x.PlanStatus == EnumPlanStatus.WaitForCompleteAnswerProcessByAttendanceClient)
                .AsNoTracking()
                .Include(x => x.User_Plan)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return _mapper.Map<List<PaidPlanRecorrd>>(plans);
        }

        public async Task<string> EditPlanAfterSent(int planId)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.Where(x => x.Id == planId).FirstOrDefaultAsync();

                var diffTime = DateTime.Now.Subtract(plan.SendPlanDate.Value).TotalHours;
                if (diffTime < 24)
                {
                    plan.PlanStatus = EnumPlanStatus.TrainerApprovment;
                    await _unitOfWork.PlanRepository.UpdateAsync(plan);

                    var user = _unitOfWork.UserRepository.GetById(plan.UserId);

                    //var message =
                    //    $"ورزشکار عزیز;برنامه با کد {plan.TrackingCode} برای اصلاح توسط مربی شما برگشت داده شد.موجیم";


                    //var messageUser = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                    //    $"برگشت برنامه برای اصلاح-{user.Mobile}",
                    //    message);
                    //await _emailSender.SendEmailAsync(messageUser);

                    //await _smsService.SendSms(user.Mobile, message);
                    await _smsService.SendOTP(user.Mobile, plan.TrackingCode.ToString(), "EditPlanAfterSent");

                    return null;

                }
                else return $"بیشتر از ۲۴ ساعت از ثبت برنامه گذشته و امکان اصلاح وجود ندارد";
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public async Task<int> GetSentPlanCountForIndexPage()
        {
            return await _unitOfWork.PlanRepository.Find(x => x.PlanStatus == EnumPlanStatus.Sent).CountAsync();
        }

        public async Task<PlanCommentRecord> GetPlanDetailsForComment(int planId)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.Find(x => x.Id == planId && x.PlanStatus == EnumPlanStatus.Sent)
                    .Include(x => x.TrainerProfile_Plan)
                    .ThenInclude(x => x.User)
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync();

                if (plan.Comments.Count > 0)
                    throw new Exception("برای این برنامه قبلا نظر ثبت شده است");


                return _mapper.Map<PlanCommentRecord>(plan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task ApprovePlan(int planId)
        {
            try
            {
                var plan = _unitOfWork.PlanRepository.GetById(planId);
                plan.PlanStatus = EnumPlanStatus.TrainerApprovment;
                await _unitOfWork.PlanRepository.UpdateAsync(plan);

                var athlete = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                    .Include(x => x.User_Plan)
                    .Select(x => x.User_Plan)
                    .FirstOrDefaultAsync();

                //var message = $"ورزشکار عزیز;برنامه شما با کد پیگیری {plan.TrackingCode} توسط مربی تائید گردد - موجیم";

                //var message = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                //    $"تائید برنامه توسط مربی-{athlete.Mobile}",
                //    $"ورزشکار عزیز;برنامه شما با کد پیگیری {plan.TrackingCode} توسط مربی تائید گردد - موجیم");

                //await _emailSender.SendEmailAsync(message);

                //await _smsService.SendSms(athlete.Mobile, message);
                await _smsService.SendOTP(athlete.Mobile, plan.TrackingCode.ToString(), "ApprovePlan");

            }
            catch (Exception ex)
            {
                var message = $"ApprovePlan in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<ApprovePlanRecord>?> GetApprovePlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var trainer = await _trainerProfileService.GetCurrentUserTrainer();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.TrainerId == trainer.Id && x.PlanStatus == EnumPlanStatus.TrainerApprovment)
                    .AsNoTracking()
                    .Include(x => x.User_Plan)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return _mapper.Map<List<ApprovePlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetApprovePlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<PlanDetailsRecord> GetPlanDetails(int planId)
        {
            try
            {
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.Id == planId)
                    .AsNoTracking()
                    .Include(x => x.Workouts)
                    .Include(x=>x.Meals)
                    .Include(x=>x.SupplimentPlans)
                    .FirstOrDefaultAsync();
                return _mapper.Map<PlanDetailsRecord>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetPlanDetails in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task AddDescription(int planId, string description)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
            plan.Description = description;
            await _unitOfWork.PlanRepository.UpdateAsync(plan);
        }

        public async Task SendPlan(int planId)
        {
            try
            {
                var plan = _unitOfWork.PlanRepository.GetById(planId);
                plan.PlanStatus = EnumPlanStatus.Sent;
                plan.SendPlanDate=DateTime.Now;
                await _unitOfWork.PlanRepository.UpdateAsync(plan);


                var athlete = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                    .Include(x => x.User_Plan)
                    .Select(x => x.User_Plan)
                    .FirstOrDefaultAsync();

                var message =
                    $"ورزشکار عزیز;برنامه شما با کد پیگیری {plan.TrackingCode} در کارتابل برنامه های دریافت شده قابل دسترسی می باشد - موجیم";

                var email = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                    $"ارسال برنامه توسط مربی-{athlete.Mobile}",
                    message);

                await _emailSender.SendEmailAsync(email);


                //await _smsService.SendSms(athlete.Mobile, message);
                await _smsService.SendOTP(athlete.Mobile, plan.TrackingCode.ToString(), "SendPlan");


            }
            catch (Exception ex)
            {
                var message = $"SendPlan in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<SentPlanRecord>?> GetSentPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var trainer = await _trainerProfileService.GetCurrentUserTrainer();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.TrainerId == trainer.Id && x.PlanStatus == EnumPlanStatus.Sent)
                    .AsNoTracking()
                    .Include(x => x.User_Plan)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return _mapper.Map<List<SentPlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetSentPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<PlanRecord>> GetMyPaidPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId && x.PlanStatus == EnumPlanStatus.Paid)
                    .AsNoTracking()
                    .Include(x => x.TrainerProfile_Plan)
                    .ThenInclude(x => x.User)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return _mapper.Map<List<PlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetMyPaidPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<PlanRecord>> MyApprovedPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId && x.PlanStatus == EnumPlanStatus.TrainerApprovment)
                    .AsNoTracking()
                    .Include(x => x.TrainerProfile_Plan)
                    .ThenInclude(x => x.User)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return _mapper.Map<List<PlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"MyApprovedPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<PlanRecord>> MyRecivedPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId && x.PlanStatus == EnumPlanStatus.Sent)
                    .AsNoTracking()
                    .Include(x => x.TrainerProfile_Plan)
                    .ThenInclude(x => x.User)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return _mapper.Map<List<PlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"MyRecivedPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<bool> IsThisPlanIdForThisTrainer(int planId)
        {
            var trainer = await _trainerProfileService.GetCurrentUserTrainer();
            return await _unitOfWork.PlanRepository.Find(x => x.Id == planId && x.TrainerId == trainer.Id).AnyAsync();
        }

        public async Task<bool> IsThereAnyPlanWithStatus(int planId, EnumPlanStatus planStatus)
        {
            return await _unitOfWork.PlanRepository.Find(x => x.Id == planId && x.PlanStatus == planStatus).AnyAsync();
        }
    }
}
