using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reservationMicroservice.Models
{
    public class CardInfo
    {
        string[] reservationNumbers;
        private long cardNumber;
        private int securityCode;
        private long amount;

        public string[] ReservationNumbers { get; set; }
        public long CardNumber { get; set; }
        public int SecurityCode { get; set; }
        public long Amount { get; set; }
    }
}