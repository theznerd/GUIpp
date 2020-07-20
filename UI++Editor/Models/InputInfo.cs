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
    public class InputInfo : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "InputInfo"; } }
        public string[] ValidChildren { get; set; }
        public string[] ValidParents { get; set; } = { "Input" };
        public string Color { get; set; } = "#000000";
        public int NumberOfLines { get; set; } = 1; // 1-2
        public string Content { get; set; }
        public string Condition { get; set; }

        public InputInfo(Input i)
        {
            Parent = i;
            ViewModel = new ViewModels.Actions.Children.InputInfoViewModel(this);
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
            XmlNode output = d.CreateNode("element", "InputInfo", null);
            XmlAttribute color = d.CreateAttribute("Color");
            XmlAttribute numberOfLines = d.CreateAttribute("NumberOfLines");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute values
            color.Value = Color;
            numberOfLines.Value = NumberOfLines.ToString();
            condition.Value = Condition;

            // Attach Attributes and Content
            if (!string.IsNullOrEmpty(Color))
            {
                output.Attributes.Append(color);
            }
            if(NumberOfLines == 2)
            {
                output.Attributes.Append(numberOfLines);
            }
            output.InnerText = Content;
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
