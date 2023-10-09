using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mogym.Domain.Common
{
    public enum EnumGender
    {
        [EnumMember]
        Zan = 1,
        [EnumMember]
        Mard = 2
    }
    public enum EnumStatus
    {
        [EnumMember]
        Active = 1,
        [EnumMember]
        NotActive = 2,
        [EnumMember]
        WaitingForSmsConfirm = 3
    }
    public enum EnumYesNo
    {
        [EnumMember]
        Yes = 1,
        [EnumMember]
        No = 2
    }
}
