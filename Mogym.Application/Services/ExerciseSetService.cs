using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.ExerciseSet;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using Mogym.Application.Records.ExerciseVideo;

namespace Mogym.Application.Services
{
    public class ExerciseSetService: IExerciseSetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;

        public ExerciseSetService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ISeriLogService logger,
            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }
        public async Task<List<ExerciseSetRecord>> GetExerciseSetDetail(int exerciseId)
        {
            try
            {
                return await _unitOfWork.ExerciseSetRepository.Find(x => x.ExerciseId == exerciseId)
                    .ProjectTo<ExerciseSetRecord>(_mapper.ConfigurationProvider).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"GetExerciseSetDetail in ExerciseSetService";
                _logger.LogError(message, ex);
                throw ex;
            }
        }
    }
}
