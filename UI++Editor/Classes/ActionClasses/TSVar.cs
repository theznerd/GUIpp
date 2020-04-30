using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class TSVar : IElement, IAction
    {
        public string Type { get; } = "TSVar";
        public string Variable { get; set; } // required - could be named either Name or Variable, rename if Name
        public bool? DontEval { get; set; } // default is false
        public string Content { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute variable = d.CreateAttribute("Variable");
            XmlAttribute dontEval = d.CreateAttribute("DontEval");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = Type;
            variable.Value = Variable;
            dontEval.Value = DontEval.ToString();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            output.Attributes.Append(variable);
            if(null != DontEval)
            {
                output.Attributes.Append(dontEval);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            output.InnerText = Content;

            return output;
        }
    }
}
