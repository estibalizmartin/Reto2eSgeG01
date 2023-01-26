using System;
using System.Collections.Generic;

namespace Reto2eSgeG01.Core.Entities.DbViews
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
    }
}
