using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Interfaces;
using Caliburn.Micro;
using System.Xml;

namespace UI__Editor.Models.ActionClasses
{
    public class InfoFullScreen : PropertyChangedBase, IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "Info Full Screen";
        public string Image { get; set; }
        public string BackgroundColor { get; set; } = "#002147";
        public string TextColor { get; set; } = "#FFFFFF";
        public string Content { get; set; }
        public string Condition { get; set; }

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
        public InfoFullScreen(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.InfoFullScreenViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute image = d.CreateAttribute("Image");
            XmlAttribute backgroundColor = d.CreateAttribute("BackgroundColor");
            XmlAttribute textColor = d.CreateAttribute("TextColor");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = "InfoFullScreen";
            image.Value = Image;
            backgroundColor.Value = BackgroundColor;
            textColor.Value = TextColor;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(BackgroundColor))
            {
                output.Attributes.Append(backgroundColor);
            }
            if (!string.IsNullOrEmpty(TextColor))
            {
                output.Attributes.Append(textColor);
            }
            if (!string.IsNullOrEmpty(Image))
            {
                output.Attributes.Append(image);
            }
            output.InnerText = Content;

            return output;
        }
    }
}
