using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class SoftwareSets : IElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Software Sets"; } }
        public ObservableCollection<Set> Sets { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareSets", null);

            // Append children
            foreach(Set set in Sets)
            {
                XmlNode importNode = d.ImportNode(set.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
