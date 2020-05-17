using Caliburn.Micro;
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
    public class Actions : IElement, IRootElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string RootElementType { get; } = "Actions";
        public string ActionType { get { return "Actions"; } }

        public ObservableCollection<Interfaces.IAction> actions;

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Actions", null);

            // Append all children
            foreach(Interfaces.IAction action in actions)
            {
                XmlNode importNode = d.ImportNode(action.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
