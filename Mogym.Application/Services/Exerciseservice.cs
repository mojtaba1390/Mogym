using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Workout;
using Mogym.Application.Records.ExerciseVideo;
using Mogym.Domain.Entities;
using Newtonsoft.Json;

namespace Mogym.Application.Services
{
    public class Exerciseservice : IExerciseservice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;

        public Exerciseservice(IUnitOfWork unitOfWork,
            IMapper mapper,
            ISeriLogService logger,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task AddAndUpdateExercises(List<WorkoutExerciseRecord> workoutExerciseRecords)
        {
            try
            {
                var exercises = _mapper.Map<List<Exercise>>(workoutExerciseRecords);

                var newExercises = exercises.Where(x => x.Id == 0).ToList();
                var updateExercises = exercises.Where(x => x.Id != 0).ToList();

                if (newExercises.Any())
                    await _unitOfWork.ExerciseRepository.AddRangAsync(newExercises);
                if (updateExercises.Any())
                    _unitOfWork.ExerciseRepository.UpdateRange(updateExercises);



            }
            catch (Exception ex)
            {
                var message = $"AddAndUpdateExercises in ExerciseVideoService,obj=" + JsonConvert.SerializeObject(workoutExerciseRecords);
                _logger.LogError(message, ex);
                throw ex;
            }
        }
    }
}
