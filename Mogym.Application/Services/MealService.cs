using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Meal;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using Mogym.Application.Records.Workout;
using Mogym.Domain.Entities;
using Newtonsoft.Json;

namespace Mogym.Application.Services
{
    public class MealService: IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public MealService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }



        public async Task AddOrUpdate(List<MealRecord> mealRecords)
        {
            try
            {
                var meals = _mapper.Map<List<Meal>>(mealRecords);
                var addMeals = meals.Where(x => x.Id == 0);
                var updatedMeals = meals.Where(x => x.Id != 0);
                if (addMeals.Any())
                    await _unitOfWork.MealRepository.AddRangAsync(addMeals);
                if (updatedMeals.Any())
                    _unitOfWork.MealRepository.UpdateRange(updatedMeals);
            }
            catch (Exception ex)
            {
                var message = $"AddOrUpdate in MealService,obj=" + JsonConvert.SerializeObject(mealRecords);
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }


        public async Task Edit(int id, string title)
        {
            try
            {
                var entity = await _unitOfWork.MealRepository.GetByIdAsync(id);
                entity.Title = title;
                await _unitOfWork.MealRepository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                var message = $"Edit in MealService,id=" + id;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task Delete(int deleteId)
        {
            try
            {
                var entity = await _unitOfWork.MealRepository.GetByIdAsync(deleteId);
                _unitOfWork.MealRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                var message = $"Delete in MealService,id=" + deleteId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<SentMealRecord>> GetSentDietDetail(int planId)
        {
            try
            {
                var meals =await  _unitOfWork.MealRepository.Find(x => x.PlanId == planId)
                    .Include(x => x.MealIngridients)
                    .ThenInclude(x => x.Ingredient_MealIngridient)
                    .ToListAsync();
                return _mapper.Map<List<SentMealRecord>>(meals);
            }
            catch (Exception ex)
            {
                var message = $"GetSentDietDetail in MealService,id=" + planId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }
    }
}
