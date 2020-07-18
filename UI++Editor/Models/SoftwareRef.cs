using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using UI__Editor.ViewModels.Actions.Children;

namespace UI__Editor.Models
{
    public class SoftwareRef : PropertyChangedBase, IElement, ISoftwareRef, IChildElement, IAppTreeSubChild
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "SoftwareRef"; } }
        public string[] ValidChildren { get; set; }
        public string[] ValidParents { get; set; } = { "Set", "SoftwareGroup" };
        public string Id { get; set; } // required
        public bool Hidden { get; set; } = false; // default is false
        public bool Default { get; set; } = false; // default is false
        public bool Required { get; set; } = false; // default is false
        public string Condition { get; set; }

        // TreeView Specifics
        public Enums.AppTreeEnum.CheckStyle CheckStyle { get; set; } = Enums.AppTreeEnum.CheckStyle.Unchecked;
        public Enums.AppTreeEnum.IconStyle IconStyle { get; set; } = Enums.AppTreeEnum.IconStyle.App;
        public string Label { get; set; }

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

        public SoftwareRef(IElement parent)
        {
            Parent = parent;
            ViewModel = new SoftwareRefViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareRef", null);
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute hidden = d.CreateAttribute("Hidden");
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute required = d.CreateAttribute("Required");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            id.Value = Id;
            hidden.Value = Hidden.ToString();
            _default.Value = Default.ToString();
            required.Value = Required.ToString();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(id);
            if (null != Hidden)
            {
                output.Attributes.Append(hidden);
            }
            if (null != Default)
            {
                output.Attributes.Append(_default);
            }
            if (null != Required)
            {
                output.Attributes.Append(required);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
