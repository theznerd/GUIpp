using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class Text : IElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Text"; } }
        public string Type { get; set; }
        public string Value { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Text", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute value = d.CreateAttribute("Value");

            // Assign Attribute Values
            type.Value = Type;
            value.Value = Value;

            // Append Attributes
            if (!string.IsNullOrEmpty(Type))
            {
                output.Attributes.Append(type);
            }
            if (!string.IsNullOrEmpty(Value))
            {
                output.Attributes.Append(value);
            }

            return output;
        }
    }
}
