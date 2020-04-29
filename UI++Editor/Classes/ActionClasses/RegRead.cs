using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class RegRead : IElement, IAction
    {
        public string Type { get; } = "RegRead";
        public string Default { get; set; }
        public string Hive { get; set; } // Default is HKLM
        public string Key { get; set; } // Required
        public bool? Reg64 { get; set; } // Default is true
        public string Variable { get; set; } // required
        public string Value { get; set; } // required

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute hive = d.CreateAttribute("Hive");
            XmlAttribute key = d.CreateAttribute("Key");
            XmlAttribute reg64 = d.CreateAttribute("Reg64");
            XmlAttribute variable = d.CreateAttribute("Variable");
            XmlAttribute value = d.CreateAttribute("Value");

            // Assign attribute values
            type.Value = Type;
            _default.Value = Default;
            hive.Value = Hive;
            key.Value = Key;
            reg64.Value = Reg64.ToString();
            variable.Value = Variable;
            value.Value = Value;

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(Default))
            {
                output.Attributes.Append(_default);
            }
            if (!string.IsNullOrEmpty(Hive))
            {
                output.Attributes.Append(hive);
            }
            output.Attributes.Append(key);
            if(null != Reg64)
            {
                output.Attributes.Append(reg64);
            }
            output.Attributes.Append(variable);
            output.Attributes.Append(value);

            return output;
        }
    }
}
