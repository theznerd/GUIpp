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
    public class Software : IElement
    {
        public ObservableCollection<ISoftware> Softwares { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Software", null);

            foreach(ISoftware software in Softwares)
            {
                output.AppendChild(software.GenerateXML());
            }

            return output;
        }
    }
}
