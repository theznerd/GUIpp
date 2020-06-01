using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class CheckboxInput : IInput, IElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public ViewModels.Actions.Children.IInput ChildViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Checkbox Input"; } }
        public string CheckedValue { get; set; } = "True";
        public string Default { get; set; }
        public string Question { get; set; } // required
        public string Variable { get; set; } // required
        public string UncheckedValue { get; set; } = "False";

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "CheckboxInput", null);
            XmlAttribute checkedValue = d.CreateAttribute("CheckedValue");
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute question = d.CreateAttribute("Question");
            XmlAttribute variable = d.CreateAttribute("Variable");
            XmlAttribute uncheckedValue = d.CreateAttribute("UncheckedValue");

            // Set Attribute Values
            checkedValue.Value = CheckedValue;
            _default.Value = Default;
            question.Value = Question;
            variable.Value = Variable;
            uncheckedValue.Value = UncheckedValue;

            // Append Attributes
            output.Attributes.Append(checkedValue);
            if (!string.IsNullOrEmpty(Default))
            {
                output.Attributes.Append(_default);
            }
            output.Attributes.Append(question);
            output.Attributes.Append(variable);
            output.Attributes.Append(uncheckedValue);

            return output;
        }
    }
}
