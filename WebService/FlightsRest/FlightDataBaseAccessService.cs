using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TecAirlines
{
    /// <summary>
    /// This class provides connection to the data layer for the flights microservice
    /// </summary>
    public class FlightDataBaseAccessService : DatabaseAccess
    {
        /// <summary>
        /// This constructor calls to the one of the super clase 
        /// </summary>
        /// <param name="filename">name of the file</param>
        public FlightDataBaseAccessService(string filename) : base(filename){}

        /// <summary>
        /// Method get all the flights stored in the XML
        /// </summary>
        /// <returns>list of flights</returns>
        public List<Models.Flight> getAllFlights()
        {
            List<Models.Flight> list = new List<Models.Flight>();
            List<XElement> flightsElements = GetAllElements();
            foreach (XElement flightElement in flightsElements)
            {
                Models.Flight flight = new Models.Flight();
                //The reservations are added
                this.addReservations(flight, flightElement.Element("Reservations").Elements());
                //The Airline is added
                this.addAirline(flight, flightElement.Element("Airline"));
                //The stops are added
                this.addStops(flight, flightElement.Element("Stops").Elements());
                //The ATOs are added
                this.addATOs(flight, flightElement);
                //The departureDate is added
                this.addDepartureDate(flight, flightElement.Element("DepartureDate"));
                //The arrivalDate is added
                this.addArrivalDate(flight, flightElement.Element("ArrivalDate"));                
                //Several simples attributes are added
                flight.ID = long.Parse(flightElement.Element("ID").Value);
                flight.Price = float.Parse(flightElement.Element("Price").Value);
                flight.Distance = long.Parse(flightElement.Element("Distance").Value);
                flight.Status = short.Parse(flightElement.Element("Status").Value);
                //The flight is added to the list
                list.Add(flight);
            }
            return list;
        }//End of the method

        /// <summary>
        /// Adds the departure date to the flight
        /// </summary>
        /// <param name="flight">flight whose departure date is going to be added</param>
        /// <param name="element">Corresponds to the element of the departure Date</param>
        private void addDepartureDate(Models.Flight flight, XElement element)
        {
            //The departureDate is added
            DateTime dateDep = new DateTime();
            dateDep = dateDep.AddMinutes(Double.Parse(element.Element("Minute").Value));
            dateDep = dateDep.AddHours(Double.Parse(element.Element("Hour").Value));
            dateDep = dateDep.AddDays(Double.Parse(element.Element("Day").Value) - 1);
            dateDep = dateDep.AddMonths(Int32.Parse(element.Element("Month").Value) - 1);
            dateDep = dateDep.AddYears(Int32.Parse(element.Element("Year").Value) - 1);
            flight.DepartureDate = dateDep;
        }//End of the method

        /// <summary>
        /// Adds the arrival date to the flight
        /// </summary>
        /// <param name="flight">flight whose departure date is going to be added</param>
        /// <param name="element">Corresponds to the element of the arrival Date</param>
        private void addArrivalDate(Models.Flight flight, XElement element)
        {
            //The arrivalDate is added
            DateTime arrivalDate = new DateTime();
            arrivalDate = arrivalDate.AddMinutes(Double.Parse(element.Element("Minute").Value));
            arrivalDate = arrivalDate.AddHours(Double.Parse(element.Element("Hour").Value));
            arrivalDate = arrivalDate.AddDays(Double.Parse(element.Element("Day").Value) - 1);
            arrivalDate = arrivalDate.AddMonths(Int32.Parse(element.Element("Month").Value) - 1);
            arrivalDate = arrivalDate.AddYears(Int32.Parse(element.Element("Year").Value) - 1);
            flight.ArrivalDate = arrivalDate;
        }//End of the method

        /// <summary>
        /// Adds the stop list of the flight
        /// </summary>
        /// <param name="flight">flight</param>
        /// <param name="elements">list of elements with every id stop</param>
        /// <returns>the flight with the stops added</returns>
        private void addStops(Models.Flight flight, IEnumerable<XElement> elements)
        {
            foreach(XElement element in elements)
            {
                //The id stop is obtained and added to the flight 
                flight.Stops.AddLast(long.Parse(element.Value));
            }
        }//End of the method

        /// <summary>
        /// Adds the id reservation list of the flight
        /// </summary>
        /// <param name="flight">flight</param>
        /// <param name="elements">list of elements with every id reservation</param>
        /// <returns>the flight with the reservations added</returns>
        private void addReservations(Models.Flight flight, IEnumerable<XElement> elements)
        {
            foreach (XElement element in elements)
            {
                //The id reservation is obtained and added to the flight 
                flight.Reservations.AddLast(long.Parse(element.Value));
            }
        }//End of the method

        /// <summary>
        /// Adds the ATOs of the flight
        /// </summary>
        /// <param name="flight">flight</param>
        /// <param name="element"> elements of the flight</param>
        /// <returns>the flight with the ATOs added</returns>
        private void addATOs(Models.Flight flight, XElement element)
        {
            //The departure ato is added
            Models.Airport depATO = new Models.Airport();
            XElement depATOElement = element.Element("DepartureATO");
            depATO.ID_Airport = long.Parse(depATOElement.Element("ID_Airport").Value);
            depATO.Name = depATOElement.Element("Name").Value;
            depATO.Location = depATOElement.Element("Location").Value;
            depATO.Code = depATOElement.Element("Code").Value;
            flight.DepartureATO = depATO;
            //The arrival ato is added
            Models.Airport aATO = new Models.Airport();
            XElement arrivalATOElement = element.Element("ArrivalATO");
            aATO.ID_Airport = long.Parse(arrivalATOElement.Element("ID_Airport").Value);
            aATO.Name = arrivalATOElement.Element("Name").Value;
            aATO.Location = arrivalATOElement.Element("Location").Value;
            aATO.Code = arrivalATOElement.Element("Code").Value;
            flight.ArrivalATO = aATO;
        }//End of the method

        /// <summary>
        /// Adds the Airline of the flight
        /// </summary>
        /// <param name="flight">flight</param>
        /// <param name="element"> element of the flight</param>
        /// <returns>the flight with the Airline added</returns>
        private void addAirline(Models.Flight flight, XElement element)
        {
            Models.Airline airline = new Models.Airline();
            airline.ID_Airline = long.Parse(element.Element("ID_Airline").Value);
            airline.Name = element.Element("Name").Value;
            //The airline of the flight is setted
            flight.Airline = airline;
        }//End of the method

        private void printList(List<Models.Flight> list)
        {
            foreach(Models.Flight f in list)
            {
                f.print();
            }
        }

    }//End of the class
}