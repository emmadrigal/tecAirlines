using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlightWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            AutomaticFlightController afc = new AutomaticFlightController();
            //afc.createXML();
            FlightDataBaseAccessService fs = new FlightDataBaseAccessService();
            //fs.selectFlights();
            //while (true);
            //DateTime today = DateTime.Now;
            //DateTime date = DateTime.Now;
            //date = date.AddHours(-15);

            //System.Diagnostics.Debug.WriteLine("Now: " + today.Date + " " + today.Hour + ":" + today.Minute );
            //System.Diagnostics.Debug.WriteLine("Date: " + date.Date + " " + date.Hour + ":" + date.Minute);
            //System.Diagnostics.Debug.WriteLine("cmp : " + today.CompareTo(date));

        }
    }
}
