using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class DefaultValues : IElement, IAction
    {
        public string Type { get; } = "DefaultValues";
        public bool? ShowProgress { get; set; }
        public string ValueTypes { get; set; } // required, default is All

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showProgress = d.CreateAttribute("ShowProgress");
            XmlAttribute valueTypes = d.CreateAttribute("ValueTypes");
            
            // Assign attribute values
            type.Value = Type;
            showProgress.Value = ShowProgress.ToString();
            valueTypes.Value = ValueTypes;

            // Append Attributes
            output.Attributes.Append(type);
            if(null != ShowProgress)
            {
                output.Attributes.Append(showProgress);
            }
            output.Attributes.Append(valueTypes);

            return output;
        }
    }
}
