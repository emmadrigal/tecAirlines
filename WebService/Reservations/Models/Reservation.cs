using System;

namespace reservationMicroservice.Models
{
    /*
     * This class holds the information for a reservation
     */
    public class Reservation
    {
        public string ReservationNumber { get; set; }
        public long IDFlight { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}