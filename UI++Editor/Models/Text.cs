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
    public class Text : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Text"; } }
        public string[] ValidChildren { get; set; } = { "" };
        public string[] ValidParents { get; set; } = { "DefaultValues" };
        public string Type { get; set; }
        public string Value { get; set; }
        public string Condition { get; set; }

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

        private bool _TVIsExpanded = true;
        public bool TVIsExpanded
        {
            get { return _TVIsExpanded; }
            set
            {
                _TVIsExpanded = value;
                NotifyOfPropertyChange(() => TVIsExpanded);
            }
        }

        public Text(IParentElement p)
        {
            Parent = p;
            ViewModel = new ViewModels.Actions.Children.TextViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Text", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute value = d.CreateAttribute("Value");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign Attribute Values
            type.Value = Type;
            value.Value = Value;
            condition.Value = Condition;

            // Append Attributes
            if (!string.IsNullOrEmpty(Type))
            {
                output.Attributes.Append(type);
            }
            if (!string.IsNullOrEmpty(Value))
            {
                output.Attributes.Append(value);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
