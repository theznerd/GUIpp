using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class _Template : IElement, IAction
    {
        public string Type { get; } = "";
        public bool? ShowBack { get; set; }
        public bool? ShowCancel { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute showCancel = d.CreateAttribute("ShowCancel");

            // Assign attribute values
            type.Value = Type;
            showBack.Value = ShowBack.ToString();
            showCancel.Value = ShowCancel.ToString();

            // Append Attributes
            output.Attributes.Append(type);
            if(null != ShowBack)
            {
                output.Attributes.Append(showBack);
            }
            if(null != ShowCancel)
            {
                output.Attributes.Append(showCancel);
            }

            return output;
        }
    }
}
