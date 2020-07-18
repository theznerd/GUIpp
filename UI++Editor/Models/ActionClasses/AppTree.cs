using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.Models.ActionClasses
{
    public class AppTree : PropertyChangedBase, IElement, IAction, IParentElement
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return true; } }
        public string ActionType { get; } = "App Tree";
        public string[] ValidChildren { get; set; } = { "Set" };
        public string ApplicationVariableBase { get; set; }
        public string PackageVariableBase { get; set; }
        public bool ShowBack { get; set; } = false; // default is false
        public bool ShowCancel { get; set; } = false; // default is false
        public string Title { get; set; }
        public string Size { get; set; } // default is regular | regular, tall, and extratall
        public bool Expanded { get; set; } = true; // default is true
        public bool CenterTitle = false;
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
        public AppTree(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            SubChildren = new ObservableCollection<IChildElement>();
            ViewModel = new ViewModels.Actions.AppTreeViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute applicationVariableBase = d.CreateAttribute("ApplicationVariableBase");
            XmlAttribute packageVariableBase = d.CreateAttribute("PackageVariableBase");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute showCancel = d.CreateAttribute("ShowCancel");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute size = d.CreateAttribute("Size");
            XmlAttribute expanded = d.CreateAttribute("Expanded");
            XmlAttribute centerTitle = d.CreateAttribute("CenterTitle");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            applicationVariableBase.Value = ApplicationVariableBase;
            packageVariableBase.Value = PackageVariableBase;
            showBack.Value = ShowBack.ToString();
            showCancel.Value = ShowCancel.ToString();
            title.Value = Title;
            size.Value = Size;
            expanded.Value = Expanded.ToString();
            centerTitle.Value = CenterTitle.ToString();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(ApplicationVariableBase))
            {
                output.Attributes.Append(applicationVariableBase);
            }
            if (!string.IsNullOrEmpty(PackageVariableBase))
            {
                output.Attributes.Append(packageVariableBase);
            }
            if(null != ShowBack)
            {
                output.Attributes.Append(showBack);
            }
            if (null != ShowCancel)
            {
                output.Attributes.Append(showCancel);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (!string.IsNullOrEmpty(Size))
            {
                output.Attributes.Append(size);
            }
            if (null != Expanded)
            {
                output.Attributes.Append(expanded);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }
            output.Attributes.Append(centerTitle);

            // Add all child nodes to the group
            XmlNode SoftwareSetsNode = d.CreateNode("element", "SoftwareSets", "");
            foreach (IElement sets in SubChildren)
            {
                XmlNode importNode = d.ImportNode(sets.GenerateXML(), true);
                SoftwareSetsNode.AppendChild(importNode);
            }
            output.AppendChild(SoftwareSetsNode);

            return output;
        }
    }
}
