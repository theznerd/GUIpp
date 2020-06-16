using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;

namespace UI__Editor.Models.ActionClasses
{
    public class ErrorInfo : PropertyChangedBase, IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public IElement Parent { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "Error Info";
        public bool? ShowBack { get; set; } = false;
        public bool ShowCancel { get; set; } = true;
        public string Name { get; set; }
        public string Image { get; set; }
        public string InfoImage { get; set; }
        public string Title { get; set; }
        public bool CenterTitle = false;
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
        public ErrorInfo(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.ErrorInfoViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute showCancel = d.CreateAttribute("ShowCancel");
            XmlAttribute name = d.CreateAttribute("Name");
            XmlAttribute image = d.CreateAttribute("Image");
            XmlAttribute infoImage = d.CreateAttribute("InfoImage");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute centerTitle = d.CreateAttribute("CenterTitle");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            showBack.Value = ShowBack.ToString();
            showCancel.Value = ShowCancel.ToString();
            name.Value = Name;
            image.Value = Image;
            infoImage.Value = InfoImage;
            title.Value = Title;
            centerTitle.Value = CenterTitle.ToString();
            condition.Value = Condition;

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
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }
            output.Attributes.Append(centerTitle);

            // Set Content
            output.InnerText = Content;

            return output;
        }
    }
}
