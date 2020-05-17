using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;

namespace UI__Editor.Models.ActionClasses
{
    public class SaveItems : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "SaveItems";
        public string Items { get; set; } // required
        public string Path { get; set; } // required
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute items = d.CreateAttribute("Items");
            XmlAttribute path = d.CreateAttribute("Path");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            items.Value = Items;
            path.Value = Path;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            output.Attributes.Append(items);
            output.Attributes.Append(path);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
