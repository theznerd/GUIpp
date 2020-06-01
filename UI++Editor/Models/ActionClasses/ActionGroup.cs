using Caliburn.Micro;
using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models.ActionClasses
{
    public class ActionGroup : PropertyChangedBase, IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Action Group"; } }
        public IElement Parent { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }

        private ObservableCollection<IElement> _Children = new ObservableCollection<IElement>();
        public ObservableCollection<IElement> Children
        {
            get { return _Children; }
            set
            {
                _Children = value;
                NotifyOfPropertyChange(() => Children);
            }
        }

        public void AddChild(IElement element)
        {
            element.Parent = this;
            Children.Add(element);
            NotifyOfPropertyChange(() => Children);
        }

        public void AddChild(IElement element, int index)
        {
            element.Parent = this;
            Children.Insert(index + 1, element);
            NotifyOfPropertyChange(() => Children);
        }

        public void RemoveChild(IElement element)
        {
            element.Parent = null;
            Children.Remove(element);
            NotifyOfPropertyChange(() => Children);
        }

        public ActionGroup(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.ActionGroupViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "ActionGroup", null);
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Node Values
            name.Value = Name;
            condition.Value = Condition;

            // Add Attributes to Node
            output.Attributes.Append(name);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Add all child nodes to the group
            foreach(IElement action in Children)
            {
                XmlNode importNode = d.ImportNode(action.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
