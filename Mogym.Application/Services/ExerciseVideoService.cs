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

        public ExerciseVideoService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ISeriLogService logger,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }


        public async Task<List<ExerciseVideoRecord>> GetAllExerciseVideo()
        {
            try
            {
                var exercises = _unitOfWork.ExerciseVideoRepository.GetAll().OrderByDescending(x=>x.Id);
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
    }
}
