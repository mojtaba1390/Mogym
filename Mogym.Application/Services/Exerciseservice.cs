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
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> AddAndUpdateExercises(List<WorkoutExerciseRecord> workoutExerciseRecords)
        {
            try
            {

                var newExercises = workoutExerciseRecords.Where(x => x.Id == 0).ToList();
                var updateExercises = workoutExerciseRecords.Where(x => x.Id != 0).ToList();

                if (newExercises.Any())
                {
                    var nexercises = _mapper.Map<List<Exercise>>(newExercises);
                    await _unitOfWork.ExerciseRepository.AddRangAsync(nexercises);

                }

                if (updateExercises.Any())
                {
                    List<Exercise> exercises = new List<Exercise>();
                    foreach (var ex in updateExercises)
                    {
                        if (ex.SuperSetId is not null)
                        {
                            var thisSupersetCount = updateExercises.Count(x=>x.SuperSetId==ex.SuperSetId);
                            if (thisSupersetCount>1)
                                return "این حرکت قبلا برای سوپرست انتخاب شده است";

                        }
                        var exercise = _unitOfWork.ExerciseRepository.GetById(ex.Id??0);

                        var uexercise = _mapper.Map(ex, exercise);
                        exercises.Add(uexercise);
                    }
                    _unitOfWork.ExerciseRepository.UpdateRange(exercises);

                }


                return null;
            }
            catch (Exception ex)
            {
                var message = $"AddAndUpdateExercises in ExerciseService,obj=" + JsonConvert.SerializeObject(workoutExerciseRecords);
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task<string> Delete(int id,int workoutId)
        {
            try
            {
                bool isThisIdIsAnyExerciseSuperSetId = await _unitOfWork.ExerciseRepository
                    .Find(x => x.SuperSetId == id && x.WorkoutId == workoutId).AnyAsync();
                if (isThisIdIsAnyExerciseSuperSetId)
                    return
                        "این حرکت سوپر ست حرکت دیگری می باشد و امکان حذف آن وجود ندارد.لطفا ابتدا حرکت سوپرست وابسته را تغییر و سپس این حرکت را حذف کنید.";

                var entity = await _unitOfWork.ExerciseRepository.GetByIdAsync(id);
                 _unitOfWork.ExerciseRepository.Delete(entity);
                 return null;
            }
            catch (Exception ex)
            {
                var message = $"Delete in ExerciseService,id=" + id;
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task<bool> IsAnyExcerciseExistByWorkoutId(int id)
        {
            try
            {
                return await _unitOfWork.ExerciseRepository.Find(x => x.WorkoutId == id).AnyAsync();

            }
            catch (Exception ex)
            {
                var message = $"IsAnyExcerciseExistByWorkoutId in ExerciseService,id=" + id;
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task<List<int?>> GetWorkoutSuperSetIds(int workoutId)
        {
            return await _unitOfWork.ExerciseRepository
                .Find(x => x.WorkoutId == workoutId && x.SuperSetId!=null)
                .Select(z => z.SuperSetId)
                .ToListAsync();
        }

        public async Task<Exercise?> GetByIdAsync(int id)
        {
            return await _unitOfWork.ExerciseRepository.GetByIdAsync(id);
        }
    }
}
