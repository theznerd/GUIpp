﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Classes
{
    public class UIpp : IElement
    {
        public bool? AlwaysOnTop { get; set; }
        public string Color { get; set; }
        public bool? DialogSidebar { get; set; }
        public bool? Flat { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string RootXMLPath { get; set; }

        public ObservableCollection<IRootElement> Elements { get; set; } = new ObservableCollection<IRootElement>();

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "UIpp", null);
            XmlAttribute alwaysOnTop = d.CreateAttribute("AlwaysOnTop");
            XmlAttribute color = d.CreateAttribute("Color");
            XmlAttribute dialogSidebar = d.CreateAttribute("DialogSidebar");
            XmlAttribute flat = d.CreateAttribute("Flat");
            XmlAttribute icon = d.CreateAttribute("Icon");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute rootXMLPath = d.CreateAttribute("RootXMLPath");

            // Assign attribute valutes
            alwaysOnTop.Value = AlwaysOnTop.ToString();
            color.Value = Color;
            dialogSidebar.Value = DialogSidebar.ToString();
            flat.Value = Flat.ToString();
            icon.Value = Icon;
            title.Value = Title;
            rootXMLPath.Value = RootXMLPath;

            // Append attributes
            if(null != AlwaysOnTop)
            {
                output.Attributes.Append(alwaysOnTop);
            }
            if (!string.IsNullOrEmpty(Color))
            {
                output.Attributes.Append(color);
            }
            if (null != DialogSidebar)
            {
                output.Attributes.Append(dialogSidebar);
            }
            if (null != Flat)
            {
                output.Attributes.Append(flat);
            }
            if (!string.IsNullOrEmpty(Icon))
            {
                output.Attributes.Append(icon);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (!string.IsNullOrEmpty(RootXMLPath))
            {
                output.Attributes.Append(rootXMLPath);
            }

            // Append Children
            foreach (IRootElement element in Elements)
            {
                XmlNode e = element.GenerateXML();
                XmlNode en = d.ImportNode(e, true);
                output.AppendChild(en);
            }

            return output;
        }
    }
}
