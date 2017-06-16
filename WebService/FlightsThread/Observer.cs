using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebService
{
    
    public abstract class Observer
    {
        protected Subject subject;

        /// <summary>
        /// Method to be implemented in the concrete observer
        /// </summary>
        public abstract void update();
        public void setSubject(Subject sub)
        {
            this.subject = sub;
        }//End of the mthod
    }//End of Observer
}
