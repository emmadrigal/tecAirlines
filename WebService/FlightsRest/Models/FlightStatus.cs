using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TecAirlines.Models
{
    /// <summary>
    /// This class stores the status of a flight which can be 
    /// created, opened or closed.
    /// </summary>
    public class FlightStatus
    {
        //Attributes of a Flight status
        private long id_Status;
        private string name;
        
        /// <summary>
        /// Property to get and set the name of the status
        /// </summary>
        public string Name {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Property to get and set the id_Status of the status
        /// </summary>
        public long ID_Status
        {
            get { return id_Status; }
            set { id_Status = value; }
        }

    }//End of the FlightStatus class
}