using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebService
{
    /// <summary>
    /// Class to represent Airlines 
    /// </summary>
    public class Airline
    {
        //Attributes of a Airline
        private long id_Airline;
        private string name;

        /// <summary>
        /// Property to modify and get the id of the airline
        /// </summary>
        public long ID_Airline
        {
            get { return id_Airline; }
            set { id_Airline = value; }
        }

        /// <summary>
        /// Property to modify and get the name of the airline
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }//End of the Airline class
}
