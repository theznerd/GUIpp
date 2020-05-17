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
    public class Software : IElement, IRootElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Software"; } }
        public string RootElementType { get; } = "Software";
        public ObservableCollection<ISoftware> Softwares { get; set; }

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
