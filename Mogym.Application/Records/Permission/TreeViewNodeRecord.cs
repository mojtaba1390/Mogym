using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Permission
{
    public record TreeViewNodeRecord
    {
        public string Id { get; init; }
        public string Text { get; init; }

        public List<TreeViewNodeRecord> Children { get; set; }

    }
}
