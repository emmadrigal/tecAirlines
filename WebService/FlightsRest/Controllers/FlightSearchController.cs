using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TecAirlines.Controllers
{
    /// <summary>
    /// This controller manage the directions to access to the
    /// flights through the microservice.
    /// </summary>
    [RoutePrefix("Flight")]
    public class FlightSearchController : ApiController
    {
       
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult getFlight(long id)
        {
            var dato = "Un Vuelo";
            if (dato == null)
            {
                return NotFound();
            }
            return Ok(dato);
        }//End of getFlights

        [Route("~/getAllFlights")]
        [HttpGet]
        public IHttpActionResult getAllFlights()
        {
            FlightDataBaseAccessService service = new FlightDataBaseAccessService("flightsRead");
            var dato = service.getAllFlights();
            return Ok(dato);
        }//End of getAllFlights

    }//End of the controller
}
