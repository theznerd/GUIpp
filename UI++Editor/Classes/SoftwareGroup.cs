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
    public class SoftwareGroup : IElement, ISoftwareRef
    {
        public bool? Default { get; set; } // default is false
        public string Id { get; set; } // required
        public string Label { get; set; } // required
        public bool? Required { get; set; } // default is false
        public ObservableCollection<ISoftwareRef> SoftwareRefs { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareGroup", null);
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute label = d.CreateAttribute("Label");
            XmlAttribute required = d.CreateAttribute("Required");

            // Assign Attribute Values
            _default.Value = Default.ToString();
            id.Value = Id;
            label.Value = Label;
            required.Value = Required.ToString();

            // Append Attributes
            if(null != Default)
            {
                output.Attributes.Append(_default);
            }
            output.Attributes.Append(id);
            output.Attributes.Append(label);
            if(null != Required)
            {
                output.Attributes.Append(required);
            }

            // Append Children
            foreach(ISoftwareRef softwareRef in SoftwareRefs)
            {
                output.AppendChild(softwareRef.GenerateXML());
            }

            return output;
        }
    }
}
