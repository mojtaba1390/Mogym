using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Common
{
    public enum EnumGender
    {
        [Description("زن")]
        [EnumMember]
        Zan = 1,
        [Description("مرد")]
        [EnumMember]
        Mard = 2
    }
    public enum EnumStatus
    {
        [Description("فعال")]
        [EnumMember]
        Active = 1,
        [Description("غیرفعال")]
        [EnumMember]
        NotActive = 2,
        [Description("در انتظار تائید پیامک")]
        [EnumMember]
        WaitingForSmsConfirm = 3
    }
    public enum EnumYesNo
    {
        [EnumMember]
        [Description("بله")]
        Yes = 1,
        [EnumMember]
        [Description("خیر")]
        No = 2
    }
}
