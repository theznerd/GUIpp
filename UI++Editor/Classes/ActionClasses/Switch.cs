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
    public class Switch : IElement, IAction
    {
        public string Type { get; } = "Switch";
        public bool? DontEval { get; set; } // default is false
        public string OnValue { get; set; } // required
        public ObservableCollection<Case> Cases { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute onValue = d.CreateAttribute("OnValue");
            XmlAttribute dontEval = d.CreateAttribute("DontEval");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = Type;
            onValue.Value = OnValue;
            dontEval.Value = DontEval.ToString();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            output.Attributes.Append(onValue);
            if(null != DontEval)
            {
                output.Attributes.Append(dontEval);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (Case c in Cases)
            {
                output.AppendChild(c.GenerateXML());
            }

            return output;
        }
    }
}
