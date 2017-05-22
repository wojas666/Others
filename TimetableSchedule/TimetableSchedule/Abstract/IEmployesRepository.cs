using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableSchedule.Abstract
{
    public interface IEmployesRepository
    {
        IEnumerable<Employee> EmployesRepository { get; }

        void Insert(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
