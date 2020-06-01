using Caliburn.Micro;
using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    public class WMIReadViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "WMI Read"; } }
        public string HiddenAttributes
        {
            get 
            {
                string ha;
                ha = "Class: " + Class;
                ha += "\r\nDefault Value: " + Default;
                ha += "\r\nKey Qualifier: " + KeyQualifier;
                ha += "\r\nNamespace: " + Namespace;
                ha += "\r\nProperty: " + Property;
                ha += "\r\nVariable: " + Variable;
                ha += "\r\nQuery: " + Query;
                return ha;
            }
        }

        public WMIReadViewModel(WMIRead t)
        {
            ModelClass = t;
            EventAggregator = t.EventAggregator;
        }

        public string Class
        {
            get { return (ModelClass as WMIRead).Class; }
            set
            {
                (ModelClass as WMIRead).Class = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Class);
            }
        }

        public string Default
        {
            get { return (ModelClass as WMIRead).Default; }
            set
            {
                (ModelClass as WMIRead).Default = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Default);
            }
        }

        public string KeyQualifier
        {
            get { return (ModelClass as WMIRead).KeyQualifier; }
            set
            {
                (ModelClass as WMIRead).KeyQualifier = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => KeyQualifier);
            }
        }

        public string Namespace
        {
            get { return (ModelClass as WMIRead).Namespace; }
            set
            {
                (ModelClass as WMIRead).Namespace = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Namespace);
            }
        }

        public string Property
        {
            get { return (ModelClass as WMIRead).Property; }
            set
            {
                (ModelClass as WMIRead).Property = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Property);
            }
        }

        public string Variable
        {
            get { return (ModelClass as WMIRead).Variable; }
            set
            {
                (ModelClass as WMIRead).Variable = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Variable);
            }
        }

        public string Query
        {
            get { return (ModelClass as WMIRead).Query; }
            set
            {
                (ModelClass as WMIRead).Query = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Query);
            }
        }

        public string Condition
        {
            get { return (ModelClass as WMIRead).Condition; }
            set
            {
                (ModelClass as WMIRead).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
