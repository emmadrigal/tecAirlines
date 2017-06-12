using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace reservationMicroservice
{
    /**
     * This class provides connection to the data layer for the reservations microservice
     */
    public class ReservationAccessService : DatabaseAccess
    {

        static private long reservationNumber = 0;


        public ReservationAccessService(string filename) : base(filename){}

        /**
        * Gets information about a specific reservation7
        * @param userName who owns the reservation
        * @param date of the reservation
        * @return information of the reservation
        */
        public List<Models.Reservation> SelectReservation(string userName, string date)
        {
            //Checkea que la fecha venga en el formato correcto
            DateTime fecha;
            try
            {
                //Le da el correcto formato a la fecha
                fecha = DateTime.Parse(date);
            }
            catch
            {
                return null;
            }

            List<string> attributes = new List<string>
            {
                "userName",
                "date"
            };
            List<string> keys = new List<string>
            {
                userName,
                fecha.ToString()
            };

            List<Models.Reservation> reservationReturnList = new List<Models.Reservation>();

            List<XElement> reservationsElements = GetElement(attributes, keys);
            foreach(XElement reservationElement in reservationsElements)
            {
                Models.Reservation reservation = new Models.Reservation()
                {
                    ReservationNumber = reservationElement.Attribute("reservationNumber").Value,
                    IDFlight = long.Parse(reservationElement.Attribute("flightNumber").Value),
                    SeatNumber = int.Parse(reservationElement.Attribute("seatNumber").Value),
                    Status = reservationElement.Attribute("status").Value,
                    Date = DateTime.Parse(reservationElement.Attribute("date").Value)
                };

                reservationReturnList.Add(reservation);
            }

            return reservationReturnList;
        }

        /**
         * Gets information about all the reservations made by a user
         * @param userName whose resrvations will be shown
         * @return List of flights of the user
         */
        public List<Models.Reservation> SelectReservation(string userName)
        {
            List<string> attributes = new List<string>
            {
                "userName"
            };
            List<string> keys = new List<string>
            {
                userName
            };

            List<Models.Reservation> reservationReturnList = new List<Models.Reservation>();

            List<XElement> reservationsElements = GetElement(attributes, keys);

            foreach (XElement reservationElement in reservationsElements)
            {
                Models.Reservation reservation = new Models.Reservation()
                {
                    ReservationNumber = reservationElement.Attribute("reservationNumber").Value,
                    IDFlight = long.Parse(reservationElement.Attribute("flightNumber").Value),
                    SeatNumber = int.Parse(reservationElement.Attribute("seatNumber").Value),
                    Status = reservationElement.Attribute("status").Value,
                    Date = DateTime.Parse(reservationElement.Attribute("date").Value)
                };

                reservationReturnList.Add(reservation);
            }

            return reservationReturnList;
        }

        /**
         * Gets information about a specific reservation
         * Note: this isnt available as a service to the REST service, its for internal use only
         * @param reservation whose information is to be gotten
         * @return Information about the specific reservation
         */
        public List<Models.Reservation> SelectReservationByNumber(string reservationNumber)
        {
            //Checks that the number is a hexadecimal number, since only these area available as reservation numbers
            if (!System.Text.RegularExpressions.Regex.IsMatch(reservationNumber, @"\A\b[0-9a-fA-F]+\b\Z")){
                return null;
            }

            List<string> attributes = new List<string>
            {
                "reservationNumber"
            };
            List<string> keys = new List<string>
            {
                reservationNumber
            };

            List<Models.Reservation> reservationReturnList = new List<Models.Reservation>();

            List<XElement> reservationsElements = GetElement(attributes, keys);

            foreach (XElement reservationElement in reservationsElements)
            {
                Models.Reservation reservation = new Models.Reservation()
                {
                    ReservationNumber = reservationElement.Attribute("reservationNumber").Value,
                    IDFlight = long.Parse(reservationElement.Attribute("flightNumber").Value),
                    SeatNumber = int.Parse(reservationElement.Attribute("seatNumber").Value),
                    Status = reservationElement.Attribute("status").Value,
                    Date = DateTime.Parse(reservationElement.Attribute("date").Value)
                };

                reservationReturnList.Add(reservation);
            }

            return reservationReturnList;
        }


        /**
         * Creates one or more reservations and associates them with a user
         * @param userName who is associated with the account
         * @param lsit of users who are going to flight
         * @param flight number to be associated
         * @param date of the flight
         * @return success of the operation
         */
        public bool ReserveFlight(string userName, string[] userID, int flightNumber, DateTime date)
        {
            List<string> attributes = new List<string>
            {
                "userName",
                "userId",
                "flightNumber",
                "date",
                "reservationNumber",
                "seatNumber",
                "status"
            };

            List<string> values = new List<string>
            {
                userName,
                "",
                flightNumber.ToString(),
                date.ToString(),
                "",
                "-1",
                "reserved"
            };

            foreach (string id in userID)
            {
                reservationNumber++;
                //Adds user ID to the reservation
                values[1] = id;
                //Gets the reservation number
                values[4] = reservationNumber.ToString("X");
                AddElement("reservation", attributes, values);
            }
            return true;
        }

        /**
         * Associates a reservation ot a specific seat in the flight
         * @param reservation te be assocaited to a seat
         * @param seat to be taken
         * @return sucess of the operation
         */
        public bool InsertSeat(string reservationNumber, int seat)
        {
            throw new NotImplementedException();
        }

        /**
         * Updates a specific column in a reservation, used for checkin and boarding of passengers
         * @param reservation to be updated
         * @param newValue to be added
         * @param column to be updated
         */
        public bool Update(string reservationNumber, string newValue, string column)
        {
            //Checks that the reservation number is a hex number
            if (!System.Text.RegularExpressions.Regex.IsMatch(reservationNumber, @"\A\b[0-9a-fA-F]+\b\Z"))
            {
                return false;
            }

            List<string> attributes = new List<string>
            {
                "reservationNumber"
            };
            List<string> keys = new List<string>
            {
                reservationNumber
            };
            List<string> attributesToChange = new List<string>
            {
                column
            };
            List<string> changes = new List<string>
            {
                newValue
            };

            return UpdateElement(attributes, keys, attributesToChange, changes);
        }

        /**
         * Associates a bag to a reservation and marks it weight
         * @param reservation to be associated
         * @param weight of the bag
         * @return success of the operation
         */
        public bool InsertBagInfo(string reservationNumber, float weight)
        {
            throw new NotImplementedException();
        }

        /**
         * Generates a new reservation number to be associated with a reservation
         * @return a new alphanumeric reservation number
         */
        private string GenerateReservationNumber()
        {
            /*
            XDocument document = XDocument.Load(Filename);

            XElement parent = document.Root;

            IEnumerable<XElement> elements = parent.Elements();

            XElement lastElement = elements.Last();

            string number = lastElement.Attribute("reservationNumber").Value;

            */

            return "F3C9AC";
        }


    }
}