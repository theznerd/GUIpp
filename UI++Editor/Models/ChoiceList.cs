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

namespace UI__Editor.Models
{
    public class ChoiceList : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "ChoiceList"; } }
        public string[] ValidParents { get; set; } = { "InputChoice" };
        public string[] ValidChildren { get; set; }
        public string AlternateValueList { get; set; }
        public string OptionList { get; set; }
        public string ValueList { get; set; }
        public string Condition { get; set; }

        public ChoiceList(IElement p)
        {
            Parent = p;
            ViewModel = new ViewModels.Actions.Children.ChoiceListViewModel(this);
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
            XmlNode output = d.CreateNode("element", "ChoiceList", null);
            XmlAttribute optionList = d.CreateAttribute("OptionList");
            XmlAttribute valueList = d.CreateAttribute("ValueList");
            XmlAttribute alternateValueList = d.CreateAttribute("AlternateValueList");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            optionList.Value = OptionList;
            valueList.Value = ValueList;
            alternateValueList.Value = AlternateValueList;
            condition.Value = Condition;

            // Append Attribute
            if (!string.IsNullOrEmpty(OptionList))
            {
                output.Attributes.Append(optionList);
            }
            if (!string.IsNullOrEmpty(ValueList))
            {
                output.Attributes.Append(valueList);
            }
            if(!string.IsNullOrEmpty(AlternateValueList))
            {
                output.Attributes.Append(alternateValueList);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
