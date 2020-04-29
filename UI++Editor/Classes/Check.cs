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
    public class Check : IElement
    {
        public string CheckCondition { get; set; } // required
        public string Description { get; set; }
        public string ErrorDescription { get; set; }
        public string Text { get; set; } // required
        public string WarnCondition { get; set; }
        public string WarnDescription { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Check", null);
            XmlAttribute checkCondition = d.CreateAttribute("CheckCondition");
            XmlAttribute description = d.CreateAttribute("Description");
            XmlAttribute errorDescription = d.CreateAttribute("ErrorDescription");
            XmlAttribute text = d.CreateAttribute("Text");
            XmlAttribute warnCondition = d.CreateAttribute("WarnCondition");
            XmlAttribute warnDescription = d.CreateAttribute("WarnDescription");

            // Set Attribute Values
            checkCondition.Value = CheckCondition;
            description.Value = Description;
            errorDescription.Value = ErrorDescription;
            text.Value = Text;
            warnCondition.Value = WarnCondition;
            warnDescription.Value = WarnDescription;

            // Append Attributes
            output.Attributes.Append(checkCondition);
            if(!string.IsNullOrEmpty(Description))
            {
                output.Attributes.Append(description);
            }
            if (!string.IsNullOrEmpty(ErrorDescription))
            {
                output.Attributes.Append(errorDescription);
            }
            output.Attributes.Append(text);
            if (!string.IsNullOrEmpty(WarnCondition))
            {
                output.Attributes.Append(warnCondition);
            }
            if (!string.IsNullOrEmpty(WarnDescription))
            {
                output.Attributes.Append(warnDescription);
            }

            return output;
        }
    }
}
