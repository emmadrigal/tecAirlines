using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace reservationMicroservice.Controllers
{
    /**
     * This class controls the web connection for the reservation controller 
     */
    [RoutePrefix("Reservation")]
    public class ReservationController : ApiController
    {

        /**
         * This method recieves the web connection for the flight reservatio
         * @param the parameters for the new reservation
         * @return a boolean indicating if the operation was succesfull
        */
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetReservedFlights(string id)
        {
            ReservationAccessService dataLayer = new ReservationAccessService("reservations");

            return Ok(dataLayer.SelectReservation(id));
        }

        /**
         * This method recieves the web connection for the flight reservatio
         * @param the parameters for the new reservation
         * @return a boolean indicating if the operation was succesfull
        */
        [Route("{id}/{date}")]
        [HttpGet]
        public IHttpActionResult GetReservedFlights(string id, string date)
        {
            ReservationAccessService dataLayer = new ReservationAccessService("reservations");


            var datos = dataLayer.SelectReservation(id, date);
            if (datos != null)
            {
                return Ok(datos);
            }
            else
            {
                return InternalServerError();
            }
            
            
        }

        /**
         * This method recieves the web connection for the flight reservatio
         * @param the parameters for the new reservation
         * @return a boolean indicating if the operation was succesfull
        */
        [Route("create")]
        [HttpPost]
        public IHttpActionResult ReserveFlight([FromBody] Models.ReservationCreation newReservation)
        {
            ReservationAccessService dataLayer = new ReservationAccessService("reservations");
            dataLayer.ReserveFlight(newReservation.Username, newReservation.UserID, newReservation.FlightNumber, newReservation.Date);
            
            return Ok();
        }

        /**
         * This method receives the web connection for the cancelation of a reservation
         * @param reservation number to be cancelled
         * @return success of the operation
        */
        [Route("{reservationNumber}")]
        [HttpDelete]
        public IHttpActionResult CancelReservation(string reservationNumber)
        {
            ReservationAccessService dataLayer = new ReservationAccessService("reservations");

            return Ok(dataLayer.Update(reservationNumber, "cancelled", "status"));
        }

        /**
         * Method to pay for one or more reservations with miles from the user
         * @param reservation number to be payed with miles
         * @return success of the operation
         */
        [Route("pay/{reservationNumber}")]
        [HttpPut]
        public IHttpActionResult PayReservationMiles(string reservationNumber)
        {
            ReservationAccessService dataLayer = new ReservationAccessService("reservations");

            return Ok(dataLayer.Update(reservationNumber, "payed", "status"));
        }

        /**
         * Method to pay for one or more reservations with miles from the user
         * @param information about the car that will perform the payment
         * @return sucess of the operation
         */
        [Route("pay")]
        [HttpPost]
        public IHttpActionResult PayReservationCard([FromBody] Models.CardInfo card)
        {
            BankService bank = new BankService();
            if (bank.PaymentTransaction(card))
            {
                ReservationAccessService dataLayer = new ReservationAccessService("reservations");

                foreach(string reservationNumber in card.ReservationNumbers)
                {
                    dataLayer.Update(reservationNumber, "payed", "status");
                }

                return Ok(true);
            }
            return Ok(false);
        }
    }

}