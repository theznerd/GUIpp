using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;

namespace UI__Editor.Models.ActionClasses
{
    public class Input : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public string ActionType { get; } = "Input";
        public bool? ShowBack { get; set; } // default is false
        public bool? ShowCancel { get; set; } // default is false
        public bool? ADValidate { get; set; } // default is false
        public string Name { get; set; }
        public string Size { get; set; } // default is Regular | Regular, Tall
        public string Title { get; set; }
        public ObservableCollection<IInput> Inputs { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute showCancel = d.CreateAttribute("ShowCancel");
            XmlAttribute adValidate = d.CreateAttribute("ADValidate");
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute size = d.CreateAttribute("Size");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            showBack.Value = ShowBack.ToString();
            showCancel.Value = ShowCancel.ToString();
            adValidate.Value = ADValidate.ToString();
            name.Value = Name;
            size.Value = Size;
            title.Value = Title;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if(null != ShowBack)
            {
                output.Attributes.Append(showBack);
            }
            if(null != ShowCancel)
            {
                output.Attributes.Append(showCancel);
            }
            if (null != ADValidate)
            {
                output.Attributes.Append(adValidate);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                output.Attributes.Append(name);
            }
            if (!string.IsNullOrEmpty(Size))
            {
                output.Attributes.Append(size);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (IInput input in Inputs)
            {
                XmlNode importNode = d.ImportNode(input.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
