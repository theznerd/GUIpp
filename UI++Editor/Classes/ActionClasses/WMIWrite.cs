using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class WMIWrite : IElement, IAction
    {
        public string Type { get; } = "WMIWrite";
        public string Class { get; set; } // required
        public string Namespace { get; set; } // default is root\cimv2
        public ObservableCollection<Property> Properties { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute _class = d.CreateAttribute("Class");
            XmlAttribute _namespace = d.CreateAttribute("Namespace");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = Type;
            _class.Value = Class;
            _namespace.Value = Namespace;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            output.Attributes.Append(_class);
            if (!string.IsNullOrEmpty(Namespace))
            {
                output.Attributes.Append(_namespace);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach(Property property in Properties)
            {
                output.AppendChild(property.GenerateXML());
            }

            return output;
        }
    }
}
