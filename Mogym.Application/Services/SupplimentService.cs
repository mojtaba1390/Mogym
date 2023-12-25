using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Suppliment;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Ingridient;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class SupplimentService: ISupplimentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public SupplimentService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task<List<SupplimentRecord>> GetAll()
        {
            try
            {
                return await Task.Run(() => _unitOfWork.SupplimentRepository.GetAll()
                    .ProjectTo<SupplimentRecord>(_mapper.ConfigurationProvider).ToList());
            }
            catch (Exception ex)
            {
                var message = $"GetAll in SupplimentService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task AddAsync(SupplimentRecord supplimentRecord)
        {
            try
            {
                var entity = _mapper.Map<Suppliment>(supplimentRecord);
                await _unitOfWork.SupplimentRepository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                var message = $"AddAsync in SupplimentService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task Edit(int id, string title)
        {
            try
            {
                var entity = await _unitOfWork.SupplimentRepository.GetByIdAsync(id);
                entity.Title = title;
                await _unitOfWork.SupplimentRepository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                var message = $"Edit in SupplimentService,id=" + id;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task Delete(int deleteId)
        {
            try
            {
                var entity = await _unitOfWork.SupplimentRepository.GetByIdAsync(deleteId);
                _unitOfWork.SupplimentRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                var message = $"Delete in SupplimentService,id=" + deleteId;
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }
    }
}
