using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class Vars : IElement, IAction
    {
        public string Type { get; } = "Vars";
        public string Direction { get; set; } // default is Save | Save/Load
        public string Filename { get; set; } // defaults to %temp%\ui++vars.dat
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute direction = d.CreateAttribute("Direction");
            XmlAttribute filename = d.CreateAttribute("Filename");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = Type;
            direction.Value = Direction;
            filename.Value = Filename;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(Direction))
            {
                output.Attributes.Append(direction);
            }
            if (!string.IsNullOrEmpty(Filename))
            {
                output.Attributes.Append(filename);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
