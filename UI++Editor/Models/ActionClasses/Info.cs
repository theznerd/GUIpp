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
    public class Info : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "Info";
        public bool? ShowBack { get; set; } = false;
        public bool? ShowCancel { get; set; } = false;
        public string Name { get; set; }
        public string Image { get; set; }
        public string InfoImage { get; set; }
        public string Title { get; set; }
        public int? Timeout { get; set; } // default is 0, no timeout
        public string TimeoutAction { get; set; } // default is continue | continue, cancel, return code (Cancel + custom exit code)
        public bool CenterTitle = false;
        public string Content { get; set; }
        public string Condition { get; set; }

        public Info(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.InfoViewModel(this);
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
            XmlAttribute timeout = d.CreateAttribute("Timeout");
            XmlAttribute timeoutAction = d.CreateAttribute("TimeoutAction");
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
            timeout.Value = Timeout.ToString();
            timeoutAction.Value = TimeoutAction;
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
            if(null != Timeout)
            {
                output.Attributes.Append(timeout);
            }
            if (!string.IsNullOrEmpty(TimeoutAction))
            {
                output.Attributes.Append(timeoutAction);
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
