using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes
{
    public class Choice : IElement, IChoice
    {
        public string Option { get; set; } // required
        public string Value { get; set; }
        public string AlternateValue { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Choice", null);
            XmlAttribute option = d.CreateAttribute("Option");
            XmlAttribute value = d.CreateAttribute("Value");
            XmlAttribute alternateValue = d.CreateAttribute("AlternateValue");

            // Set Attribute Values
            option.Value = Option;
            value.Value = Value;
            alternateValue.Value = AlternateValue;

            // Append Attribute
            output.Attributes.Append(option);
            if (!string.IsNullOrEmpty(Value))
            {
                output.Attributes.Append(value);
            }
            if(!string.IsNullOrEmpty(AlternateValue))
            {
                output.Attributes.Append(alternateValue);
            }

            return output;
        }
    }
}
