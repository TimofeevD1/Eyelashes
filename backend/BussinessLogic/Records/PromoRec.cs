﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Records
{
    public record PromoRec
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
        public string DiscountDescription { get; set; }
        public List<string> Benefits { get; set; }
    }
}
