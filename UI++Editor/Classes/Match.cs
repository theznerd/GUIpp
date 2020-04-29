using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes
{
    public class Match : IElement
    {
        public string DisplayName { get; set; } // required
        public string Variable { get; set; } // required
        public string Version { get; set; }
        public string VersionOperator { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Match", null);
            XmlAttribute displayName = d.CreateAttribute("DisplayName");
            XmlAttribute variable = d.CreateAttribute("Variable");
            XmlAttribute version = d.CreateAttribute("Version");
            XmlAttribute versionOperator = d.CreateAttribute("VersionOperator");

            // Set Attribute Values
            displayName.Value = DisplayName;
            variable.Value = Variable;
            version.Value = Version;
            versionOperator.Value = VersionOperator;

            // Append Attributes
            output.Attributes.Append(displayName);
            output.Attributes.Append(variable);
            if (!string.IsNullOrEmpty(Version))
            {
                output.Attributes.Append(version);
            }
            if (!string.IsNullOrEmpty(VersionOperator))
            {
                output.Attributes.Append(versionOperator);
            }

            return output;
        }
    }
}
