using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;
using UI__Editor.Interfaces;
using UI__Editor.Models.ActionClasses;

namespace UI__Editor.Models
{
    public class Check : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Check"; } }
        public string[] ValidParents { get; set; } = { "Preflight" };
        public string[] ValidChildren { get; set; }
        public string CheckCondition { get; set; } // required
        public string Description { get; set; }
        public string ErrorDescription { get; set; }
        public string Text { get; set; } // required
        public string WarnCondition { get; set; }
        public string WarnDescription { get; set; }
        public string Condition { get; set; }

        public Check(Preflight p)
        {
            Parent = p;
            ViewModel = new ViewModels.Actions.Children.CheckViewModel(this);
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
        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Check", null);
            XmlAttribute checkCondition = d.CreateAttribute("CheckCondition");
            XmlAttribute description = d.CreateAttribute("Description");
            XmlAttribute errorDescription = d.CreateAttribute("ErrorDescription");
            XmlAttribute text = d.CreateAttribute("Text");
            XmlAttribute warnCondition = d.CreateAttribute("WarnCondition");
            XmlAttribute warnDescription = d.CreateAttribute("WarnDescription");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            checkCondition.Value = CheckCondition;
            description.Value = Description;
            errorDescription.Value = ErrorDescription;
            text.Value = Text;
            warnCondition.Value = WarnCondition;
            warnDescription.Value = WarnDescription;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(checkCondition);
            if(!string.IsNullOrEmpty(Description))
            {
                output.Attributes.Append(description);
            }
            if (!string.IsNullOrEmpty(ErrorDescription))
            {
                output.Attributes.Append(errorDescription);
            }
            output.Attributes.Append(text);
            if (!string.IsNullOrEmpty(WarnCondition))
            {
                output.Attributes.Append(warnCondition);
            }
            if (!string.IsNullOrEmpty(WarnDescription))
            {
                output.Attributes.Append(warnDescription);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
