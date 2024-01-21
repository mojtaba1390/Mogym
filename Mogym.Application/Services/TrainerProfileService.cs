using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Profile;
using Mogym.Application.Records.Question;
using Mogym.Common;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class TrainerProfileService:ITrainerProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        private static int takeCount = 10;
        public TrainerProfileService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger,IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task<TrainerProfileDetailRecord?> GetByUserName(string username)
        {
            try
            {
                var trainer= await _unitOfWork.TrainerProfileRepository
                    .Find(x => x.User.UserName == username.Trim() &&
                               x.User.UserRoles.Any(z => z.RoleId == 3))
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x=>x.TrainerAchievements)
                    .Include(x=>x.TrainerPlanCosts)
                    .Include(x=>x.Comments)
                    .ThenInclude(x=>x.User_Comment)
                    .FirstOrDefaultAsync();
                if(trainer is not null)
                    return _mapper.Map<TrainerProfileDetailRecord?>(trainer);

            }
            catch (Exception ex)
            {
                var message = $"GetByUserName in TrainerProfile Service";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }



        public async Task<CreateTrainerProfileRecord> GetByUserId(int userId)
        {
	        try
	        {
		        var trainer= await _unitOfWork.TrainerProfileRepository
			        .Find(x => x.User.Id == userId && x.User.UserRoles.Any(z => z.RoleId == 3))
			        .AsNoTracking()
			        .Include(x => x.User)
			        .ThenInclude(x => x.UserRoles)
			        .FirstOrDefaultAsync();

		        return _mapper.Map<CreateTrainerProfileRecord>(trainer);
	        }
	        catch (Exception ex)
	        {
		        var message = $"GetByUserId in TrainerProfile Service";
		        _logger.LogError(message, ex.InnerException);
		        throw ex;
	        }

	        return null;
        }



		public void Update(CreateTrainerProfileRecord trainerInfoRecord)
        {
            try
            {
                var trainerProfileOld = _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerInfoRecord.Id)
                    .Include(x => x.User)
                    .FirstOrDefault();

                var trainerInfo = _mapper.Map(trainerInfoRecord, trainerProfileOld);
                //var user = _mapper.Map(trainerInfoRecord, trainerProfileOld.User);

                  _unitOfWork.TrainerProfileRepository.Update(trainerInfo);
                  //_unitOfWork.UserRepository.Update(user);
            }
            catch (Exception ex)
            {
                var message = $"Update in TrainerProfile Service";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<CreateTrainerProfileRecord> GetEntityByUserId(int userId)
        {
            var trainerProfile = await _unitOfWork.TrainerProfileRepository.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            return  _mapper.Map<CreateTrainerProfileRecord>(trainerProfile);
        }

        public bool IsAnyUserNameExist(string? username)
        {
            return _unitOfWork.TrainerProfileRepository.Find(x => x.User.UserName == username.Trim())
                .AsNoTracking()
                .Include(x => x.User)
                .Any();
        }

        public async Task<TrainerProfileRecord?> GetById(int trainerId)
        {
            var trainer= await _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerId)
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync();
            if (trainer is not null)
                return _mapper.Map<TrainerProfileRecord>(trainer);

            return null;


        }

        public async Task<CreateQuestionRecord?> GetTrainerForCreateQuestion(int trainerId)
        {
            var trainer = await _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerId)
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x=>x.TrainerPlanCosts)
                .FirstOrDefaultAsync();
            if (trainer is not null)
                return _mapper.Map<CreateQuestionRecord>(trainer);

            return null;
        }

        public async Task<TrainerProfile?> GetCurrentUserTrainer()
        {
            var userId = _accessor.GetUser();
            return await _unitOfWork.TrainerProfileRepository.Find(x => x.UserId == userId)
                .Include(x=>x.User)
                .FirstOrDefaultAsync();
        }

        public async Task<Tuple<int, int, List<TrainersRecord>>> GetAllTrainers(int? page, string search, string sort)
        {
            var trainers =  _unitOfWork.TrainerProfileRepository
                .Where(x => x.User.Status == EnumStatus.Active)
                .Include(x => x.User)
                .Include(x => x.TrainerAchievements)
                .Include(x => x.Plans)
                .Include(x=>x.Comments).AsQueryable();


            if (page is null)
                page = 1;

            if (!string.IsNullOrWhiteSpace(search))
                trainers=trainers.Where(x=>x.User.FirstName.Contains(search) || x.User.LastName.Contains(search));

            if (!string.IsNullOrWhiteSpace(sort))
            {
                if (sort=="newest")
                    trainers = trainers.OrderByDescending(x => x.Id);
                else if (sort == "sentPlan")
                    trainers = trainers.OrderByDescending(x => x.Plans.Count(z => z.PlanStatus == EnumPlanStatus.Sent));
                else if (sort == "rate")
                    trainers = trainers.OrderByDescending(x => x.AvgUserRate);
                else if (sort == "comment")
                    trainers = trainers.OrderByDescending(x => x.Comments.Count(z => z.CommentStatus == EnumCommentStatus.Approve));

            }
            else
                trainers = trainers.OrderByDescending(x => x.SignupRate);


            var takeTrainers =await trainers.Skip((page.Value - 1) * takeCount).Take(takeCount).ToListAsync();


            var getTrainers= _mapper.Map<List<TrainersRecord>>(takeTrainers);

            return new Tuple<int, int, List<TrainersRecord>>(trainers.ToList().Count, page.Value, getTrainers);
        }

        public async Task<ConfirmAnswerQuestionRecord> GetConfirmAnswerQuestion(int trainerId,int trainerPlanId)
        {
            var trainer = await _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerId)
                .Include(x => x.User)
                .Include(x => x.TrainerPlanCosts.Where(z => z.Id == trainerPlanId))
                .Include(x => x.User).FirstOrDefaultAsync();
            return _mapper.Map<ConfirmAnswerQuestionRecord>(trainer);
        }

        public async Task<List<LastTrainersForHomePageRecord>> GetLastTrainersForHomepage()
        {
            var lastTrainers = await _unitOfWork.TrainerProfileRepository
                .Where(x=>x.User.Status==EnumStatus.Active)
                .Include(x=>x.User)
                .OrderByDescending(x=>x.Id)
                .Take(4)
                .ToListAsync();

            return   _mapper.Map<List<LastTrainersForHomePageRecord>>(lastTrainers);
        }

        public async Task<int> GetAllTrainersCount()
        {
            return await _unitOfWork.TrainerProfileRepository.GetAll().CountAsync();
        }

        public async Task<Tuple<int, int, List<TrainersRecord>>> Search(string searchText)
        {
            try
            {
                var trainers = await _unitOfWork.TrainerProfileRepository
                    .Where(x => x.User.Status == EnumStatus.Active && 
                                (x.User.FirstName.Contains(searchText) || x.User.LastName.Contains(searchText)))
                    .Include(x => x.User)
                    .Include(x => x.TrainerAchievements)
                    .Include(x => x.Plans)
                    .ToListAsync();

                var getFirstTake = trainers.Take(takeCount);

                var getTrainers = _mapper.Map<List<TrainersRecord>>(getFirstTake);

                return new Tuple<int, int, List<TrainersRecord>>(trainers.Count, 1, getTrainers);

            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public async Task<Tuple<int, int, List<TrainersRecord>>> SortByNewest()
        {
            var trainers = await _unitOfWork.TrainerProfileRepository
                .Where(x => x.User.Status == EnumStatus.Active)
                .Include(x => x.User)
                .Include(x => x.TrainerAchievements)
                .Include(x => x.Plans)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var getFirstTake = trainers.Take(takeCount);

            var getTrainers = _mapper.Map<List<TrainersRecord>>(getFirstTake);

            return new Tuple<int, int, List<TrainersRecord>>(trainers.Count, 1, getTrainers);
        }

        public async Task<Tuple<int, int, List<TrainersRecord>>> SortBySentPlan()
        {
            var trainers = await _unitOfWork.TrainerProfileRepository
                .Where(x => x.User.Status == EnumStatus.Active)
                .Include(x => x.User)
                .Include(x => x.TrainerAchievements)
                .Include(x => x.Plans)
                .OrderByDescending(x => x.Plans.Count(z=>z.PlanStatus==EnumPlanStatus.Sent))
                .ToListAsync();

            var getFirstTake = trainers.Take(takeCount);

            var getTrainers = _mapper.Map<List<TrainersRecord>>(getFirstTake);

            return new Tuple<int, int, List<TrainersRecord>>(trainers.Count, 1, getTrainers);
        }
    }
}
