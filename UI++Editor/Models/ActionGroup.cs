using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class ActionGroup : IElement
    {
        public ObservableCollection<IElement> Actions;
        public string Name { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "ActionGroup", null);
            XmlAttribute name = d.CreateAttribute("Name");

            // Add Attributes to Node
            output.Attributes.Append(name);

            // Add all child nodes to the group
            foreach(IElement action in Actions)
            {
                XmlNode importNode = d.ImportNode(action.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
