using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Records
{
    public record BookingCalendarRec
    {
        public DateTime Date { get; set; }
        public List<BookingSlotRec> Slots { get; set; }
    }
}
