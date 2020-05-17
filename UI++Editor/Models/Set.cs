using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class Set : IElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Set"; } }
        public string Name { get; set; } // required
        public ObservableCollection<ISoftwareRef> SoftwareRefs { get; set; } // SoftwareGroup, SoftwareRef
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Set", null);
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            name.Value = Name;
            condition.Value = Condition;

            // Append Children
            foreach (ISoftwareRef softwareRef in SoftwareRefs)
            {
                XmlNode importNode = d.ImportNode(softwareRef.GenerateXML(), true);
                output.AppendChild(importNode);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
