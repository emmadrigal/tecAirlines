using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebService
{
    /// <summary>
    /// This class stores instances of airport objects
    /// </summary>
    public class Airport
    {
        private long id_Airport { get; set; }
        private string name { get; set; }
        private string location { get; set; }
        private string code { get; set; }

        /// <summary>
        /// Property to access and modify the id of the airport
        /// </summary>
        public long ID_Airport
        {
            set {id_Airport = value;}
            get { return id_Airport;}
        }

        /// <summary>
        /// Property to access and modify the name of the airport
        /// </summary>
        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        /// <summary>
        /// Property to access and modify the location of the airport
        /// </summary>
        public string Location
        {
            set { location = value; }
            get { return location; }
        }

        /// <summary>
        /// Property to access and modify the code of the airport
        /// </summary>
        public string Code
        {
            set { code = value; }
            get { return code; }
        }
    }//End of the Airport Class
}
