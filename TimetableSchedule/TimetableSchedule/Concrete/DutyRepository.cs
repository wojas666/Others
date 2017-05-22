using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableSchedule.Abstract;

namespace TimetableSchedule
{
    public class DutyRepository : IDutyRepository
    {
        private List<Duty> context = new List<Duty>();

        IEnumerable<Duty> IDutyRepository.DutyRepository { get { return context; } }

        public void Delete(Duty duty)
        {
            if (duty != null)
                context.Remove(duty);
        }

        public void Insert(Duty duty)
        {
            if (duty != null)
                context.Add(duty);
        }

        public void Update(Duty duty)
        {
            if (duty != null)
            {
                var _temp = context.Where(e => e.Date.DayOfYear.Equals(duty.Date.DayOfYear)).FirstOrDefault();

                if (_temp != null)
                {
                    _temp.EmployeeOnDuty_ID = duty.EmployeeOnDuty_ID;
                }
                else
                {
                    Console.WriteLine("Nie znaleziono żadnego rekordu dyżuru do edycji!");
                }
            }
            else
            {
                Console.WriteLine("Nie wprowadzono danych dyżuru do edycji!");
            }
        }
    }
}
