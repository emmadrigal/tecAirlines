using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace reservationMicroservice.Controllers
{
    public class ReservationController : ApiController
    {
        public IHttpActionResult ReserveFlight()
        {
            return Ok();

        }

        public IHttpActionResult CancelReservation(string reservationNumber)
        {
            return Ok();
        }

        public IHttpActionResult PayReservationMiles(string reservationNumber)
        {
            return Ok();
        }

        public IHttpActionResult PayReservationCard(long CardNumber, int securityCode, long amount)
        {
            return Ok();
        }

        private string GenerateReservationNumber()
        {
            return "F3C9AC";
        }
    }
}