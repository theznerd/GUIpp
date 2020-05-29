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
    public class UserAuth : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "User Authentication";
        public string Attributes { get;set; }
        public bool DisableCancel { get; set; } = false;
        public string Domain { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public int? MaxRetryCount { get; set; } // no default
        public bool ShowBack { get; set; } = true;
        public string Condition { get; set; }

        public UserAuth(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.UserAuthViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute disableCancel = d.CreateAttribute("DisableCancel");
            XmlAttribute attributes = d.CreateAttribute("Attributes");
            XmlAttribute domain = d.CreateAttribute("Domain");
            XmlAttribute group = d.CreateAttribute("Group");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute maxRetryCount = d.CreateAttribute("MaxRetryCount");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            showBack.Value = ShowBack.ToString();
            disableCancel.Value = DisableCancel.ToString();
            attributes.Value = Attributes;
            domain.Value = Domain;
            group.Value = Group;
            title.Value = Title;
            maxRetryCount.Value = MaxRetryCount.ToString();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            output.Attributes.Append(showBack);
            output.Attributes.Append(disableCancel);
            if(null != MaxRetryCount)
            {
                output.Attributes.Append(maxRetryCount);
            }
            if (!string.IsNullOrEmpty(Attributes))
            {
                output.Attributes.Append(attributes);
            }
            if (!string.IsNullOrEmpty(Domain))
            {
                output.Attributes.Append(domain);
            }
            if (!string.IsNullOrEmpty(Group))
            {
                output.Attributes.Append(group);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
