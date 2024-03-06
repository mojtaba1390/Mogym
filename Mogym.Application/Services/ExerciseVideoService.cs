using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.ExerciseVideo;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Common;
using Mogym.Common.ModelExtended;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class ExerciseVideoService: IExerciseVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailSender _emailSender;


        public ExerciseVideoService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ISeriLogService logger,
            IHttpContextAccessor accessor,
            IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
            _emailSender = emailSender;
        }


        public async Task<List<ExerciseVideoRecord>> GetAllExerciseVideo()
        {
            try
            {
                var currentUser = _accessor.GetUser();
                var exercises = _unitOfWork.ExerciseVideoRepository.Find(x=>x.Status==EnumExrciseVideoStatus.Approve && (x.UserId==null || x.UserId== currentUser)).OrderByDescending(x=>x.Id);
                return _mapper.Map<List<ExerciseVideoRecord>>(exercises);
            }
            catch (Exception ex)
            {
                var message = $"GetAllExerciseVideo in ExerciseVideoService";
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task AddAsync(CreateExerciseVideoRecord createExerciseVideoRecord)
        {
            try
            {
                var exerciseVideo = _mapper.Map<ExerciseVideo>(createExerciseVideoRecord);
                 await _unitOfWork.ExerciseVideoRepository.AddAsync(exerciseVideo);

                 var email = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                     $"بارگزاری ویدئو",
                     "");

                 await _emailSender.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                var message = $"AddAsync in ExerciseVideoService";
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task<ExerciseVideoRecord> GetEntityByIdAsync(int videoId)
        {
            try
            {
                var exerciseVideo =await _unitOfWork.ExerciseVideoRepository.Find(x=>x.Id==videoId).FirstOrDefaultAsync();
                 return _mapper.Map<ExerciseVideoRecord>(exerciseVideo);
            }
            catch (Exception ex)
            {
                var message = $"GetEntityByIdAsync in ExerciseVideoService,id="+videoId;
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task<List<ExerciseVideoRecord>> GetMyExerciseVideo()
        {
            var uId = _accessor.GetUser();
            try
            {
                var exercises = _unitOfWork.ExerciseVideoRepository.Find(x => x.UserId == uId).OrderByDescending(x => x.Id);
                return _mapper.Map<List<ExerciseVideoRecord>>(exercises);
            }
            catch (Exception ex)
            {
                var message = $"GetMyExerciseVideo in ExerciseVideoService";
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public void Delete(int id)
        {
            var entity =  _unitOfWork.ExerciseVideoRepository.GetById(id);
             _unitOfWork.ExerciseVideoRepository.Delete(entity);
        }
    }
}
