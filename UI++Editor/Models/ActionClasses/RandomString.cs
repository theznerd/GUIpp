using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models.ActionClasses
{
    public class RandomString : PropertyChangedBase, IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "Random String";
        public string AllowedChars { get; set; } = "";
        public int Length { get; set; } = 6;
        public string Variable { get; set; }
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
        public RandomString(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.RandomStringViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute allowedChars = d.CreateAttribute("AllowedChars");
            XmlAttribute length = d.CreateAttribute("Length");
            XmlAttribute variable = d.CreateAttribute("Variable");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            allowedChars.Value = AllowedChars;
            length.Value = Length.ToString();
            variable.Value = Variable;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(AllowedChars))
            {
                output.Attributes.Append(allowedChars);
            }
            if (!string.IsNullOrEmpty(Variable))
            {
                output.Attributes.Append(variable);
            }
            output.Attributes.Append(length);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
