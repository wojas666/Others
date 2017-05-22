using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableSchedule.Abstract;

namespace TimetableSchedule
{
    public class Employee : IPersonal, IEmploying
    {
        public int Employee_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int WorkDays { get; set; }
        public int LastThreeDays { get; set; }

        public Vacation Vacation { get; set; }
    }
}
