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
    public class SoftwareDiscovery : IElement, IAction
    {
        public string Type { get; } = "SoftwareDiscovery";
        public ObservableCollection<Match> Matches { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = Type;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (Match match in Matches)
            {
                output.AppendChild(match.GenerateXML());
            }

            return output;
        }
    }
}
