using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;


namespace reservationMicroservice
{
    abstract public class DatabaseAccess
    {
        protected XDocument doc;

        public string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public DatabaseAccess(string filename)
        {
            DatabaseAccessBuilder builder = new DatabaseAccessBuilder();

            builder.CreateAndLoadXML(this, filename);
            
        }

        /**
         * This adds a new element to the file with all the corresponding attributes
         * @param elementName Name of the element to be added
         * @param attributeName List of attribute names
         * @param attribute List of attributes that correspond to the attribute names
         * @return a boolean indicating the success of the operation
         */
        protected bool AddElement(string elementName, List<string> attributeName, List<string> attribute)
        {
            if (attributeName.Count == attribute.Count)
            {
                XElement parent = new XElement(elementName);
                for (int i = 0; i < attributeName.Count; i++)
                {
                    parent.Add(new XAttribute(attributeName[i], attribute[i]));
                }
                doc = XDocument.Load(Filename);
                doc.Root.Add(parent);
                doc.Save(Filename);
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * This gets all the elements from the file where the key corresponds
         * @param attributes that are check to see which elements to select, if this is empty it returns all elements
         * @param keys to be checked to agains the attributes from the xml file, this must be the same size as attributes
         * @returm a list of XElements with the corresponding keys
         */
        protected List<XElement> GetElement(List<string> attributes, List<string> keys)
        {
            XDocument document = XDocument.Load(Filename);
            XElement parent = document.Root;

            List<XElement> lista = new List<XElement>();

            //Checks that both lists are of the same size
            if (attributes.Count == keys.Count)
            {
                //Goes through each saved element
                foreach (XElement element in parent.Elements())
                {
                    bool match = true;
                    //Makes a check for all attributes
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        //If the attribute is the same as the key, then add it to the return List
                        if (element.Attribute(attributes[i]).Value != keys[i])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        lista.Add(element);
                    }
                }
            }
            return lista;
        }

        /**
         * This deletes all the elements in the file where the keys correspond
         * @param attributes that are check to see which elements to delete
         * @param keys to be checked to agains the attributes from the xml file
         * @return a boolean indicating if any element was deleted
         */
        protected bool DeleteElement(List<string> attributeName, List<string> attribute)
        {
            bool deleted = false;
            if (attributeName.Count == attribute.Count)
            {
                doc = XDocument.Load(Filename);

                XElement parent = doc.Root;

                //Goes through each saved element
                foreach (XElement element in parent.Elements())
                {
                    //Makes a check for all attributes
                    for (int i = 0; i < attributeName.Count; i++)
                    {
                        //If the attribute is the same as the key, then it removes it
                        if (element.Attribute(attributeName[i]).Value == attribute[i])
                        {
                            element.Remove();
                            deleted = true;
                        }
                    }
                }

                //Saves the results to the xml file
                doc.Save(Filename);
            }

            return deleted;
        }

        /**
         * This updates all the elements in the file where the keys correspond
         * @param attributes that are check to see which elements to change
         * @param keys to be checked to agains the attributes from the xml file
         * @param attributestoChange the attributes that are going to be changed
         * @param changes new values for the attributes
         * @return a boolean indicating if a change is to be made
         */
        protected bool UpdateElement(List<string> attributes, List<string> keys, List<string> attributestoChange, List<string> changes)
        {
            bool updated = false;
            if ((attributes.Count == keys.Count) && (attributestoChange.Count == changes.Count))
            {
                doc = XDocument.Load(Filename);
                XElement parent = doc.Root;


                //This code is used to test that all column requested for the update are valid
                XElement tester = (XElement) parent.FirstNode;
                if (tester != null)
                    foreach(string attribute in attributestoChange)
                        if (tester.Attribute(attribute) == null)
                            return false;

                //Goes through each saved element
                foreach (XElement element in parent.Elements())
                {
                    //Makes a check for all attributes
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        //If the attribute is the same as the key, then it updates it
                        if (element.Attribute(attributes[i]).Value == keys[i])
                        {
                            //Changes all necessary attributes
                            for (int j = 0; j < attributestoChange.Count; j++)
                            {
                                element.Attribute(attributestoChange[j]).Value = changes[j];
                            }
                            updated = true;
                        }
                    }
                }

                //Saves the results to the xml file
                doc.Save(Filename);
            }

            return updated;
        }
    }
}