using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TecAirlines.Tests
{
    /// <summary>
    /// Class responsible to do the unitary tests to the methods
    /// used in the flight microservice
    /// </summary>
    [TestClass()]
    public class FlightDataBaseAccessServiceTests
    {
        /// <summary>
        /// Test if the list returned is the correct 
        /// </summary>
        [TestMethod()]
        public void getAllFlightsTest()
        {
            List<Models.Flight> list = getFlightsList();
            Assert.AreEqual(list.Count, 2);
        }//End of the method

        /// <summary>
        /// Compare the departure date obtained of the first flight
        /// in the xml flight vs the correct one
        /// </summary>
        [TestMethod()]
        public void addDepartureDateTest()
        {
            //The list of flights is recovered
            List<Models.Flight> list = getFlightsList();
            DateTime dateTest = list[0].DepartureDate;
            //The correct date object is created
            DateTime dateCorrect = new DateTime();
            dateCorrect = dateCorrect.AddMinutes(33);
            dateCorrect = dateCorrect.AddHours(19);
            dateCorrect = dateCorrect.AddDays(7);
            dateCorrect = dateCorrect.AddMonths(5);
            dateCorrect = dateCorrect.AddYears(2016);
            Assert.AreEqual(dateTest, dateCorrect);
        }//End of the method

        /// <summary>
        /// Compare the arrival date obtained of the first flight
        /// in the xml flight vs the correct one
        /// </summary>
        [TestMethod()]
        public void addArrivaleDateTest()
        {
            //The list of flights is recovered
            List<Models.Flight> list = getFlightsList();
            DateTime dateTest = list[0].ArrivalDate;
            //The correct date object is created
            DateTime dateCorrect = new DateTime();
            dateCorrect = dateCorrect.AddMinutes(30);
            dateCorrect = dateCorrect.AddHours(3);
            dateCorrect = dateCorrect.AddDays(6);
            dateCorrect = dateCorrect.AddMonths(5);
            dateCorrect = dateCorrect.AddYears(2016);
            Assert.AreEqual(dateTest, dateCorrect);
        }//End of the method

        /// <summary>
        /// Compare the arrival and departure ATOs obtained of the first flight
        /// in the xml flight vs the correct one
        /// </summary>
        [TestMethod()]
        public void addATOsTest()
        {
            //The list of flights is recovered
            List<Models.Flight> list = getFlightsList();
            //Airport object used to store the correct data
            Models.Airport atoCorrect = new Models.Airport();
            // Test 1: departure ATO
            //The departure ATO of the flight is gotten
            Models.Airport depATO = list[0].DepartureATO;
            //The correct departure ATO info are stored 
            atoCorrect.ID_Airport = 1;
            atoCorrect.Name = "JuanSantamaria";
            atoCorrect.Location = "Alajuela";
            atoCorrect.Code = "ABcd1";
            //The departure ATOs are compared
            Assert.AreEqual(depATO.Name, atoCorrect.Name);
            Assert.AreEqual(depATO.ID_Airport, atoCorrect.ID_Airport);
            Assert.AreEqual(depATO.Location, atoCorrect.Location);
            Assert.AreEqual(depATO.Code, atoCorrect.Code);
            // Test 2: arrival ATO
            //The arrival ATO of the flight is gotten
            Models.Airport arrivalATO = list[0].ArrivalATO;
            //The correct arrival ATO info are stored 
            atoCorrect.ID_Airport = 3;
            atoCorrect.Name = "LondresAir";
            atoCorrect.Location = "England";
            atoCorrect.Code = "AABB";
            //The arrival ATOs are compared
            Assert.AreEqual(arrivalATO.Name, atoCorrect.Name);
            Assert.AreEqual(arrivalATO.ID_Airport, atoCorrect.ID_Airport);
            Assert.AreEqual(arrivalATO.Location, atoCorrect.Location);
            Assert.AreEqual(arrivalATO.Code, atoCorrect.Code);
        }//End of the method

        /// <summary>
        /// Compare the airline obtained of the first flight
        /// in the xml flight vs the correct one
        /// </summary>
        [TestMethod()]
        public void addAirlineTest()
        {
            //The list of flights is recovered
            List<Models.Flight> list = getFlightsList();
            Models.Airline airline = list[0].Airline;
            //The correct date object is created
            Models.Airline correct = new Models.Airline();
            correct.ID_Airline = 4;
            correct.Name = "TEC_Airlines";
            //The airlines are compared
            Assert.AreEqual(airline.ID_Airline, correct.ID_Airline);
            Assert.AreEqual(airline.Name, correct.Name);
        }//End of the method

        /// <summary>
        /// Compare the stops obtained of the first flight
        /// in the xml flight vs the correct ones
        /// </summary>
        [TestMethod()]
        public void addStopsTest()
        {
            //The list of flights is recovered
            List<Models.Flight> list = getFlightsList();
            List<long> stops = list[0].Stops;
            //The correct date object is created
            List<long> correct = new List<long>();
            correct.Add(8);
            correct.Add(9);
            correct.Add(10);
            //Every stop is compared
            for (int i = 0; i < correct.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine("Stop position " + i + ": " + getPosition(stops, i));
                Assert.AreEqual(correct[i], stops[i]);
            }//End of foreach
        }//End of the method

        /// <summary>
        /// Compare the reservations obtained of the first flight
        /// in the xml flight vs the correct ones
        /// </summary>
        [TestMethod()]
        public void addReservationsTest()
        {
            //The list of flights is recovered
            List<Models.Flight> list = getFlightsList();
            List<long> reservations = list[0].Reservations;
            //The correct reservations list object is created
            List<long> correct = new List<long>();
            correct.Add(100);
            correct.Add(200);
            correct.Add(300);
            //Every stop is compared
            for (int i = 0; i < correct.Count; i++)
            {
                Assert.AreEqual(correct[i], reservations[i]);
            }//End of for
        }//End of the method

        /// <summary>
        /// This method returns the flights read by the FlightDataBaseAccessService
        /// of the xml file. It is used by the others test methods.  
        /// </summary>
        /// <returns>list of flights</returns>
        private List<Models.Flight> getFlightsList()
        {
            FlightDataBaseAccessService obj = new FlightDataBaseAccessService("flightsReadTest");
            return obj.getAllFlights();
        }//End of the method

    }//End of the class
}