using Caliburn.Micro;
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
    public class UIpp : PropertyChangedBase, IElement
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "UI++ Element"; } }
        public bool? AlwaysOnTop { get; set; } = true;
        public string Color { get; set; }
        public bool? DialogSidebar { get; set; } = true;
        public bool? Flat { get; set; } = false;
        public string Icon { get; set; }
        public string Title { get; set; }
        public string RootXMLPath { get; set; }

        public Software Software { get; set; }
        public Actions Actions { get; set; }
        public Messages Messages { get; set; }

        // Code to handle TreeView Selection
        private bool _TVSelected = false;
        public bool TVSelected
        {
            get { return _TVSelected; }
            set
            {
                _TVSelected = value;
                NotifyOfPropertyChange(() => TVSelected);
            }
        }
        public UIpp()
        {
            Software = new Software();
            Actions = new Actions();
            Messages = new Messages();
        }

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

            // Append Software
            if(Software.Softwares.Count != 0)
            {
                XmlNode importNode = d.ImportNode(Software.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            // Append Messages
            if(Messages.MessageCollection.Count != 0)
            {
                XmlNode importNode = d.ImportNode(Messages.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            // Append Actions
            if (Actions.actions.Count != 0)
            {
                XmlNode importNode = d.ImportNode(Actions.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
