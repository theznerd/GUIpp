using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class Case : IElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Case"; } }
        public bool CaseInsensitive { get; set; } = false;
        public bool DontEval { get; set; } = false;
        public string RegEx { get; set; } // required
        public ObservableCollection<Variable> Variables { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Case", null);
            XmlAttribute caseInsensitive = d.CreateAttribute("CaseInsensitive");
            XmlAttribute dontEval = d.CreateAttribute("DontEval");
            XmlAttribute regEx = d.CreateAttribute("RegEx");

            // Set Attribute Values
            caseInsensitive.Value = CaseInsensitive.ToString();
            dontEval.Value = DontEval.ToString();
            regEx.Value = RegEx;

            // Append Attributes
            output.Attributes.Append(caseInsensitive);
            output.Attributes.Append(dontEval);
            output.Attributes.Append(regEx);

            // Append Children
            foreach (Variable v in Variables)
            {
                XmlNode importNode = d.ImportNode(v.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
