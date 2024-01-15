using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain
{
    public abstract class BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public DateTime InsertDate { get; set; } = DateTime.Now;


        public DateTime? LastModifiedDate { get; set; } = DateTime.Now;

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
