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
    public class InputText : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "InputText"; } }
        public string[] ValidChildren { get; set; }
        public string[] ValidParents { get; set; } = { "Input" };
        public string Default { get; set; }
        public string ForceCase { get; set; }
        public string Hint { get; set; }
        public bool HScroll { get; set; } = false; // default is false
        public bool Password { get; set; } = false; // default is false
        public string Prompt { get; set; }
        public string Question { get; set; } // required
        public string RegEx { get; set; }
        public bool Required { get; set; } // default is true, True,False,Yes,No
        public string Variable { get; set; } // required
        public string ADValidate { get; set; }
        public string Condition { get; set; }

        public InputText(Input i)
        {
            Parent = i;
            ViewModel = new ViewModels.Actions.Children.InputTextViewModel(this);
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
            XmlNode output = d.CreateNode("element", "InputText", null);
            XmlAttribute adValidate = d.CreateAttribute("ADValidate");
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute forceCase = d.CreateAttribute("ForceCase");
            XmlAttribute hint = d.CreateAttribute("Hint");
            XmlAttribute hScroll = d.CreateAttribute("HScroll");
            XmlAttribute password = d.CreateAttribute("Password");
            XmlAttribute prompt = d.CreateAttribute("Prompt");
            XmlAttribute question = d.CreateAttribute("Question");
            XmlAttribute regEx = d.CreateAttribute("RegEx");
            XmlAttribute required = d.CreateAttribute("Required");
            XmlAttribute variable = d.CreateAttribute("Variable");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            adValidate.Value = ADValidate;
            _default.Value = Default;
            forceCase.Value = ForceCase;
            hint.Value = Hint;
            hScroll.Value = HScroll.ToString();
            password.Value = Password.ToString();
            prompt.Value = Prompt;
            question.Value = Question;
            regEx.Value = RegEx;
            required.Value = Required.ToString();
            variable.Value = Variable;
            condition.Value = Condition;

            // Append Attributes
            if (!string.IsNullOrEmpty(ADValidate))
            {
                output.Attributes.Append(adValidate);
            }
            if (!string.IsNullOrEmpty(Default))
            {
                output.Attributes.Append(_default);
            }
            if (!string.IsNullOrEmpty(ForceCase))
            {
                output.Attributes.Append(forceCase);
            }
            if (!string.IsNullOrEmpty(Hint))
            {
                output.Attributes.Append(hint);
            }
            if (null != HScroll)
            {
                output.Attributes.Append(hScroll);
            }
            if (null != Password)
            {
                output.Attributes.Append(password);
            }
            if (!string.IsNullOrEmpty(Prompt))
            {
                output.Attributes.Append(prompt);
            }
            if (!string.IsNullOrEmpty(Question))
            {
                output.Attributes.Append(question);
            }
            if (!string.IsNullOrEmpty(RegEx))
            {
                output.Attributes.Append(regEx);
            }
            if (null != Required)
            {
                output.Attributes.Append(required);
            }
            if (!string.IsNullOrEmpty(Variable))
            {
                output.Attributes.Append(variable);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
