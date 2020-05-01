using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes
{
    public class Software : IElement, IRootElement
    {
        public string RootElementType { get; } = "Software";
        public ObservableCollection<ISoftware> Softwares { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Software", null);

            foreach(ISoftware software in Softwares)
            {
                XmlNode s = software.GenerateXML();
                XmlNode sn = d.ImportNode(s, true);
                output.AppendChild(sn);
            }

            return output;
        }
    }
}
