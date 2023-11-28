using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Common
{
    public static class Helper
    {

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }

        public static string GetShamsiDate(this DateTime miladi)
        {
            if (miladi != null)
            {
                DateTime param = Convert.ToDateTime(miladi);
                System.Globalization.PersianCalendar Persian = new System.Globalization.PersianCalendar();
                string Year = Persian.GetYear(param).ToString();
                string Month = Persian.GetMonth(param).ToString();
                string Day = Persian.GetDayOfMonth(param).ToString();
                if (Month.Length == 1)
                    Month = "0" + Month;
                if (Day.Length == 1)
                    Day = "0" + Day;
                return (Year/*.EnglishNumberToPerainNumber()*/ + "/" + Month/*.EnglishNumberToPerainNumber()*/ + "/" + Day/*.EnglishNumberToPerainNumber()*/);
            }
            return String.Empty;
        }
        public static string GetShamsiDate(this DateTime? miladi)
        {
            if (miladi != null)
            {
                DateTime param = Convert.ToDateTime(miladi);
                System.Globalization.PersianCalendar Persian = new System.Globalization.PersianCalendar();
                string Year = Persian.GetYear(param).ToString();
                string Month = Persian.GetMonth(param).ToString();
                string Day = Persian.GetDayOfMonth(param).ToString();
                if (Month.Length == 1)
                    Month = "0" + Month;
                if (Day.Length == 1)
                    Day = "0" + Day;
                return Year + "/" + Month + "/" + Day;
            }
            return String.Empty;
        }
        public static string GetPersianDateDetail(this DateTime miladi)
        {
            if (miladi != null)
            {
                DateTime param = Convert.ToDateTime(miladi);
                System.Globalization.PersianCalendar Persian = new System.Globalization.PersianCalendar();
                string Year = Persian.GetYear(param).ToString();
                string Month = Persian.GetMonth(param).ToString();
                string Day = Persian.GetDayOfMonth(param).ToString();
                if (Month.Length == 1)
                    Month = "0" + Month;
                if (Day.Length == 1)
                    Day = "0" + Day;
                return Year + "/" + Month + "/" + Day + " " + miladi.Hour + ":" + miladi.Minute;
            }
            return String.Empty;
        }
        public static List<int> GetCurrntPerisnDate()
        {
            DateTime currentDateTime = DateTime.Now;
            System.Globalization.PersianCalendar Persian = new System.Globalization.PersianCalendar();
            List<int> listYear = new List<int>();
            int Year = Persian.GetYear(currentDateTime);
            int Month = Persian.GetMonth(currentDateTime);
            int Day = Persian.GetDayOfMonth(currentDateTime);
            listYear.Add(Year);
            listYear.Add(Month);
            listYear.Add(Day);
            return listYear;

        }

        public static string GetPersianDate(this DateTime miladi)
        {
            if (miladi != null)
            {
                DateTime param = Convert.ToDateTime(miladi);
                System.Globalization.PersianCalendar Persian = new System.Globalization.PersianCalendar();
                string Year = Persian.GetYear(param).ToString();
                string Month = Persian.GetMonth(param).ToString();
                string Day = Persian.GetDayOfMonth(param).ToString();
                if (Month.Length == 1)
                    Month = "0" + Month;
                if (Day.Length == 1)
                    Day = "0" + Day;
                return Year + Month + Day;
            }
            return String.Empty;
        }
        public static IEnumerable<Tuple<DateTime, DateTime>> EachDayFromDateTimes(DateTime from, DateTime to, IEnumerable<Tuple<short, TimeSpan?, TimeSpan?>> weekdays)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                foreach (var weekday in weekdays)
                    if ((short)day.DayOfWeek == weekday.Item1)
                        yield return new(weekday.Item2.HasValue ? day.Add(weekday.Item2.Value) : day,
                            weekday.Item3.HasValue ? day.Add(weekday.Item3.Value) : day);
        }

        public static string GetShamsiDateWithoutSlash(this DateTime miladi)
        {
            if (miladi != null)
            {
                DateTime param = Convert.ToDateTime(miladi);
                System.Globalization.PersianCalendar Persian = new System.Globalization.PersianCalendar();
                string Year = Persian.GetYear(param).ToString();
                string Month = Persian.GetMonth(param).ToString();
                string Day = Persian.GetDayOfMonth(param).ToString();
                if (Month.Length == 1)
                    Month = "0" + Month;
                if (Day.Length == 1)
                    Day = "0" + Day;
                return Year + "" + Month + "" + Day;
            }
            return String.Empty;
        }

        public static string GetMiladiDateString(string shamsiDate)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            var shamsiInfo = shamsiDate.Split('/');
            int year = int.Parse(shamsiInfo[0]);
            int month = int.Parse(shamsiInfo[1]);
            int day = int.Parse(shamsiInfo[2]);

            var newDateTime = new DateTime(year, month, day, new PersianCalendar());
            return $"{newDateTime.Year}-{newDateTime.Month}-{newDateTime.Day}";
        }
        public static string Base64Encode(string textToEncode)
        {
            byte[] textAsBytes = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(textAsBytes);
        }

        public static DateTime GetMiladiDate(string shamsiDate)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            var shamsiInfo = shamsiDate.Split('/');
            int year = int.Parse(shamsiInfo[0]);
            int month = int.Parse(shamsiInfo[1]);
            int day = int.Parse(shamsiInfo[2]);
            DateTime dt = new DateTime(year, month, day, pc);
            return dt;
        }

        public static string changePersianNumbersToEnglish(string input)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(persian[j], j.ToString());

            return input;
        }

        public static List<string> TextFormat = new List<string> { ".docx", ".pdf", ".text", ".xlsx" };
        public static List<string> VoicesFormat = new List<string> { ".mp3" };
        public static List<string> ImagesFormat = new List<string> { ".jpg", ".png", ".webp", ".jif" };


        public static string GetDetDayOfWeek(string culture, DayOfWeek dayOfWeek)
        {
            Dictionary<string, string[]> DayOfWeeks = new Dictionary<string, string[]>();
            DayOfWeeks.Add("en", new string[] { "Saturday", "Sunday", "Monday", "Tuesday", "Thursday", "Wednesday", "Friday" });
            //  DayOfWeeks.Add("fa", new string[] { "شنبه", "یک شنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه" });
            DayOfWeeks.Add("fa", new string[] { "جمعه", "پنج شنبه", "چهار شنبه", "سه شنبه", "دو شنبه", "یک شنبه", "شنبه" });
            var list = new List<string>() { "یک شنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه" };

            return /*DayOfWeeks[culture]*/ list[(int)dayOfWeek];
        }

        public static string GetMonthShamsi(int currentMonth)
        {
            var list = new List<string>() { "فروردین", "اردبیهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            return list[currentMonth - 1];
        }

        static Dictionary<char, char> LettersDictionary = new Dictionary<char, char>() {
            {'۰', '0'},
            {'۱', '1'},
            {'۲', '2'},
            {'۳', '3'},
            {'۴', '4'},
            {'۵', '5'},
            {'۶', '6'},
            {'۷', '7'},
            {'۸', '8'},
            {'۹', '9'},
            {'/', '/'}
        };


        public static string PersianToEnglishNumber(this string persianStr)
        {
            if (persianStr.Any(x => LettersDictionary.Keys.Contains(x)))
            {
                foreach (var item in persianStr)
                {
                    try { persianStr = persianStr.Replace(item, LettersDictionary[item]); }
                    catch { }
                }
            }
            return persianStr;
        }

        public static string EnglishNumberToPerainNumber(this string EnStr)
        {
            if (EnStr.Any(x => LettersDictionary.Values.Contains(x)))
            {
                foreach (var item in EnStr)
                {
                    try { EnStr = EnStr.Replace(item, LettersDictionary.FirstOrDefault(l => l.Value == item).Key); }
                    catch { }
                }
            }
            return EnStr;
        }


    }
}
