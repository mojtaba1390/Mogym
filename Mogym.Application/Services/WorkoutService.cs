using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Workout;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Interfaces.ILog;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;
using Newtonsoft.Json;

namespace Mogym.Application.Services
{
    public class WorkoutService: IWorkoutService
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
                var addWorkouts = workouts.Where(x => x.Id==0);
                var updateWorkouts = workouts.Where(x => x.Id!=0);
                if (addWorkouts.Any())
                   await _unitOfWork.WorkoutRepository.AddRangAsync(addWorkouts);
                if (updateWorkouts.Any())
                     _unitOfWork.WorkoutRepository.UpdateRange(updateWorkouts);
            }
            catch (Exception ex)
            {
                var message = $"AddOrUpdate in WorkoutService,obj="+JsonConvert.SerializeObject(workoutRecords);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }
    }
}
