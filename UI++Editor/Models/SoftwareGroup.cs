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
    public class SoftwareGroup : IElement, ISoftwareRef
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Software Group"; } }
        public bool? Default { get; set; } // default is false
        public string Id { get; set; } // required
        public string Label { get; set; } // required
        public bool? Required { get; set; } // default is false
        public ObservableCollection<ISoftwareRef> SoftwareRefs { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareGroup", null);
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute label = d.CreateAttribute("Label");
            XmlAttribute required = d.CreateAttribute("Required");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign Attribute Values
            _default.Value = Default.ToString();
            id.Value = Id;
            label.Value = Label;
            required.Value = Required.ToString();
            condition.Value = Condition;

            // Append Attributes
            if (null != Default)
            {
                output.Attributes.Append(_default);
            }
            output.Attributes.Append(id);
            output.Attributes.Append(label);
            if(null != Required)
            {
                output.Attributes.Append(required);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (ISoftwareRef softwareRef in SoftwareRefs)
            {
                XmlNode importNode = d.ImportNode(softwareRef.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
