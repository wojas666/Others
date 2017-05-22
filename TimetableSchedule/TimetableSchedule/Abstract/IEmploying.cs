using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableSchedule.Abstract
{
    public interface IEmploying
    {
        int Employee_ID { get; set; }
        Vacation Vacation { get; set; }
        int WorkDays { get; set; }
        int LastThreeDays { get; set; }
    }

    public struct Vacation
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
