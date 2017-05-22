using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableSchedule.Abstract
{
    public interface IDutyRepository
    {
        IEnumerable<Duty> DutyRepository { get; }

        void Insert(Duty duty);
        void Update(Duty duty);
        void Delete(Duty duty);
    }
}
