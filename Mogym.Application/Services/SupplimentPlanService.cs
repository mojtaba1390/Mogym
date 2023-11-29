using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.MealIngridient;
using Mogym.Domain.Entities;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Suppliment;
using Mogym.Infrastructure;
using Mogym.Application.Records.SupplimentPlan;

namespace Mogym.Application.Services
{
    public class SupplimentPlanService: ISupplimentPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public SupplimentPlanService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }


        public  async Task AddOrUpdate(List<SupplimentPlanRecord> supplimentPlanRecords)
        {
            try
            {
                var planId = supplimentPlanRecords.First().PlanId;

                var supplimentPlans = _mapper.Map<List<SupplimentPlan>>(supplimentPlanRecords);


                var supplimentPlansInserted = await _unitOfWork.SupplimentPlanRepository.Find(x => x.PlanId == planId).ToListAsync();

                var newSupplimentPlanss = supplimentPlans.Where(x => x.Id == 0).ToList();
                var updatedSupplimentPlans = supplimentPlans.Where(x => x.Id > 0).ToList();

                var deletedsSupplimentPlans = supplimentPlansInserted.ExceptBy(updatedSupplimentPlans.Select(x => x.Id), x => x.Id).ToList();

                if (newSupplimentPlanss.Count > 0)
                    _unitOfWork.SupplimentPlanRepository.AddRang(newSupplimentPlanss);

                if (updatedSupplimentPlans.Count > 0)
                {
                    List<SupplimentPlan> updateList = new List<SupplimentPlan>();
                    foreach (var item in updatedSupplimentPlans)
                    {
                        var entity = await _unitOfWork.SupplimentPlanRepository.Where(x => x.Id == item.Id).FirstAsync();
                        updateList.Add(entity);
                    }
                    _unitOfWork.SupplimentPlanRepository.UpdateRange(updateList);
                }

                if (deletedsSupplimentPlans.Count > 0)
                {
                    List<SupplimentPlan> deleteList = new List<SupplimentPlan>();
                    foreach (var item in deletedsSupplimentPlans)
                    {
                        var entity =await _unitOfWork.SupplimentPlanRepository.Where(x => x.Id == item.Id).FirstAsync();
                        deleteList.Add(entity);
                    }
                    _unitOfWork.SupplimentPlanRepository.DeleteRange(deleteList);
                }





            }
            catch (Exception ex)
            {
                var message = $"AddOrUpdate in SupplimentPlanService,obj=" + JsonConvert.SerializeObject(supplimentPlanRecords);
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task Delete(int deleteId)
        {
            try
            {
                var entity = await _unitOfWork.SupplimentPlanRepository.GetByIdAsync(deleteId);
                _unitOfWork.SupplimentPlanRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                var message = $"Delete in SupplimentPlanService,id=" + deleteId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<SentSupplimentPlanRecord>> GetSentSupplimentDetail(int planId)
        {
            try
            {
                var supplimentPlans =await  _unitOfWork.SupplimentPlanRepository.Find(x => x.PlanId == planId)
                    .Include(x => x.SupplimentPlanDetails)
                    .ThenInclude(x => x.Suppliment_SupplimentPlanDetail)
                    .ToListAsync();

                return _mapper.Map<List<SentSupplimentPlanRecord>>(supplimentPlans);
            }
            catch (Exception ex)
            {
                var message = $"GetSentSupplimentDetail in SupplimentPlanService,id=" + planId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }
    }
}
