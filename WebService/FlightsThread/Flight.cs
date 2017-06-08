using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebService
{
    /// <summary>
    /// Model that stores the information of a flight
    /// </summary>
    public class Flight: Observer
    {
        //Attributes of a Flight
        private long id;
        private Airport departureATO;
        private Airport arrivalATO;
        private Airline airline;
        private DateTime departureDate;
        private DateTime arrivalDate;
        private LinkedList<long> stops;
        private string airplane;
        private LinkedList<string> reservations;
        /// <summary>
        /// 0 created
        /// 1 opened
        /// 2 closed
        /// 3 canceled
        /// </summary>
        private short status;
        private long distance;
        private float price;

        //Properties to access to the class attributes
        /// <summary>
        /// Property to access and modify the id of the instance
        /// </summary>
        public long ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Property to access and modify the departure airport of the flight
        /// </summary>
        public Airport DepartureATO
        {
            get { return departureATO; }
            set { departureATO = value; }
        }
        /// <summary>
        /// Property to access and modify the arrival airport of the flight
        /// </summary>
        public Airport ArrivalATO
        {
            get { return arrivalATO; }
            set { arrivalATO = value; }
        }
        /// <summary>
        /// Property to access and modify the airline of the flight
        /// </summary>
        public Airline Airline
        {
            get { return airline; }
            set { airline = value; }
        }
        /// <summary>
        /// Property to access and modify the departure date of the flight
        /// </summary>
        public DateTime DepartureDate
        {
            get { return departureDate; }
            set { departureDate = value; }
        }
        /// <summary>
        /// Property to access and modify the arrival date of the flight
        /// </summary>
        public DateTime ArrivalDate
        {
            get { return arrivalDate; }
            set { arrivalDate = value; }
        }
        /// <summary>
        /// Property to add and remove stops of the flight
        /// </summary>
        public LinkedList<long> Stops
        {
            get { return stops; }
            set { stops = value; }
        }
        /// <summary>
        /// Property to access and modify the airplane of the flight
        /// </summary>
        public string Airplane
        {
            get { return airplane; }
            set { airplane = value; }
        }
        /// <summary>
        /// Property to add and remove reservations of the flight
        /// </summary>
        public LinkedList<string> Reservations
        {
            get { return reservations; }
            set { reservations = value; }
        }
        /// <summary>
        /// Property to access and modify the status of the flight
        /// </summary>
        public short Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// Property to access and modify the distance of the flight
        /// </summary>
        public long Distance
        {
            get { return distance; }
            set
            {
                if (value > 0)
                    distance = value;
                else
                    System.Diagnostics.Debug.WriteLine("Error in the Flight class: method setDistance",
                        " has received a negative value.");
            }
        }
        /// <summary>
        /// Property to access and modify the price of the flight
        /// </summary>
        public float Price
        {
            get { return price; }
            set
            {
                if (value > 0)
                    price = value;
                else
                    System.Diagnostics.Debug.WriteLine("Error in the Flight class: method setPrice",
                        " has received a negative value.");
            }
        }

        /// <summary>
        /// This method prints the information of the flight 
        /// </summary>
        public void print()
        {
            System.Diagnostics.Debug.WriteLine("------Flight Data------");
            System.Diagnostics.Debug.WriteLine("ID: " + this.id);
            System.Diagnostics.Debug.WriteLine("Departure ATO: ");
            System.Diagnostics.Debug.WriteLine("    ID: " + this.departureATO.ID_Airport);
            System.Diagnostics.Debug.WriteLine("    Name: " + this.departureATO.Name);
            System.Diagnostics.Debug.WriteLine("    Location: " + this.departureATO.Location);
            System.Diagnostics.Debug.WriteLine("    Code: " + this.departureATO.Code);
            System.Diagnostics.Debug.WriteLine("Arrival ATO: ");
            System.Diagnostics.Debug.WriteLine("    ID: " + this.arrivalATO.ID_Airport);
            System.Diagnostics.Debug.WriteLine("    Name: " + this.arrivalATO.Name);
            System.Diagnostics.Debug.WriteLine("    Location: " + this.arrivalATO.Location);
            System.Diagnostics.Debug.WriteLine("    Code: " + this.arrivalATO.Code);
            System.Diagnostics.Debug.WriteLine("Airline: ");
            System.Diagnostics.Debug.WriteLine("    ID: " + this.airline.ID_Airline);
            System.Diagnostics.Debug.WriteLine("    Name: " + this.airline.Name);
            System.Diagnostics.Debug.WriteLine("Departure Date: ");
            System.Diagnostics.Debug.WriteLine("    Hours: " + this.departureDate.Hour);
            System.Diagnostics.Debug.WriteLine("    Minutes: " + this.departureDate.Minute);
            System.Diagnostics.Debug.WriteLine("    Day: " + this.departureDate.Day);
            System.Diagnostics.Debug.WriteLine("    Month: " + this.departureDate.Month);
            System.Diagnostics.Debug.WriteLine("    Year: " + this.departureDate.Year);
            System.Diagnostics.Debug.WriteLine("Arrival Date: ");
            System.Diagnostics.Debug.WriteLine("    Hours: " + this.arrivalDate.Hour);
            System.Diagnostics.Debug.WriteLine("    Minutes: " + this.arrivalDate.Minute);
            System.Diagnostics.Debug.WriteLine("    Day: " + this.arrivalDate.Day);
            System.Diagnostics.Debug.WriteLine("    Month: " + this.arrivalDate.Month);
            System.Diagnostics.Debug.WriteLine("    Year: " + this.arrivalDate.Year);
            System.Diagnostics.Debug.WriteLine("Airplane: " + this.airplane);
            System.Diagnostics.Debug.WriteLine("Status: " + this.status);
            System.Diagnostics.Debug.WriteLine("Distance: " + this.distance);
            System.Diagnostics.Debug.WriteLine("Price: " + this.price);
        }//End of the method

        void Observer.update()
        {
            
        }
    }//End of the Flight class
}
