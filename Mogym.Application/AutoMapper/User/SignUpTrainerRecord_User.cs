using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Profile;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.User
{
    public class SignUpTrainerRecord_User:global::AutoMapper.Profile
    {
        public SignUpTrainerRecord_User()
        {
            CreateMap<SignUpTrainerRecord, Domain.Entities.User>()
                .ForMember(x => x.RowVersion,
                    z => z.Ignore())
                .ForMember(x => x.InsertDate,
                    z => z.Ignore())
                .ForMember(x => x.Mobile,
                    z => z.Ignore())
                .ForMember(x => x.Status,
                    z => z.MapFrom(a => EnumStatus.PreRegister))
                .ForMember(x => x.FirstName,
                    z => z.MapFrom(a => a.FirstName))
                .ForMember(x => x.LastName,
                    z => z.MapFrom(a => a.LastName))
                .ForMember(x => x.Gender,
                    z => z.MapFrom(a => a.Gender))
                .ForMember(x => x.UserName,
                    z => z.MapFrom(a => a.UserName))
                .ForPath(x=>x.TrainerProfile.CartOwnerName,
                    z=>z.MapFrom(a=>a.FirstName + " " + a.LastName))
                .ForPath(x => x.TrainerProfile.NationalCartPic,
                    z => z.MapFrom(a => a.NationalCartPic.FileName))
                .ForPath(x => x.TrainerProfile.TrainingCertificatePic,
                    z => z.MapFrom(a => a.TrainingCertificatePic.FileName))
                .ForPath(x => x.TrainerProfile.WorkingCertificatePic,
                    z => z.MapFrom(a => a.WorkingCertificatePic.FileName))
                .ForPath(x => x.TrainerProfile.ChampCertificatePic1,
                    z => z.MapFrom(a => a.ChampCertificatePic1.FileName))
                .ForPath(x => x.TrainerProfile.ChampCertificatePic2,
                    z => z.MapFrom(a => a.ChampCertificatePic2.FileName))
                .ForPath(x => x.TrainerProfile.ChampCertificatePic3,
                    z => z.MapFrom(a => a.ChampCertificatePic3.FileName))
                .ForPath(x => x.TrainerProfile.GraduationCertificatePic,
                    z => z.MapFrom(a => a.GraduationCertificatePic.FileName))
                .ForPath(x => x.TrainerProfile.StudingCertificatePic,
                    z => z.MapFrom(a => a.StudingCertificatePic.FileName));

        }
    }
}
