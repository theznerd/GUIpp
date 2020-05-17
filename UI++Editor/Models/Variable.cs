using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class Variable : IElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Variable"; } }
        public string Name { get; set; } // required
        public bool DontEval { get; set; } // default is false

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Variable", null);
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute dontEval = d.CreateAttribute("DontEval");

            // Set Attribute Value
            name.Value = Name;
            dontEval.Value = DontEval.ToString();

            // Append Attributes
            output.Attributes.Append(name);
            output.Attributes.Append(dontEval);

            return output;
        }
    }
}
