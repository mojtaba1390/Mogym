using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Profile
{
    public record SignUpTrainerRecord
    {
        public int TrainerProfileId { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int Gender { get; init; }
        public string UserName { get; init; }
        public string Mobile { get; init; }

        public IFormFile NationalCartPic { get; init; }
        public IFormFile TrainingCertificatePic { get; init; }
        public IFormFile? WorkingCertificatePic { get; init; }
        public IFormFile? ChampCertificatePic1 { get; init; }
        public IFormFile? ChampCertificatePic2 { get; init; }
        public IFormFile? ChampCertificatePic3 { get; init; }
        public IFormFile? GraduationCertificatePic { get; init; }
        public IFormFile? StudingCertificatePic { get; init; }
        public bool IsConfirm { get; init; }

    }
}
