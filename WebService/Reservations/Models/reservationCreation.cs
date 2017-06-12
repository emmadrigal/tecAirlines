using System;

namespace reservationMicroservice.Models
{
    /**
     * This class is used to recieve the parameters from the web co 
    */
    public class ReservationCreation
    { 
        public string Username { get; set; }//user to whom the reservations are asociated
        public string[] UserID { get; set; }//list of passangers who the reservations will be associated
        public int FlightNumber { get; set; }//Flight number
        public DateTime Date { get; set; }//Date of the flight

    }
}