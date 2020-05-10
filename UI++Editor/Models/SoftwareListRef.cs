using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class SoftwareListRef : IElement
    {
        public string Id { get; set; } // required

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareListRef", null);
            XmlAttribute id = d.CreateAttribute("Id");

            // Set Attribute Values
            id.Value = Id;

            // Append Attributes
            output.Attributes.Append(id);

            return output;
        }
    }
}
