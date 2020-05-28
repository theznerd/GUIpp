using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;

namespace UI__Editor.Models.ActionClasses
{
    public class WMIWrite : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return true; } }
        public string ActionType { get; } = "WMI Write";
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
            type.Value = ActionType;
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
                XmlNode importNode = d.ImportNode(property.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
