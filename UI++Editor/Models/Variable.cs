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
    public class Variable : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public string[] ValidChildren { get; set; }
        public string[] ValidParents { get; set; } = { "Case" };
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Variable"; } }
        public string Name { get; set; } // required
        public string Condition { get; set; }
        public string Content { get; set; }
        public bool DontEval { get; set; } // default is false

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

        public Variable()
        {
            ViewModel = new ViewModels.Actions.Children.VariableViewModel(this);
        }

        public Variable(IElement parent)
        {
            Parent = parent;
            ViewModel = new ViewModels.Actions.Children.VariableViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Variable", null);
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute dontEval = d.CreateAttribute("DontEval");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Value
            name.Value = Name;
            dontEval.Value = DontEval.ToString();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(name);
            output.Attributes.Append(dontEval);
            output.Attributes.Append(condition);

            // Append Content
            output.InnerText = Content;

            return output;
        }
    }
}
