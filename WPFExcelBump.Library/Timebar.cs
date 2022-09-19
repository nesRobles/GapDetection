using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExcelBump.Library
{
    public class Timebar
    {
        public static List<DateTime> CreateTimeBar()
        {
            List<DateTime> timebar = new();

            timebar.Add(DateTime.Parse("6:00:00 AM"));

            //120 forloop stops at 2:00am
            for (int i = 0; i < 120; i++)
            {
                timebar.Add(timebar[i].AddMinutes(10));
            }

            return timebar;
        }
    }
}
