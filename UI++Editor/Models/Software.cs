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
    public class Software : PropertyChangedBase, IElement, IRootElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Software"; } }
        public string RootElementType { get; } = "Software";
        public ObservableCollection<ISoftware> Softwares { get; set; }

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
        public Software()
        {
            Softwares = new ObservableCollection<ISoftware>();
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Software", null);

            foreach(ISoftware software in Softwares)
            {
                XmlNode importNode = d.ImportNode(software.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
