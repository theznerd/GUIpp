using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes
{
    public class Application : IElement, ISoftware
    {
        public string ID { get; set; } // required
        public string IncludeID { get; set; }
        public string Label { get; set; } // required
        public string Name { get; set; } // required

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Application", null);
            XmlAttribute id = d.CreateAttribute("ID");
            XmlAttribute includeID = d.CreateAttribute("IncludeID");
            XmlAttribute label = d.CreateAttribute("Label");
            XmlAttribute name = d.CreateAttribute("Name");

            // Set Attribute Values
            id.Value = ID;
            includeID.Value = IncludeID;
            label.Value = Label;
            name.Value = Name;

            // Append Attributes to Node
            output.Attributes.Append(id);
            output.Attributes.Append(label);
            output.Attributes.Append(name);
            if(!string.IsNullOrEmpty(IncludeID))
            {
                output.Attributes.Append(includeID);
            }

            return output;
        }
    }
}
