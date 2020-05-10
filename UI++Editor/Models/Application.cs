using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class Application : PropertyChangedBase, IElement, ISoftware
    {
        private string _Id;
        public string Id // required
        {
            get { return _Id; }
            set
            {
                _Id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }
        public string IncludeID { get; set; }
        private string _Label;
        public string Label // required
        {
            get { return _Label; }
            set
            {
                _Label = value;
                NotifyOfPropertyChange(() => Label);
            }
        }
        private string _Name;
        public string Name // required
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public string Type { get { return this.GetType().Name; } }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Application", null);
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute includeID = d.CreateAttribute("IncludeID");
            XmlAttribute label = d.CreateAttribute("Label");
            XmlAttribute name = d.CreateAttribute("Name");

            // Set Attribute Values
            id.Value = Id;
            includeID.Value = IncludeID;
            label.Value = Label;
            name.Value = Name;

            // Append Attributes to Node
            output.Attributes.Append(id);
            output.Attributes.Append(label);
            output.Attributes.Append(name);
            if(!string.IsNullOrEmpty(IncludeID))
            {
                output.Attributes.Append(includeID);
            }

            return output;
        }
    }
}
