using System;
using System.Collections.Generic;

namespace Reto2eSgeG01.Core.Entities.DbViews
{
    public partial class SummaryOfSalesByQuarter
    {
        public DateTime? ShippedDate { get; set; }
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
