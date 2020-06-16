using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;
using UI__Editor.Interfaces;
using UI__Editor.ViewModels.Actions.Children;

namespace UI__Editor.Models
{
    public class Case : PropertyChangedBase, IElement, IChildElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public string[] ValidChildren { get; set; } = { "Variable" };
        public string[] ValidParents { get; set; } = { "Switch" };
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Case"; } }
        public bool CaseInsensitive { get; set; } = false;
        public bool DontEval { get; set; } = false;
        public string RegEx { get; set; } // required
        public string Condition { get; set; }
        public ObservableCollection<IElement> Children { get; set; } = new ObservableCollection<IElement>();

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
        public Case()
        {
            ViewModel = new CaseViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Case", null);
            XmlAttribute caseInsensitive = d.CreateAttribute("CaseInsensitive");
            XmlAttribute dontEval = d.CreateAttribute("DontEval");
            XmlAttribute regEx = d.CreateAttribute("RegEx");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Set Attribute Values
            caseInsensitive.Value = CaseInsensitive.ToString();
            dontEval.Value = DontEval.ToString();
            regEx.Value = RegEx;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(caseInsensitive);
            output.Attributes.Append(dontEval);
            output.Attributes.Append(regEx);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (Variable v in Children)
            {
                XmlNode importNode = d.ImportNode(v.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
