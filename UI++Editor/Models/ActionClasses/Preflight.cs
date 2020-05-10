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
    public class Preflight : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public string ActionType { get; } = "Preflight";
        public bool? ShowBack { get; set; }
        public bool? ShowCancel { get; set; }
        public string Title { get; set; }
        public bool? ShowOnFailureOnly { get; set; } // default is false
        public string Size { get; set; }
        public int? Timeout { get; set; } // default is 0, no timeout
        public string TimeoutAction { get; set; } // default is Continue | Continue, ContinueOnWarning, Cance, or custom (cancel + exitcode)
        public ObservableCollection<Check> Checks { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showBack = d.CreateAttribute("ShowBack");
            XmlAttribute showCancel = d.CreateAttribute("ShowCancel");
            XmlAttribute title = d.CreateAttribute("Title");
            XmlAttribute showOnFailureOnly = d.CreateAttribute("ShowOnFailureOnly");
            XmlAttribute size = d.CreateAttribute("Size");
            XmlAttribute timeout = d.CreateAttribute("Timeout");
            XmlAttribute timeoutAction = d.CreateAttribute("TimeoutAction");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            showBack.Value = ShowBack.ToString();
            showCancel.Value = ShowCancel.ToString();
            title.Value = Title;
            showOnFailureOnly.Value = ShowOnFailureOnly.ToString();
            size.Value = Size;
            timeout.Value = Timeout.ToString();
            timeoutAction.Value = TimeoutAction;
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
            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.Append(title);
            }
            if (null != ShowOnFailureOnly)
            {
                output.Attributes.Append(showOnFailureOnly);
            }
            if (!string.IsNullOrEmpty(Size))
            {
                output.Attributes.Append(size);
            }
            if (null != Timeout)
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

            // Append Children
            foreach (Check check in Checks)
            {
                XmlNode importNode = d.ImportNode(check.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
