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
            FlightDataBaseAccessService fs = new FlightDataBaseAccessService();
        }
    }
}
