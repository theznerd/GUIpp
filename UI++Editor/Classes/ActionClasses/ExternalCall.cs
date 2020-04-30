using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class ExternalCall : IElement, IAction
    {
        public string Type { get; } = "ExternalCall";
        public int? MaxRunTime { get; set; }
        public string Title { get; set; } // Shown in log and progress bar
        public string Content { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute maxRunTime = d.CreateAttribute("MaxRunTime");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = Type;
            maxRunTime.Value = MaxRunTime.ToString();
            title.Value = Title;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if(null != MaxRunTime)
            {
                output.Attributes.Append(maxRunTime);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Content
            output.InnerText = Content;

            return output;
        }
    }
}
