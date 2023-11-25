using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.AutoMapper.SupplimentPlan;
using Mogym.Application.Records.MealIngridient;
using Mogym.Domain.Entities;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;

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


        public  void AddOrUpdate(List<SupplimentPlanRecord> supplimentPlanRecords)
        {
            try
            {
                var planId = supplimentPlanRecords.First().PlanId;

                var supplimentPlans = _mapper.Map<List<SupplimentPlan>>(supplimentPlanRecords);


                var supplimentPlansInserted = _unitOfWork.SupplimentPlanRepository.Find(x => x.PlanId == planId).ToList();

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
                        var entity = _unitOfWork.SupplimentPlanRepository.Where(x => x.Id == item.Id).First();
                        updateList.Add(entity);
                    }
                    _unitOfWork.SupplimentPlanRepository.UpdateRange(updateList);
                }

                if (deletedsSupplimentPlans.Count > 0)
                {
                    List<SupplimentPlan> deleteList = new List<SupplimentPlan>();
                    foreach (var item in deletedsSupplimentPlans)
                    {
                        var entity = _unitOfWork.SupplimentPlanRepository.Where(x => x.Id == item.Id).First();
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
    }
}
