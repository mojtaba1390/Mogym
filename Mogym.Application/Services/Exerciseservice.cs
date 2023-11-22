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

        public Exerciseservice(IUnitOfWork unitOfWork,
            IMapper mapper,
            ISeriLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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
                var message = $"AddAndUpdateExercises in ExerciseService,obj=" + JsonConvert.SerializeObject(workoutExerciseRecords);
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await _unitOfWork.ExerciseRepository.GetByIdAsync(id);
                 _unitOfWork.ExerciseRepository.Delete(entity);

            }
            catch (Exception ex)
            {
                var message = $"Delete in ExerciseService,id=" + id;
                _logger.LogError(message, ex);
                throw ex;
            }
        }
    }
}
