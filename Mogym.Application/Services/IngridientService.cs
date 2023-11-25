using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Ingridient;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using Mogym.Domain.Entities;

namespace Mogym.Application.Services
{
    public class IngridientService: IIngridientService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        public IngridientService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task<List<IngredientRecord>> GetAll()
        {
            try
            {
                return await Task.Run(()=> _unitOfWork.IngridientRepository.GetAll()
                    .ProjectTo<IngredientRecord>(_mapper.ConfigurationProvider).ToList());
            }
            catch (Exception ex)
            {
                var message = $"GetAll in IngridientService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task AddAsync(IngredientRecord ingredientRecord)
        {
            try
            {
                var entity = _mapper.Map<Ingredient>(ingredientRecord);
                await _unitOfWork.IngridientRepository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                var message = $"GetAll in AddAsync";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }
    }
}
