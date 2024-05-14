﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderingSystemApp.DtoLayer.OrderDto
{
    public class GetOrderDto
    {
        public int OrderID { get; set; }
        //[Column(TypeName = "Date")]
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalPrice { get; set; }
        public bool Status { get; set; }
        public int MenuTableID { get; set; }
    }
}
