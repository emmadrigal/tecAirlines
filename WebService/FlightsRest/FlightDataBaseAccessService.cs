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
                System.Diagnostics.Debug.WriteLine("SI PASAAAAAA  " + flightElement.Element("Price") + " //" );
                //The reservations are added
                //The stops are added
                this.addStops(flight, flightElement.Element("Stops").Elements());
                //The ATOs are added
                this.addATOs(flight, flightElement);
                //The departureDate is added
                DateTime dateDep = new DateTime();
                dateDep = dateDep.AddMinutes(Double.Parse(flightElement.Element("DepartureDate").Element("Minute").Value));
                dateDep = dateDep.AddHours(Double.Parse(flightElement.Element("DepartureDate").Element("Hour").Value));
                dateDep = dateDep.AddDays(Double.Parse(flightElement.Element("DepartureDate").Element("Day").Value)-1);
                dateDep = dateDep.AddMonths(Int32.Parse(flightElement.Element("DepartureDate").Element("Month").Value)-1);
                dateDep = dateDep.AddYears(Int32.Parse(flightElement.Element("DepartureDate").Element("Year").Value)-1);
                flight.DepartureDate = dateDep;
                //The arrivalDate is added
                DateTime aDate = new DateTime();
                aDate = aDate.AddMinutes(Double.Parse(flightElement.Element("ArrivalDate").Element("Minute").Value));
                aDate = aDate.AddHours(Double.Parse(flightElement.Element("ArrivalDate").Element("Hour").Value));
                aDate = aDate.AddDays(Double.Parse(flightElement.Element("ArrivalDate").Element("Day").Value) - 1);
                aDate = aDate.AddMonths(Int32.Parse(flightElement.Element("ArrivalDate").Element("Month").Value) - 1);
                aDate = aDate.AddYears(Int32.Parse(flightElement.Element("ArrivalDate").Element("Year").Value) - 1);
                flight.ArrivalDate = aDate;
                //The dearture ATO is added
                
                //Several simples attributes are added
                flight.ID = long.Parse(flightElement.Element("ID").Value);
                flight.Price = float.Parse(flightElement.Element("Price").Value);
                flight.Distance = long.Parse(flightElement.Element("Distance").Value);
                flight.Status = short.Parse(flightElement.Element("Status").Value);

                //The flight is added to the list
                list.Add(flight);
            }
            printList(list);
            return list;
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

        private void printList(List<Models.Flight> list)
        {
            foreach(Models.Flight f in list)
            {
                f.print();
            }
        }

    }//End of the class
}