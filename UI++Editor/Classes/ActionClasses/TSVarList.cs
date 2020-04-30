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
    public class TSVarList : IElement, IAction
    {
        public string Type { get; } = "TSVarList";
        public string ApplicationVariableBase { get; set; }
        public string PackageVariableBase { get; set; }
        public ObservableCollection<ISoftwareRef> SoftwareRefs { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute applicationVariableBase = d.CreateAttribute("ApplicationVariableBase");
            XmlAttribute packageVariableBase = d.CreateAttribute("PackageVariableBase");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = Type;
            applicationVariableBase.Value = ApplicationVariableBase;
            packageVariableBase.Value = PackageVariableBase;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if(!string.IsNullOrEmpty(ApplicationVariableBase))
            {
                output.Attributes.Append(applicationVariableBase);
            }
            if(!string.IsNullOrEmpty(PackageVariableBase))
            {
                output.Attributes.Append(packageVariableBase);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (ISoftwareRef softwareRef in SoftwareRefs)
            {
                output.AppendChild(softwareRef.GenerateXML());
            }

            return output;
        }
    }
}
