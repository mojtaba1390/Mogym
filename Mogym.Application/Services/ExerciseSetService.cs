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
using Mogym.Domain.Entities;
using Newtonsoft.Json;

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

        public void AddOrUpdateSets(List<ExerciseSetRecord> exerciseSetRecords)
        {
            try
            {
                var exerciseId = exerciseSetRecords.First().ExerciseId;

                var sets = _mapper.Map<List<ExerciseSet>>(exerciseSetRecords);


                var insertedSets =  _unitOfWork.ExerciseSetRepository.Find(x => x.ExerciseId == exerciseId).ToList();

                var newSets = sets.Where(x => x.Id == 0).ToList();
                var updatedSets = sets.Where(x => x.Id > 0).ToList();

                var deletedSets = insertedSets.ExceptBy(updatedSets.Select(x=>x.Id),x=>x.Id).ToList();

                if (newSets.Count>0)
                    _unitOfWork.ExerciseSetRepository.AddRang(newSets);


                if (updatedSets.Count > 0)
                {
                    if (updatedSets.Count > 0)
                    {
                        List<ExerciseSet> updateList = new List<ExerciseSet>();
                        foreach (var item in updatedSets)
                        {
                            var entity = _unitOfWork.ExerciseSetRepository.Where(x => x.Id == item.Id).First();
                            updateList.Add(entity);
                        }
                        _unitOfWork.ExerciseSetRepository.UpdateRange(updatedSets);
                    }
                }
                if (deletedSets.Count > 0)
                {
                    List<ExerciseSet> deleteList=new List<ExerciseSet>();
                    foreach (var item in deletedSets)
                    {
                        var entity =  _unitOfWork.ExerciseSetRepository.Where(x=>x.Id==item.Id).First();
                        deleteList.Add(entity);
                    }
                    _unitOfWork.ExerciseSetRepository.DeleteRange(deleteList);
                }





            }
            catch (Exception ex)
            {
                var message = $"AddOrUpdateSets in ExerciseSetService,obj="+JsonConvert.SerializeObject(exerciseSetRecords);
                _logger.LogError(message, ex);
                throw ex;
            }
        }
    }
}
