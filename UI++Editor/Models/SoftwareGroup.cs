using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using UI__Editor.ViewModels.Actions.Children;

namespace UI__Editor.Models
{
    public class SoftwareGroup : PropertyChangedBase, IElement, ISoftwareRef, IChildElement, IParentElement, IAppTreeSubChild
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "SoftwareGroup"; } }
        public string[] ValidChildren { get; set; } = { "SoftwareGroup", "SoftwareRef" };
        public string[] ValidParents { get; set; } = { "Set", "SoftwareGroup" };
        public bool Default { get; set; } = false; // default is false
        public string Id { get; set; } // required
        public string Label { get; set; } // required
        public bool Required { get; set; } = false; // default is false
        public ObservableCollection<IChildElement> SubChildren { get; set; }
        public string Condition { get; set; }

        // TreeView Specifics
        public Enums.AppTreeEnum.CheckStyle CheckStyle { get; set; } = Enums.AppTreeEnum.CheckStyle.Unchecked;
        public Enums.AppTreeEnum.IconStyle IconStyle { get; set; } = Enums.AppTreeEnum.IconStyle.Folder;

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

        public SoftwareGroup(IElement parent)
        {
            Parent = parent;
            SubChildren = new ObservableCollection<IChildElement>();
            ViewModel = new SoftwareGroupViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareGroup", null);
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute label = d.CreateAttribute("Label");
            XmlAttribute required = d.CreateAttribute("Required");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign Attribute Values
            _default.Value = Default.ToString();
            id.Value = Id;
            label.Value = Label;
            required.Value = Required.ToString();
            condition.Value = Condition;

            // Append Attributes
            if (null != Default)
            {
                output.Attributes.Append(_default);
            }
            output.Attributes.Append(id);
            output.Attributes.Append(label);
            if(null != Required)
            {
                output.Attributes.Append(required);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (ISoftwareRef softwareRef in SubChildren)
            {
                XmlNode importNode = d.ImportNode(softwareRef.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
