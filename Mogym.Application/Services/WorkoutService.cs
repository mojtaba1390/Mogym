using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Workout;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Exercise;
using Mogym.Application.Records.ExerciseVideo;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;
using Newtonsoft.Json;

namespace Mogym.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public WorkoutService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }
        public async Task AddOrUpdate(List<WorkoutRecord> workoutRecords)
        {
            try
            {
                var workouts = _mapper.Map<List<Workout>>(workoutRecords);
                var addWorkouts = workouts.Where(x => x.Id == 0);
                var updateWorkouts = workouts.Where(x => x.Id != 0);
                if (addWorkouts.Any())
                    await _unitOfWork.WorkoutRepository.AddRangAsync(addWorkouts);
                if (updateWorkouts.Any())
                    _unitOfWork.WorkoutRepository.UpdateRange(updateWorkouts);
            }
            catch (Exception ex)
            {
                var message = $"AddOrUpdate in WorkoutService,obj=" + JsonConvert.SerializeObject(workoutRecords);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<WorkoutExerciseRecord>> GetWorkoutDetails(int id)
        {
            try
            {
                return await _unitOfWork.ExerciseRepository.Find(x => x.WorkoutId == id)
                    .ProjectTo<WorkoutExerciseRecord>(_mapper.ConfigurationProvider).ToListAsync();

            }
            catch (Exception ex)
            {
                var message = $"GetWorkoutDetails in WorkoutService,id=" + id;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<SuperSetRecord>> GetSuperSetExercises(int id)
        {
            try
            {
                return await _unitOfWork.ExerciseRepository
                    .Find(x => x.WorkoutId == id && x.SuperSetId == null)
                    .Include(x => x.ExerciseVideo_Exercise)
                    .ProjectTo<SuperSetRecord>(_mapper.ConfigurationProvider).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"GetWorkoutDetails in WorkoutService,id=" + id;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task Edit(int id, string title)
        {
            try
            {
                var entity = await _unitOfWork.WorkoutRepository.GetByIdAsync(id);
                entity.Title = title;
                await _unitOfWork.WorkoutRepository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                var message = $"Edit in WorkoutService,id=" + id;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task Delete(int deleteId)
        {
            try
            {
                var entity = await _unitOfWork.WorkoutRepository.GetByIdAsync(deleteId);
                 _unitOfWork.WorkoutRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                var message = $"Delete in WorkoutService,id=" + deleteId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<SentWorkoutRecord>> GetSentWorkoutDetail(int planId)
        {
            try
            {
                var workouts = await _unitOfWork.WorkoutRepository.Find(x => x.PlanId == planId)
                    .Include(x => x.Exercises)
                    .ThenInclude(x => x.ExerciseVideo_Exercise)
                    .Include(x => x.Exercises)
                    .ThenInclude(x => x.ExerciseSets).ToListAsync();

                return _mapper.Map<List<SentWorkoutRecord>>(workouts);

            }
            catch (Exception ex)
            {
                var message = $"GetSentWorkoutDetail in WorkoutService,id=" + planId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<Workout?> GetByIdAsync(int id)
        {
            return await _unitOfWork.WorkoutRepository.Find(x=>x.Id==id).FirstOrDefaultAsync();
        }
    }
}
