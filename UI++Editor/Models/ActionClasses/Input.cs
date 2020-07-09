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
    public class Input : PropertyChangedBase, IElement, IAction, IParentElement
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return true; } }
        public string ActionType { get; } = "Input";
        public string[] ValidChildren { get; set; } = { "InputCheckbox","InputChoice","InputInfo","InputText" };
        public bool? ShowBack { get; set; } = false;
        public bool? ShowCancel { get; set; } = false;
        public string Name { get; set; }
        public string Size { get; set; } // default is Regular | Regular, Tall
        public bool CenterTitle = false;
        public string Title { get; set; }
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
        public Input(IEventAggregator ea)
        {
            EventAggregator = ea;
            ViewModel = new ViewModels.Actions.InputViewModel(this);
            SubChildren = new ObservableCollection<IChildElement>();
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute showCancel = d.CreateAttribute("ShowCancel");
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute size = d.CreateAttribute("Size");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute centerTitle = d.CreateAttribute("CenterTitle");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            showBack.Value = ShowBack.ToString();
            showCancel.Value = ShowCancel.ToString();
            name.Value = Name;
            size.Value = Size;
            title.Value = Title;
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
            if (!string.IsNullOrEmpty(Name))
            {
                output.Attributes.Append(name);
            }
            if (!string.IsNullOrEmpty(Size))
            {
                output.Attributes.Append(size);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }
            output.Attributes.Append(centerTitle);

            // Append Children
            foreach (IChildElement input in SubChildren)
            {
                XmlNode importNode = d.ImportNode(input.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
