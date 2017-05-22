using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableSchedule.Abstract;

namespace TimetableSchedule
{
    public class EmployesRepository : IEmployesRepository
    {
        List<Employee> context = new List<Employee>();

        IEnumerable<Employee> IEmployesRepository.EmployesRepository { get { return context; } }

        public void Delete(Employee employee)
        {
            context.Remove(employee);
        }

        public void Insert(Employee employee)
        {
            int lastID = -1;

            if (context.Count > 0)
            {
                lastID = context.OrderByDescending(e => e.Employee_ID).FirstOrDefault().Employee_ID;
            }

            employee.Employee_ID = lastID+1;
            context.Add(employee);
        }

        public void Update(Employee employee)
        {
            var record = context.Where(e => e.Employee_ID == employee.Employee_ID).FirstOrDefault();

            if(record != null)
            {
                record.Age = employee.Age;
                record.WorkDays = employee.WorkDays;
                record.FirstName = employee.FirstName;
                record.LastName = employee.LastName;
                record.Vacation = employee.Vacation;
            }
        }
    }
}
