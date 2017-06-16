using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Threading;

namespace FlightWebService
{
    /// <summary>
    /// Controller responsible to change the status of the flights, it means
    /// he should open and close the flight according to their departure date.
    /// </summary>
    public class AutomaticFlightController: Subject 
    {
        //Attribute to control the loop
        private bool keepRunning;
        //Seconds that the loop sleeps beetween every verification
        private int seconds;

        /// <summary>
        /// This is the constructor of the class which is the responsible
        /// to strat the thread.
        /// </summary>
        public AutomaticFlightController()
        {
            //The flights are added as observers
            addObservers();
            //Seconds to wait
            seconds = 10;
            keepRunning = true;
            ThreadStart obj = new ThreadStart(loop);
            //The instance thread is created 
            Thread thread = new Thread(obj);
            //The thread starts running
            thread.Start();
        }//End of the constructor

        /// <summary>
        /// Adds the flights as observer of the subject
        /// </summary>
        private void addObservers()
        {
            //The service is used to access to the current flights 
            FlightDataBaseAccessService flightAccess = new FlightDataBaseAccessService();
            LinkedList<Flight> flights = flightAccess.selectFlights();
            foreach (Flight flight in flights)
            {
                flight.setSubject(this);
                this.attach(flight);
            }//End of foreach
        }//End of the method

        /// <summary>
        /// This is the method that is always running to check the flight dates and set their status
        /// </summary>
        public void loop()
        {
            while (keepRunning)
            {
                //The current hour and date are gotten
                DateTime now = DateTime.Now;
                //The state of the subject is changed
                this.setState(now.ToString());
                //The microservice sleeps N seconds
                Thread.Sleep(seconds*1000);
            }//End of the while
        }//End of loop method


        /// <summary>
        /// Print the flights in the list parameter
        /// </summary>
        /// <param name="list">container of the flights</param>
        private void printList(LinkedList<Flight> list)
        {
            System.Diagnostics.Debug.WriteLine("PRINT LIST");
            foreach (Flight f in list)
            {
                f.print();
            }//
        }

        
    }//End of AutomaticFlightController 
}