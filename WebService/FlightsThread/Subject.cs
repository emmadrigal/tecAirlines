using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebService
{
    /// <summary>
    /// Subject to be observed by their observers
    /// </summary>
    public class Subject
    {
        private List<Observer> observers;
        private string state;

        public Subject()
        {
            state = "";
            observers = new List<Observer>();
        }//End of the constructor

        /// <summary>
        /// This method notifies to the observers the change in the
        /// state.
        /// </summary>
        public void notifyAllObservers()
        {
            foreach(Observer ob in observers)
            {
                ob.update();
            }//End of foreach
        }//End of the method

        /// <summary>
        /// Adds an observer to the subject
        /// </summary>
        /// <param name="observer">observer that will be added</param>
        public void attach(Observer observer)
        {
            if(observer != null)
                observers.Add(observer);
        }//End of attach

        /// <summary>
        /// Remove the observer of the subject
        /// </summary>
        public void detach(Observer observer)
        {
            if (observer != null)
                observers.Remove(observer);
        }//End of dettach

        /// <summary>
        /// Returns the state of the subject
        /// </summary>
        /// <returns>string</returns>
        public string getState()
        {
            return state;
        }//End of the method

        public void setState(string newState)
        {
            this.state = newState;
            //Notify the observers
            this.notifyAllObservers();
        }//End of the setState

    }//End of class
}
