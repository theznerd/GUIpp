using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;
using System.Management.Instrumentation;
using UI__Editor.EventAggregators;

namespace UI__Editor.Models.ActionClasses
{
    public class Preflight : PropertyChangedBase, IElement, IAction, IParentElement
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return true; } }
        public string ActionType { get; } = "Preflight";
        public bool? ShowBack { get; set; } = false;
        public bool? ShowCancel { get; set; } = false;
        public string Title { get; set; }
        public bool? ShowOnFailureOnly { get; set; } = false;
        public string Size { get; set; }
        public int Timeout { get; set; } = 0; // default is 0, no timeout
        public string TimeoutAction { get; set; } // default is Continue | Continue, ContinueOnWarning, Cance, or custom (cancel + exitcode)
        public bool CenterTitle = false;
        public int TimeoutReturnCode { get; set; }
        public string[] ValidChildren { get; set; } = { "Check" };
        public string Condition { get; set; }

        // Because there is a preview, we need to notify of changes downstream
        private ObservableCollection<IChildElement> _SubChildren;
        public ObservableCollection<IChildElement> SubChildren
        {
            get { return _SubChildren; }
            set
            {
                _SubChildren = value;
                NotifyOfPropertyChange(() => SubChildren);
            }
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
        public Preflight(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            SubChildren = new ObservableCollection<IChildElement>();
            ViewModel = new ViewModels.Actions.PreflightViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute showCancel = d.CreateAttribute("ShowCancel");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute showOnFailureOnly = d.CreateAttribute("ShowOnFailureOnly");
            XmlAttribute size = d.CreateAttribute("Size");
            XmlAttribute timeout = d.CreateAttribute("Timeout");
            XmlAttribute timeoutAction = d.CreateAttribute("TimeoutAction");
            XmlAttribute centerTitle = d.CreateAttribute("CenterTitle");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            showBack.Value = ShowBack.ToString();
            showCancel.Value = ShowCancel.ToString();
            title.Value = Title;
            showOnFailureOnly.Value = ShowOnFailureOnly.ToString();
            size.Value = Size;
            timeout.Value = Timeout.ToString();
            if(TimeoutAction == "Custom")
            {
                timeoutAction.Value = TimeoutReturnCode.ToString();
            }
            else
            {
                timeoutAction.Value = TimeoutAction;
            }
            centerTitle.Value = CenterTitle.ToString();
            condition.Value = Condition;

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
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (null != ShowOnFailureOnly)
            {
                output.Attributes.Append(showOnFailureOnly);
            }
            if (!string.IsNullOrEmpty(Size))
            {
                output.Attributes.Append(size);
            }
            if (null != Timeout)
            {
                output.Attributes.Append(timeout);
            }
            if (!string.IsNullOrEmpty(TimeoutAction))
            {
                output.Attributes.Append(timeoutAction);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }
            output.Attributes.Append(centerTitle);

            // Append Children
            foreach (Check check in SubChildren)
            {
                XmlNode importNode = d.ImportNode(check.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
