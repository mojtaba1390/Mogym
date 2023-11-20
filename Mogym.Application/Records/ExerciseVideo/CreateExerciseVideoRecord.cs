using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.ExerciseVideo
{
    public record CreateExerciseVideoRecord: ExerciseVideoRecord
    {
        public IFormFile? ExerciseVideo { get; set; }

    }
}
