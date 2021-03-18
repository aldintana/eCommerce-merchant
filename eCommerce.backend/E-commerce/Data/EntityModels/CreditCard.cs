﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.EntityModels
{
    public class CreditCard
    {
        public int ID { get; set; }
        public string CardType { get; set; }
        public int CardNumber { get; set; }
        public int ExpDate { get; set; }
        public string Expyear { get; set; }
        public string Cvc { get; set; }
        [ForeignKey(nameof(CustomerID))]
        public Customer Customer { get; set; }
        public string CustomerID { get; set; }
    }
}
