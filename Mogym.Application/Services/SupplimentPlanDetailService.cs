using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.SupplimentPlanDetail;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using Mogym.Application.Records.MealIngridient;
using Mogym.Domain.Entities;
using Newtonsoft.Json;

namespace Mogym.Application.Services
{
    public class SupplimentPlanDetailService: ISupplimentPlanDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public SupplimentPlanDetailService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task<List<SupplimentPlanDetailRecord>> GetSupplimentPlanDetail(int supplimentPlanId)
        {
            try
            {
                var entities = await _unitOfWork.SupplimentPlanDetailRepository.Find(x => x.SupplimentPlanId == supplimentPlanId).ToListAsync();
                return _mapper.Map<List<SupplimentPlanDetailRecord>>(entities);
            }
            catch (Exception ex)
            {
                var message = $"GetSupplimentPlanDetail in SupplimentPlanDetailService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public void AddOrUpdateSets(List<SupplimentPlanDetailRecord> supplimentPlanDetailRecords)
        {
            try
            {
                var supplimentPlanId = supplimentPlanDetailRecords.First().SupplimentPlanId;

                var supplimentPlanDetails = _mapper.Map<List<SupplimentPlanDetail>>(supplimentPlanDetailRecords);


                var inserterdSupplimentPlanDetails = _unitOfWork.SupplimentPlanDetailRepository.Find(x => x.SupplimentPlanId == supplimentPlanId).ToList();

                var newSupplimentPlanDetails = supplimentPlanDetails.Where(x => x.Id == 0).ToList();
                var updatedSupplimentPlanDetails = supplimentPlanDetailRecords.Where(x => x.Id > 0).ToList();

                var deleteSupplimentPlanDetails = inserterdSupplimentPlanDetails.ExceptBy(updatedSupplimentPlanDetails.Select(x => x.Id), x => x.Id).ToList();

                if (newSupplimentPlanDetails.Count > 0)
                    _unitOfWork.SupplimentPlanDetailRepository.AddRang(newSupplimentPlanDetails);

                if (updatedSupplimentPlanDetails.Count > 0)
                {
                    List<SupplimentPlanDetail> updateList = new List<SupplimentPlanDetail>();
                    foreach (var item in updatedSupplimentPlanDetails)
                    {
                        var entity = _unitOfWork.SupplimentPlanDetailRepository.Where(x => x.Id == item.Id).First();
                        var mapped = _mapper.Map(item, entity);
                        updateList.Add(mapped);
                    }
                    _unitOfWork.SupplimentPlanDetailRepository.UpdateRange(updateList);
                }

                if (deleteSupplimentPlanDetails.Count > 0)
                {
                    List<SupplimentPlanDetail> deleteList = new List<SupplimentPlanDetail>();
                    foreach (var item in deleteSupplimentPlanDetails)
                    {
                        var entity = _unitOfWork.SupplimentPlanDetailRepository.Where(x => x.Id == item.Id).First();
                        deleteList.Add(entity);
                    }
                    _unitOfWork.SupplimentPlanDetailRepository.DeleteRange(deleteList);
                }





            }
            catch (Exception ex)
            {
                var message = $"AddOrUpdateSets in SupplimentPlanDetailService,obj=" + JsonConvert.SerializeObject(supplimentPlanDetailRecords);
                _logger.LogError(message, ex);
                throw ex;
            }
        }

        public async Task<bool> IsAnySupplimentDetailExistBySupplimentPlanId(int id)
        {
            try
            {
                return await _unitOfWork.SupplimentPlanDetailRepository.Find(x => x.SupplimentPlanId == id).AnyAsync();

            }
            catch (Exception ex)
            {
                var message = $"IsAnySupplimentDetailExistBySupplimentPlanId in SupplimentPlanDetailService,id=" + id;
                _logger.LogError(message, ex);
                throw ex;
            }
        }



    }
}
