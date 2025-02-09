using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Records
{
    public record BookingSlotRec
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public BookingSlotStatus Status { get; set; }
        public int? OrderId { get; set; }
    }
}
