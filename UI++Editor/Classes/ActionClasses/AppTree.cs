using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes.ActionClasses
{
    public class AppTree : IElement, IAction
    {
        public string Type { get; } = "AppTree";
        public string ApplicationVariableBase { get; set; }
        public string PackageVariableBase { get; set; }
        public bool? ShowBack { get; set; } // default is false
        public bool? ShowCancel { get; set; } // default is false
        public string Title { get; set; }
        public string Size { get; set; } // default is regular | regular, tall, and extratall
        public bool? Expanded { get; set; } // default is true
        public SoftwareSets Sets { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute applicationVariableBase = d.CreateAttribute("ApplicationVariableBase");
            XmlAttribute packageVariableBase = d.CreateAttribute("PackageVariableBase");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute showCancel = d.CreateAttribute("ShowCancel");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute size = d.CreateAttribute("Size");
            XmlAttribute expanded = d.CreateAttribute("Expanded");

            // Assign attribute values
            type.Value = Type;
            applicationVariableBase.Value = ApplicationVariableBase;
            packageVariableBase.Value = PackageVariableBase;
            showBack.Value = ShowBack.ToString();
            showCancel.Value = ShowCancel.ToString();
            title.Value = Title;
            size.Value = Size;
            expanded.Value = Expanded.ToString();

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(ApplicationVariableBase))
            {
                output.Attributes.Append(applicationVariableBase);
            }
            if (!string.IsNullOrEmpty(PackageVariableBase))
            {
                output.Attributes.Append(packageVariableBase);
            }
            if(null != ShowBack)
            {
                output.Attributes.Append(showBack);
            }
            if (null != ShowCancel)
            {
                output.Attributes.Append(showCancel);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (!string.IsNullOrEmpty(Size))
            {
                output.Attributes.Append(size);
            }
            if (null != Expanded)
            {
                output.Attributes.Append(expanded);
            }

            // Append Child
            output.AppendChild(Sets.GenerateXML());

            return output;
        }
    }
}
