using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reservationMicroservice.Models
{
    /**
     * This class is used to recieve the parameters from the web co 
    */
    public class ReservationCreation
    { 
        
        private string userName;//user to whom the reservations are asociated
        private string[] userID;//list of passangers who the reservations will be associated
        private int flightNumber;//Flight number
        private DateTime date;//Date of the flight

        public string Username { get; set; }
        public string[] UserID { get; set; }
        public int FlightNumber { get; set; }
        public DateTime Date { get; set; }

    }
}