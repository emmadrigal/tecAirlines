using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reservationMicroservice.Models
{
    public class Reservation
    {
        private string reservationNumber;
        private long idFlight;
        private int seatNumber;
        private string status;
        private string date;

        public string ReservationNumber { get; set; }
        public long IDFlight { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}