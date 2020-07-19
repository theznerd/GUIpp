using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;

namespace UI__Editor.Models.ActionClasses
{
    public class WMIWrite : PropertyChangedBase, IElement, IAction, IParentElement
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return true; } }
        public string[] ValidChildren { get; set; } = { "Property" };
        public string ActionType { get; } = "WMI Write";
        public string Class { get; set; } // required
        public string Namespace { get; set; } // default is root\cimv2
        public ObservableCollection<IChildElement> SubChildren { get; set; }
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
        public WMIWrite(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.WMIWriteViewModel(this);
            SubChildren = new ObservableCollection<IChildElement>();
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute _class = d.CreateAttribute("Class");
            XmlAttribute _namespace = d.CreateAttribute("Namespace");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = "WMIWrite";
            _class.Value = Class;
            _namespace.Value = Namespace;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            output.Attributes.Append(_class);
            if (!string.IsNullOrEmpty(Namespace))
            {
                output.Attributes.Append(_namespace);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach(Property property in SubChildren)
            {
                XmlNode importNode = d.ImportNode(property.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
