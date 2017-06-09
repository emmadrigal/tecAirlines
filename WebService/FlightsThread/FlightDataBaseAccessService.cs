using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace FlightWebService
{
    /// <summary>
    /// This class is the reponsible to access to the flights that have been
    /// stored in the system.
    /// </summary>
    public class FlightDataBaseAccessService
    {
        //Direction of the XML file with the flights information
        private string path;

        /// <summary>
        /// Constructor set the path to use the temp folder of the PC
        /// </summary>
        public FlightDataBaseAccessService()
        {
            //Gets the temp file from the system
            string tempPath = Path.GetTempPath();
            path = tempPath +"flightsRead.xml";
        }//End of the constructor

        /// <summary>
        /// This method returns all the flights stored in the Database 
        /// </summary>
        /// <returns>list of flights</returns>
        public LinkedList<Flight> selectFlights()
        {
            //List to return
            LinkedList<Flight> list = new LinkedList<Flight>();
            //The xml file is loaded
            XmlDocument doc = new XmlDocument();
            System.Diagnostics.Debug.WriteLine("PATH: " + path);
            doc.Load(path);
            //They are got all the flights
            XmlNodeList flights = doc.SelectNodes("Flights/Flight");
            //Aux variable to store a flight node
            XmlNode flightNode;
            for(int i = 0; i < flights.Count; i++)
            {
                //The instance to store the flight
                Flight flight = new Flight();
                //The flight node i is got
                flightNode = flights.Item(i);
                //The ID is obtained
                flight.ID = Convert.ToInt64(flightNode.SelectSingleNode("ID").InnerText);
                //The departure date data list are got
                XmlNode depDate = flightNode.SelectSingleNode("DepartureDate");
                flight.DepartureDate = flight.DepartureDate.AddMinutes(Convert.ToDouble(depDate.SelectSingleNode("Minute").InnerText));
                flight.DepartureDate = flight.DepartureDate.AddHours(Convert.ToDouble(depDate.SelectSingleNode("Hour").InnerText));
                flight.DepartureDate = flight.DepartureDate.AddDays(Convert.ToDouble(depDate.SelectSingleNode("Day").InnerText)-1);
                flight.DepartureDate = flight.DepartureDate.AddMonths(Convert.ToInt32(depDate.SelectSingleNode("Month").InnerText) - 1);
                flight.DepartureDate = flight.DepartureDate.AddYears(Convert.ToInt32(depDate.SelectSingleNode("Year").InnerText) - 1);
                //The arrival date data list are got
                XmlNode arrivalDate = flightNode.SelectSingleNode("ArrivalDate");
                flight.ArrivalDate = flight.ArrivalDate.AddMinutes(Convert.ToDouble(arrivalDate.SelectSingleNode("Minute").InnerText));
                flight.ArrivalDate = flight.ArrivalDate.AddHours(Convert.ToDouble(arrivalDate.SelectSingleNode("Hour").InnerText));
                flight.ArrivalDate = flight.ArrivalDate.AddDays(Convert.ToDouble(arrivalDate.SelectSingleNode("Day").InnerText) - 1);
                flight.ArrivalDate = flight.ArrivalDate.AddMonths(Convert.ToInt32(arrivalDate.SelectSingleNode("Month").InnerText) - 1);
                flight.ArrivalDate = flight.ArrivalDate.AddYears(Convert.ToInt32(arrivalDate.SelectSingleNode("Year").InnerText) - 1);
                //The airline is added to the flight
                XmlNode airlineNode = flightNode.SelectSingleNode("Airline");
                flight.Airline = new Airline();
                flight.Airline.ID_Airline = Convert.ToInt64(airlineNode.SelectSingleNode("ID_Airline").InnerText);
                flight.Airline.Name = airlineNode.SelectSingleNode("Name").InnerText;
                //The distance is added to the flight
                flight.Distance = Convert.ToInt64(flightNode.SelectSingleNode("Distance").InnerText);
                //The price is added to the flight
                flight.Price = float.Parse(flightNode.SelectSingleNode("Price").InnerText);
                //The status is added to the flight
                flight.Status = Convert.ToInt16(flightNode.SelectSingleNode("Status").InnerText);
                //The departure ATO is added to the flight
                XmlNode depAtoNode = flightNode.SelectSingleNode("DepartureATO");
                Airport depAto = new Airport();
                depAto.ID_Airport = Convert.ToInt64(depAtoNode.SelectSingleNode("ID_Airport").InnerText);
                depAto.Name = depAtoNode.SelectSingleNode("Name").InnerText;
                depAto.Location = depAtoNode.SelectSingleNode("Location").InnerText;
                depAto.Code = depAtoNode.SelectSingleNode("Code").InnerText;
                flight.DepartureATO = depAto;
                //The arrival ATO is added to the flight
                XmlNode arrivalAtoNode = flightNode.SelectSingleNode("ArrivalATO");
                Airport arrivalAto = new Airport();
                arrivalAto.ID_Airport = Convert.ToInt64(arrivalAtoNode.SelectSingleNode("ID_Airport").InnerText);
                arrivalAto.Name = arrivalAtoNode.SelectSingleNode("Name").InnerText;
                arrivalAto.Location = arrivalAtoNode.SelectSingleNode("Location").InnerText;
                arrivalAto.Code = arrivalAtoNode.SelectSingleNode("Code").InnerText;
                flight.ArrivalATO = arrivalAto;
                //The flight is added to the list
                list.AddLast(flight);
            }//End of for
            //printFlights(list);
            return list;
        }//End of method

        /// <summary>
        /// This method modifies the status of the flight whose id
        /// is given by the id_flight
        /// </summary>
        /// <param name="id_flight">id of the flight whose status will be changed</param>
        public void modifyStatusXML(long id_flight, short newStatus) 
        {
            //The xml file is loaded
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            //A list with the flights is gotten
            XmlNodeList flightNodes = doc.SelectNodes("Flights/Flight");
            foreach(XmlNode flightNode in flightNodes)
            {
                long id_aux = Convert.ToInt64(flightNode.SelectSingleNode("ID").InnerText);
                if (id_aux == id_flight)
                {
                    XmlNode nodeStatus = flightNode.SelectSingleNode("Status");
                    nodeStatus.InnerText = newStatus.ToString();
                    break;
                }
            }//End of foreach 
            //The changes are saved
            doc.Save(path);
        }//End of the method

        /// <summary>
        /// Print the flights in the list
        /// </summary>
        /// <param name="list">list of flights</param>
        private void printFlights(LinkedList<Flight> list)
        {
            foreach (Flight item in list)
            {
                item.print();
            }//End of the for
        }//End of the method

        public void createXML()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Flights");
            doc.AppendChild(root);

            //Adds the flight
            XmlElement flight1 = doc.CreateElement("Flight");
            root.AppendChild(flight1);

            //Adds the id to the Flight1
            XmlElement id = doc.CreateElement("ID");
            id.AppendChild(doc.CreateTextNode("1"));
            flight1.AppendChild(id);

            XmlElement distance = doc.CreateElement("Distance");
            distance.AppendChild(doc.CreateTextNode("1001"));
            flight1.AppendChild(distance);

            XmlElement price = doc.CreateElement("Price");
            price.AppendChild(doc.CreateTextNode("200"));
            flight1.AppendChild(price);

            DateTime date1 = new DateTime();
            date1 = date1.AddDays(5);
            date1 = date1.AddYears(2016);
            date1 = date1.AddMonths(5);
            XmlElement dateDeparture = doc.CreateElement("DepartureDate");
            XmlElement dateHour = doc.CreateElement("Hour");
            XmlElement dateDay = doc.CreateElement("Day");
            XmlElement dateMonth = doc.CreateElement("Month");
            XmlElement dateYear = doc.CreateElement("Year");
            //Data are added to the Element
            dateHour.AppendChild(doc.CreateTextNode(date1.Hour.ToString()));
            dateDay.AppendChild(doc.CreateTextNode(date1.Day.ToString()));
            dateMonth.AppendChild(doc.CreateTextNode(date1.Month.ToString()));
            dateYear.AppendChild(doc.CreateTextNode(date1.Year.ToString()));
            //Elements are added to the date Departure
            dateDeparture.AppendChild(dateHour);
            dateDeparture.AppendChild(dateDay);
            dateDeparture.AppendChild(dateMonth);
            dateDeparture.AppendChild(dateYear);
            //Date departure are added to the flight
            flight1.AppendChild(dateDeparture);

            //System.Diagnostics.Debug.WriteLine("fecha " + date1);

            //Save the xml file in the directory of the project
            doc.Save("C:\\Users\\Jonathan\\Documents\\Visual Studio 2015\\Projects\\FlightWebService\\FlightWebService\\flights.xml");
        }

    }//End of the class
}
