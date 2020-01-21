using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public class CommonClasses
    {
        public CommonClasses()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string ConvertDateToYMD(string value)
        {
            string[] arrTime = null;
            string[] arrDate = null;
            string result = "";
            string sysDate = LastDayOfMonthFromDateTime(System.DateTime.Now).ToString();
            string sysDay = FindDayFromDateTime(LastDayOfMonthFromDateTime(System.DateTime.Now)).ToString("D2");
            string sysMth = FindMonthFromDateTime(LastDayOfMonthFromDateTime(System.DateTime.Now)).ToString("D2");
            string sysYear = FindYearFromDateTime(LastDayOfMonthFromDateTime(System.DateTime.Now)).ToString();
            string sysDateFormat = "";

            string[] arrSysTime = null;
            string[] arrSysDate = null;

            arrSysTime = sysDate.Split(' ');
            if (arrSysTime.Length > 1)
            {
                arrSysDate = arrSysTime[0].Split('/');
            }
            else
            {
                arrSysDate = sysDate.Split('/');
            }

            if (arrSysDate[0] == sysYear)
            {
                if (arrSysDate[2] == sysDay)
                {
                    sysDateFormat = "YYYYMMDD";
                }
                else
                {
                    throw new Exception("Date value is not a DateTime data type.");
                }
            }
            else
            {
                if (arrSysDate[0] == sysDay)
                {
                    if (arrSysDate[1] == sysYear)
                    {
                        throw new Exception("Date value is not a DateTime data type.");
                    }
                    else
                    {
                        sysDateFormat = "DDMMYYYY";
                    }
                }
                else
                {
                    if (arrSysDate[1] == sysYear)
                    {
                        //Should not happen
                        throw new Exception("Date value is not a DateTime data type.");
                    }
                    else
                    {
                        if (arrSysDate[1] == sysDay)
                        {
                            sysDateFormat = "MMDDYYYY";
                        }
                        else
                        {
                            throw new Exception("Date value is not a DateTime data type.");
                        }
                    }
                }
            }

            arrTime = value.Split(' ');
            if (arrTime.Length > 1)
            {
                arrDate = arrTime[0].Split('/');
                switch (sysDateFormat)
                {
                    case "YYYYMMDD":
                        // ERROR: Case labels with binary operators are unsupported : Equality
                        //result = arrDate[0] + "/" + Convert.ToInt32(arrDate[1]).ToString("D2") + "/" + Convert.ToInt32(arrDate[2]).ToString("D2") + " " + arrTime[1] + " " + arrTime[2];
                        result = arrDate[0] + "/" + Convert.ToInt32(arrDate[1]).ToString("D2") + "/" + Convert.ToInt32(arrDate[2]).ToString("D2") + " " + arrTime[1];
                        break;
                    case "MMDDYYYY":
                        // ERROR: Case labels with binary operators are unsupported : Equality   
                        //result = arrDate[2] + "/" + Convert.ToInt32(arrDate[0]).ToString("D2") + "/" + Convert.ToInt32(arrDate[1]).ToString("D2") + " " + arrTime[1] + " " + arrTime[2];
                        result = arrDate[2] + "/" + Convert.ToInt32(arrDate[0]).ToString("D2") + "/" + Convert.ToInt32(arrDate[1]).ToString("D2") + " " + arrTime[1];
                        break;
                    case "DDMMYYYY":
                        // ERROR: Case labels with binary operators are unsupported : Equality    
                        //result = arrDate[2] + "/" + Convert.ToInt32(arrDate[1]).ToString("D2") + "/" + Convert.ToInt32(arrDate[0]).ToString("D2") + " " + arrTime[1] + " " + arrTime[2];
                        result = arrDate[2] + "/" + Convert.ToInt32(arrDate[1]).ToString("D2") + "/" + Convert.ToInt32(arrDate[0]).ToString("D2") + " " + arrTime[1];
                        break;
                }
            }
            else
            {
                arrDate = value.Split('/');
                switch (sysDateFormat)
                {
                    case "YYYYMMDD":
                        // ERROR: Case labels with binary operators are unsupported : Equality   
                        result = arrDate[0] + "/" + Convert.ToInt32(arrDate[1]).ToString("D2") + "/" + Convert.ToInt32(arrDate[2]).ToString("D2");
                        break;
                    case "MMDDYYYY":
                        // ERROR: Case labels with binary operators are unsupported : Equality    
                        result = arrDate[2] + "/" + Convert.ToInt32(arrDate[0]).ToString("D2") + "/" + Convert.ToInt32(arrDate[1]).ToString("D2");
                        break;
                    case "DDMMYYYY":
                        // ERROR: Case labels with binary operators are unsupported : Equality
                        result = arrDate[2] + "/" + Convert.ToInt32(arrDate[1]).ToString("D2") + "/" + Convert.ToInt32(arrDate[0]).ToString("D2");
                        break;
                }
            }

            return result;
        }

        public static DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        public static int FindDayFromDateTime(DateTime dateTime)
        {
            int day = 0;
            day = dateTime.Day;
            return day;
        }

        public static int FindMonthFromDateTime(DateTime dateTime)
        {
            int month = 0;
            month = dateTime.Month;
            return month;
        }

        public static int FindYearFromDateTime(DateTime dateTime)
        {
            int year = 0;
            year = dateTime.Year;
            return year;
        }
    }
}
