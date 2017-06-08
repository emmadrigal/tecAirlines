using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TecAirlines.Controllers
{
    /// <summary>
    /// Controller responsible to change the status of the flights, it means
    /// he should open and close the flight according to their departure date.
    /// </summary>
    [RoutePrefix("Flights")]
    public class AutomaticFlightController : ApiController
    {
        /// <summary>
        /// This is the method that is always running to check the date and 
        /// </summary>
        public void loop()
        {

        }//End of loop 
    }//End of AutomaticFlightController 
}