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

        public string ReservationNumber { get => reservationNumber; set => reservationNumber = value; }
        public long IdFlight { get => idFlight; set => idFlight = value; }
        public int SeatNumber { get => seatNumber; set => seatNumber = value; }
    }
}