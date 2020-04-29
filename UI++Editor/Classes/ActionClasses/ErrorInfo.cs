using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class ErrorInfo : IElement, IAction
    {
        public string Type { get; } = "ErrorInfo";
        public bool? ShowBack { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string InfoImage { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute image = d.CreateAttribute("Image");
            XmlAttribute infoImage = d.CreateAttribute("InfoImage");
            XmlAttribute title = d.CreateAttribute("Title");

            // Assign attribute values
            type.Value = Type;
            showBack.Value = ShowBack.ToString();
            name.Value = Name;
            image.Value = Image;
            infoImage.Value = InfoImage;
            title.Value = Title;

            // Append Attributes
            output.Attributes.Append(type);
            if(null != ShowBack)
            {
                output.Attributes.Append(showBack);
            }
            if(!string.IsNullOrEmpty(Name))
            {
                output.Attributes.Append(name);
            }
            if (!string.IsNullOrEmpty(Image))
            {
                output.Attributes.Append(image);
            }
            if (!string.IsNullOrEmpty(InfoImage))
            {
                output.Attributes.Append(infoImage);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }

            // Set Content
            output.InnerText = Content;

            return output;
        }
    }
}
