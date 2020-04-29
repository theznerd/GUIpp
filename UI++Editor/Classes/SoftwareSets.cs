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
    public class SoftwareSets : IElement
    {
        public ObservableCollection<Set> Sets { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "SoftwareSets", null);

            // Append children
            foreach(Set set in Sets)
            {
                output.AppendChild(set.GenerateXML());
            }

            return output;
        }
    }
}
