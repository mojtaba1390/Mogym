using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Mogym.Application.Records.ExerciseVideo
{
    public record ExerciseVideoRecord
    {
        public int? Id { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public string? FileName { get; init; }
        public string? Status { get; init; }
    }
}
