using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes
{
    public class Package : IElement, ISoftware
    {
        public string Id { get; set; } // required
        public string IncludeID { get; set; }
        public string Label { get; set; } // required
        public string Name { get; set; } // required
        public string PkgId { get; set; } // required
        public string ProgramName { get; set; } // required

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Package", null);
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute includeID = d.CreateAttribute("IncludeID");
            XmlAttribute label = d.CreateAttribute("Label");
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute pkgId = d.CreateAttribute("PkgId");
            XmlAttribute programName = d.CreateAttribute("Programname");

            // Set Attribute Values
            id.Value = Id;
            includeID.Value = IncludeID;
            label.Value = Label;
            name.Value = Name;
            pkgId.Value = PkgId;
            programName.Value = ProgramName;

            // Append Attributes to Node
            output.Attributes.Append(id);
            output.Attributes.Append(label);
            output.Attributes.Append(name);
            output.Attributes.Append(pkgId);
            output.Attributes.Append(programName);
            if(!string.IsNullOrEmpty(IncludeID))
            {
                output.Attributes.Append(includeID);
            }

            return output;
        }
    }
}
