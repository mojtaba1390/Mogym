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
    public enum EnumTrainerPlan
    {
        [EnumMember]
        [Description("برنامه حرکات تمرینی")]
        Workout = 1,
        [EnumMember]
        [Description("برنامه رژیم غذایی")]
        Diet = 2,
        [EnumMember]
        [Description("برنامه حرکات تمرینی و رژیم غذایی")]
        WorkoutAndDiet = 3,
        [EnumMember]
        [Description("برنامه حرکات تمرینی حضوری")]
        AttendanceClientWorkout = 4,
        [EnumMember]
        [Description("برنامه رژیم غذایی حضوری")]
        AttendanceClientDiet = 5,
        [EnumMember]
        [Description("برنامه حرکات تمرینی و رژیم غذایی حضوری")]
        AttendanceClientWorkoutAndDiet = 6
    }
    public enum EnumDailyAvtivity
    {
        [EnumMember]
        [Description("بی تحرک(دائما نشسته)")]
        None = 1,
        [EnumMember]
        [Description("متوسط(فعالیت سبک مثل راه رفتن)")]
        Moderate = 2,
        [EnumMember]
        [Description("زیاد(کار سنگین و فعالیت زیاد)")]
        Active = 3
    }
    public enum EnumSessionsInWeek
    {
        [EnumMember]
        [Description("یک")]
        One = 1,
        [EnumMember]
        [Description("دو")]
        Two = 2,
        [EnumMember]
        [Description("سه")]
        Three = 3,
        [EnumMember]
        [Description("چهار")]
        Four = 4,
        [EnumMember]
        [Description("پنج")]
        Five = 5,
        [EnumMember]
        [Description("شش")]
        Six = 6,
        [EnumMember]
        [Description("هفت")]
        Seven = 7
    }


    public enum EnumPlanStatus
    {
        [EnumMember]
        [Description("ثبت شده")]
        Registered = 1,
        [EnumMember]
        [Description("پرداخت شده")]
        Paid = 2,
        [EnumMember]
        [Description("تائید مربی")]
        TrainerApprovment = 3,
        [EnumMember]
        [Description("ارسال شده")]
        Sent =4,
        [EnumMember]
        [Description("در انتظار تائید سیستم تصویر")]
        WaitForApprovePaidPic =5,
        [EnumMember]
        [Description("در انتظار تکمیل فرم توسط شاگرد حضوری")]
        WaitForCompleteAnswerProcessByAttendanceClient =6
    }
    public enum EnumSize
    {
        [EnumMember]
        [Description("کوچک")]
        Small = 1,
        [EnumMember]
        [Description("متوسط")]
        Medium = 2,
        [EnumMember]
        [Description("بزرگ")]
        Big = 3
    }
    public enum EnumScale
    {
        [EnumMember]
        [Description("عدد")]
        Count = 1,
        [EnumMember]
        [Description("گرم")]
        Gram = 2,
        [EnumMember]
        [Description("اسکوپ")]
        Scope = 3
    }
    public enum EnumTicketStatus
    {
        [EnumMember]
        [Description("ارسال شده")]
        Sent = 1,
        [EnumMember]
        [Description("خوانده شده")]
        Read = 2
    }
    public enum EnumApiType
    {
        [EnumMember]
        Get=1,
        [EnumMember]
        Post=2
    }
}
