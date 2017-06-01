using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reservationMicroservice
{
    public class ReservationAccessService : DatabaseAccess
    {
        public Models.Reservation SelectReservation(string userName, DateTime date)
        {
            return null;
        }

        public List<Models.Reservation> SelectReservation(string userName)
        {
            return null;
        }

        public bool InsertSeat(string reservationNumber, int seat)
        {
            return true;

        }

        public bool ReserveFlight(string userName, List<string> userID, int flightNumber, DateTime date)
        {
            return true;
        }

        public bool InsertBagInfo(string reservationNumber, float weight)
        {
            return true;
        }



    }
}