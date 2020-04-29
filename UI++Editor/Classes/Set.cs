using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes
{
    public class Set : IElement
    {
        public string Name { get; set; } // required
        public ObservableCollection<ISoftwareRef> SoftwareRefs { get; set; } // SoftwareGroup, SoftwareRef

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Set", null);
            XmlAttribute name = d.CreateAttribute("Name");

            // Set Attribute Values
            name.Value = Name;

            // Append Children
            foreach(ISoftwareRef softwareRef in SoftwareRefs)
            {
                output.AppendChild(softwareRef.GenerateXML());
            }

            return output;
        }
    }
}
