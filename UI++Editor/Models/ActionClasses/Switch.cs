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
    public class Switch : PropertyChangedBase, IElement, IAction, IParentElement
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return true; } }
        public string ActionType { get; } = "Switch";
        public bool? DontEval { get; set; } = false;
        public string OnValue { get; set; } // required
        public string[] ValidChildren { get; set; } = { "Case" };
        public ObservableCollection<IChildElement> SubChildren { get; set; } = new ObservableCollection<IChildElement>();
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

        public Switch(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.SwitchViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute onValue = d.CreateAttribute("OnValue");
            XmlAttribute dontEval = d.CreateAttribute("DontEval");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            onValue.Value = OnValue;
            dontEval.Value = DontEval.ToString();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            output.Attributes.Append(onValue);
            if(null != DontEval)
            {
                output.Attributes.Append(dontEval);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (Case c in SubChildren)
            {
                XmlNode importNode = d.ImportNode(c.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
