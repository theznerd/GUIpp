using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class SoftwareRef : IElement, ISoftwareRef
    {
        public string Id { get; set; } // required
        public bool? Hidden { get; set; } // default is false
        public bool? Default { get; set; } // default is false
        public bool? Required { get; set; } // default is false
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareRef", null);
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute hidden = d.CreateAttribute("Hidden");
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute required = d.CreateAttribute("Required");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            id.Value = Id;
            hidden.Value = Hidden.ToString();
            _default.Value = Default.ToString();
            required.Value = Required.ToString();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(id);
            if (null != Hidden)
            {
                output.Attributes.Append(hidden);
            }
            if (null != Default)
            {
                output.Attributes.Append(_default);
            }
            if (null != Required)
            {
                output.Attributes.Append(required);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
