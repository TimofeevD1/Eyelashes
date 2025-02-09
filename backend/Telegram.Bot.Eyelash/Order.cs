using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Eyelash
{
    public class Order
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Service { get; set; }
        public TimeSpan AppointmentTime { get; set; }
    }
}
