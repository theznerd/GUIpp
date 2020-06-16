using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class Property : PropertyChangedBase, IElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Property"; } }
        public bool Key { get; set; } // required
        public string Name { get; set; } // required
        public string Type { get; set; } // CIM_STRING is default
        public string Value { get; set; } // required

        // Code to handle TreeView Selection
        private bool _TVSelected = false;
        public bool TVSelected
        {
            get { return _TVSelected; }
            set
            {
                _TVSelected = value;
                NotifyOfPropertyChange(() => TVSelected);
            }
        }
        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Property", null);
            XmlAttribute key = d.CreateAttribute("Key");
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute value = d.CreateAttribute("Value");

            // Set Attribute values
            key.Value = Key.ToString();
            name.Value = Name;
            type.Value = Type;
            value.Value = Value;

            // Append Attributes
            output.Attributes.Append(key);
            output.Attributes.Append(type);
            if(!string.IsNullOrEmpty(Type))
            {
                output.Attributes.Append(type);
            }
            output.Attributes.Append(value);

            return output;
        }
    }
}
