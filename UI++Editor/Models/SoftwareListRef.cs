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
    public class SoftwareListRef : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string[] ValidChildren { get; set; } = { "" };
        public string[] ValidParents { get; set; } = { "TSVarList" };
        public string ActionType { get { return "Software List Ref"; } }
        public string Id { get; set; } // required
        public string Condition { get; set; }

        public SoftwareListRef(IParentElement p)
        {
            Parent = p;
            ViewModel = new ViewModels.Actions.Children.SoftwareListRefViewModel(this);
        }

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
        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareListRef", null);
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            id.Value = Id;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(id);
            if (string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
