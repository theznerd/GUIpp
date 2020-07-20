using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using UI__Editor.Models.ActionClasses;

namespace UI__Editor.Models
{
    public class InputCheckbox : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "InputCheckbox"; } }
        public string[] ValidChildren { get; set; }
        public string[] ValidParents { get; set; } = { "Input" };
        public string CheckedValue { get; set; } = "True";
        public string Default { get; set; }
        public string Question { get; set; } // required
        public string Variable { get; set; } // required
        public string UncheckedValue { get; set; } = "False";
        public string Condition { get; set; }

        public InputCheckbox(Input i)
        {
            Parent = i;
            ViewModel = new ViewModels.Actions.Children.InputCheckboxViewModel(this);
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
            XmlNode output = d.CreateNode("element", "InputCheckbox", null);
            XmlAttribute checkedValue = d.CreateAttribute("CheckedValue");
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute question = d.CreateAttribute("Question");
            XmlAttribute variable = d.CreateAttribute("Variable");
            XmlAttribute uncheckedValue = d.CreateAttribute("UncheckedValue");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            checkedValue.Value = CheckedValue;
            _default.Value = Default;
            question.Value = Question;
            variable.Value = Variable;
            uncheckedValue.Value = UncheckedValue;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(checkedValue);
            if (!string.IsNullOrEmpty(Default))
            {
                output.Attributes.Append(_default);
            }
            output.Attributes.Append(question);
            output.Attributes.Append(variable);
            output.Attributes.Append(uncheckedValue);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
