using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableSchedule.Abstract;

namespace TimetableSchedule
{
    public static class MyExtensionMethod
    {
        public static IEnumerable<Employee> GetEmployeesWhoAreNotOnVacation(this IEnumerable<Employee> employes, DateTime workDay)
        {
            foreach (Employee e in employes)
            {
                if (workDay < e.Vacation.From || workDay > e.Vacation.To)
                    yield return e;
            }
        }

        public static IEnumerable<Employee> GetEmployeesWhoAreNotWorkInLastThreeDays(this IEnumerable<Employee> employes, IEnumerable<Duty> lastThreeWorkDays)
        {
            

            if (employes.Count() == 1)
                yield return employes.FirstOrDefault();

            foreach(Employee e in employes)
            {
                //e.LastThreeDays = 0;
                int works = 0;

                foreach (Duty d in lastThreeWorkDays)
                {
                    if (d.EmployeeOnDuty_ID == e.Employee_ID)
                        works++;
                }
                if(works == 3)
                {
                    e.LastThreeDays = works;
                }
                else if (e.LastThreeDays < 3 && works < 3)
                {
                    e.LastThreeDays = works;
                    yield return e;
                }
                else if(works == 0)
                {
                    e.LastThreeDays = 0;
                }
            }
        }
        
        public static bool CheckIfDateIsNotInVacationTime(this Employee employee, DateTime workDay)
        {
            if (workDay > employee.Vacation.From && workDay < employee.Vacation.To)
                return false;
            else
                return true;
        }
    }
}
