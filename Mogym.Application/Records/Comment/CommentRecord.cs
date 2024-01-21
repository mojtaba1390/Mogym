using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Comment
{
    public class CommentRecord
    {
        public string UserFullName { get; init; }
        public string? Review { get; init; }
        public int? Rate { get; init; }
    }
}
