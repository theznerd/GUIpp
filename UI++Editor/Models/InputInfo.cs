using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class InputInfo : IElement, IInput
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public ViewModels.Actions.Children.IInput ChildViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Input Info"; } }
        public string Color { get; set; }
        public int NumberOfLines { get; set; } = 1; // 1-2
        public string Content { get; set; } 

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "InputInfo", null);
            XmlAttribute color = d.CreateAttribute("Color");
            XmlAttribute numberOfLines = d.CreateAttribute("NumberOfLines");

            // Set Attribute values
            color.Value = Color;
            numberOfLines.Value = NumberOfLines.ToString();

            // Attach Attributes and Content
            if (!string.IsNullOrEmpty(Color))
            {
                output.Attributes.Append(color);
            }
            if(NumberOfLines == 2)
            {
                output.Attributes.Append(numberOfLines);
            }
            output.InnerText = Content;

            return output;
        }
    }
}
