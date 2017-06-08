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
    public class AutomaticFlightController 
    {
        //Attribute to control the loop
        private bool keepRunning;
        //Seconds that the loop sleeps beetween every verification
        private int seconds;

        /// <summary>
        /// This the constructor of the class which is the responsible
        /// to strat the thread.
        /// </summary>
        public AutomaticFlightController()
        {
            seconds = 10;
            keepRunning = true;
            ThreadStart obj = new ThreadStart(loop);
            //Creamos la instancia del hilo 
            Thread thread = new Thread(obj);
            //The thread starts running
            thread.Start();
        }//End of the constructor

        /// <summary>
        /// This is the method that is always running to check the flight dates and set their status
        /// </summary>
        public void loop()
        {
            int n = 0; //quitar
            LinkedList<Flight> flights = null;
            while (keepRunning)
            {
                DateTime now;
                //The service is used to access to the flights 
                FlightDataBaseAccessService flightAccess = new FlightDataBaseAccessService();
                flights = flightAccess.selectFlights();
                //It iterates the list of flights
                foreach (Flight flight in flights)
                {
                    //It is gotten the current date and hour
                    now = DateTime.Now;
                    //They are determined the dates in which the system must open and close the flight
                    DateTime closeDate = flight.DepartureDate.AddHours(-1);
                    DateTime openDate = flight.DepartureDate.AddHours(-24);
                    //If it is already the time to open the flight
                    if (now.CompareTo(closeDate) < 0 && now.CompareTo(openDate) >= 0)
                    {
                        //The flight is opened
                        flight.Status = 1;
                    } //If it is the time to close the flight
                    else if (now.CompareTo(closeDate) >= 0 && now.CompareTo(flight.DepartureDate) < 0)
                    {
                        //The flight is closed
                        flight.Status = 2;
                    }
                    //The XML file are modified
                    flightAccess.modifyStatusXML(flight.ID, flight.Status);

                    //keepRunning = false;
                    System.Diagnostics.Debug.WriteLine("N= " + n);
                    n++;
                    //The microservice sleeps N seconds
                    Thread.Sleep(seconds*1000);
                }//End of the foreach
            }//End of the while
            this.printList(flights);

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