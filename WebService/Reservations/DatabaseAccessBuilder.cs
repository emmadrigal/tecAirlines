using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace reservationMicroservice
{
    public class DatabaseAccessBuilder
    {
        public void CreateAndLoadXML(DatabaseAccess db, string filename)
        {
            //Gets the temp file from the system
            string tempPath = Path.GetTempPath();
            string configPath = tempPath + filename + ".xml";

            //Set the database access to point to the same file
            db.Filename = configPath;

            //Creates file in case that this is the first run
            if (!File.Exists(configPath))
            {
                // Create XDocument
                XDocument document = new XDocument(
                    new XDeclaration("1.0", "utf8", "yes"),
                        new XElement(filename, "")
                    );
                document.Save(configPath);
                document = XDocument.Load(configPath);
            }
        }
    }

}