using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Lead;
using Mogym.Common.ModelExtended;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class LeadService:ILeadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public LeadService(IUnitOfWork unitOfWork,
            IMapper mapper,IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender= emailSender;
        }

        public async  Task Submit(LeadRecord leadRecord)
        {
            try
            {
                var entity = _mapper.Map<Lead>(leadRecord);
                await _unitOfWork.LeadRepository.AddAsync(entity);

                var email = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                    $"تماس با ما",
                    $"");


                await _emailSender.SendEmailAsync(email);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
