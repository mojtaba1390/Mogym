using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities.Log
{
    public class UserLogging
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime InsertDate { get; set; }=DateTime.Now;

        public string? Permalink { get; set; }
        public string? Ip { get; set;}
        public string? VisitorId { get; set; }
    }
}
