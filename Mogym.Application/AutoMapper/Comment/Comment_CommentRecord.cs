using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Comment;

namespace Mogym.Application.AutoMapper.Comment
{
    public class Comment_CommentRecord:global::AutoMapper.Profile
    {
        public Comment_CommentRecord()
        {
            CreateMap<Domain.Entities.Comment, CommentRecord>()
                .ForMember(x => x.UserFullName,
                    z => z.MapFrom(a => a.User_Comment.FirstName + " " + a.User_Comment.LastName))
                .ForMember(x => x.Rate,
                    z => z.MapFrom(a => a.Rate))
                .ForMember(x => x.Review,
                    z => z.MapFrom(a => a.Review));
        }
    }
}
