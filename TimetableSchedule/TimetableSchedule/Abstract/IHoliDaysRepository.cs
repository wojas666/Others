using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableSchedule.Abstract
{
    public interface IHolidaysRepository
    {
        IEnumerable<Holiday> HolidaysRepository { get; }

        void Insert(Holiday holiday);
        void Update(Holiday holiday);
        void Delete(Holiday holiday);
    }
}
