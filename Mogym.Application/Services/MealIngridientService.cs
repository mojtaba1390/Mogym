using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.MealIngridient;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using Mogym.Application.Records.Meal;
using Mogym.Domain.Entities;
using Newtonsoft.Json;
using Mogym.Application.Records.ExerciseSet;

namespace Mogym.Application.Services
{
    public class MealIngridientService : IMealIngridientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public MealIngridientService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }


        public async Task<List<MealIngridientRecord>> GetMealIngridients(int mealId)
        {
            try
            {
                var entities = await _unitOfWork.MealIngridientRepository.Find(x => x.MealId == mealId).ToListAsync();
                return _mapper.Map<List<MealIngridientRecord>>(entities);
            }
            catch (Exception ex)
            {
                var message = $"GetMealIngridients in MealIngridientService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public void AddOrUpdateSets(List<MealIngridientRecord> mealIngridientRecords)
        {
            try
            {
                var mealId = mealIngridientRecords.First().MealId;

                var sets = _mapper.Map<List<MealIngridient>>(mealIngridientRecords);


                var insertedMealIngridients = _unitOfWork.MealIngridientRepository.Find(x => x.MealId == mealId).ToList();

                var newMealIngridients = sets.Where(x => x.Id == 0).ToList();
                var updatedMealIngridientss = mealIngridientRecords.Where(x => x.Id > 0).ToList();

                var deletedMealIngridients = insertedMealIngridients.ExceptBy(updatedMealIngridientss.Select(x => x.Id), x => x.Id).ToList();

                if (newMealIngridients.Count > 0)
                    _unitOfWork.MealIngridientRepository.AddRang(newMealIngridients);

                if (updatedMealIngridientss.Count > 0)
                {
                    List<MealIngridient> updateList = new List<MealIngridient>();
                    foreach (var item in updatedMealIngridientss)
                    {
                        var entity = _unitOfWork.MealIngridientRepository.Where(x => x.Id == item.Id).First();
                        var mapped = _mapper.Map(item, entity);
                        updateList.Add(mapped);
                    }
                    _unitOfWork.MealIngridientRepository.UpdateRange(updateList);
                }

                if (deletedMealIngridients.Count > 0)
                {
                    List<MealIngridient> deleteList = new List<MealIngridient>();
                    foreach (var item in deletedMealIngridients)
                    {
                        var entity = _unitOfWork.MealIngridientRepository.Where(x => x.Id == item.Id).First();
                        deleteList.Add(entity);
                    }
                    _unitOfWork.MealIngridientRepository.DeleteRange(deleteList);
                }





            }
            catch (Exception ex)
            {
                var message = $"AddOrUpdateSets in MealIngridientService,obj=" + JsonConvert.SerializeObject(mealIngridientRecords);
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task<bool> IsAnyIngridientExistByMealId(int id)
        {
            try
            {
                return await _unitOfWork.MealIngridientRepository.Find(x => x.MealId == id).AnyAsync();

            }
            catch (Exception ex)
            {
                var message = $"IsAnyIngridientExistByMealId in MealIngridientService,id=" + id;
                _logger.LogError(message, ex);
                throw ex;
            }
        }
    }
}
