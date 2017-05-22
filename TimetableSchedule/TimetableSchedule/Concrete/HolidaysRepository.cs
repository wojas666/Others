using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableSchedule.Abstract;

namespace TimetableSchedule
{
    public class HolidaysRepository : IHolidaysRepository
    {
        private List<Holiday> context = new List<Holiday>();

        IEnumerable<Holiday> IHolidaysRepository.HolidaysRepository { get { return context; } }

        public void Delete(Holiday holiday)
        {
            if (context.Where(e => e.Date.Equals(holiday.Date)).FirstOrDefault() != null)
                context.Remove(holiday);
            else
                Console.WriteLine("Nie znaleziono takiego święta!");
        }

        public void Insert(Holiday holiday)
        {
            if (holiday.Date != null && !holiday.Name.Equals(String.Empty))
                context.Add(holiday);
            else
                Console.WriteLine("Proszę wprowadzić prawidłowe dane święta!");
        }

        public void Update(Holiday holiday)
        {
            if(holiday != null)
            {
                var _temp = context.Where(e => e.Name.Equals(holiday.Name)).FirstOrDefault();
                _temp.Date = holiday.Date;
            }
            else
            {
                Console.WriteLine("Proszę wprowadzić poprawne dane święta!");
            }
        }
    }
}
