using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes
{
    public class Variable : IElement
    {
        public string Name { get; set; } // required
        public bool DontEval { get; set; } = false;

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Variable", null);
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute dontEval = d.CreateAttribute("DontEval");

            // Set Attribute Value
            name.Value = Name;
            dontEval.Value = DontEval.ToString();

            // Append Attributes
            output.Attributes.Append(name);
            output.Attributes.Append(dontEval);

            return output;
        }
    }
}
