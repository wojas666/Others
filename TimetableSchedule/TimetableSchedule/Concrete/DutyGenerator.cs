using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableSchedule.Abstract;

namespace TimetableSchedule
{
    public class DutyGenerator
    {
        public void GenerateDuty(DateTime generateFrom, DateTime generateTo, IDutyRepository dutyRepository, IEmployesRepository employesRepository, IHolidaysRepository holidaysRepository)
        {
            DateTime dateToGenerate = generateFrom;

            while (dateToGenerate < generateTo)
            {
                if (!dateToGenerate.DayOfWeek.Equals(DayOfWeek.Sunday) && !dateToGenerate.DayOfWeek.Equals(DayOfWeek.Saturday))
                {
                    var lastThreeWorkDays = dutyRepository.DutyRepository.OrderByDescending(e => e.Date).Where(e => e.Date <= dateToGenerate).Take(3);

                    var employeeToDuty = employesRepository.EmployesRepository
                        .GetEmployeesWhoAreNotOnVacation(dateToGenerate)
                        .OrderBy(e => e.WorkDays)
                        .GetEmployeesWhoAreNotWorkInLastThreeDays(lastThreeWorkDays)
                        .OrderByDescending(e => e.LastThreeDays)
                        .Where(e => e.LastThreeDays < 3)
                        .FirstOrDefault();

                    var day = dutyRepository.DutyRepository.Where(e => e.Date.DayOfYear.Equals(dateToGenerate.DayOfYear))
                        .FirstOrDefault();

                    var holiday = holidaysRepository.HolidaysRepository
                        .Where(e => e.Date.DayOfYear.Equals(dateToGenerate.DayOfYear))
                        .FirstOrDefault();


                    employeeToDuty.WorkDays = employeeToDuty.WorkDays + 1;
                    employesRepository.Update(employeeToDuty);

                    if (day == null && holiday == null)
                        dutyRepository.Insert(new Duty { Date = dateToGenerate, EmployeeOnDuty_ID = employeeToDuty.Employee_ID });
                    else if (day != null && holiday == null)
                        dutyRepository.Update(new Duty { Date = dateToGenerate, EmployeeOnDuty_ID = employeeToDuty.Employee_ID });
                }
                
                // Increment to next day.
                dateToGenerate = dateToGenerate.AddDays(1);
            }
        }
        
        public void RecalculateWorksDays(DateTime To, IDutyRepository dutyRepository, IEmployesRepository employeeRepository)
        {
            foreach (Employee e in employeeRepository.EmployesRepository)
            {
                var dutyDays = dutyRepository.DutyRepository.Where(g => g.EmployeeOnDuty_ID == e.Employee_ID && g.Date <= To);
                e.WorkDays = dutyDays.Count();
            }
        }
    }
}
