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
    public class WMIWriteViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "WMI Write"; } }
        public string HiddenAttributes
        {
            get 
            {
                string ha;
                ha = "Class: " + Class;
                ha += "\r\nNamespace: " + Namespace;
                return ha;
            }
        }

        public WMIWriteViewModel(WMIWrite t)
        {
            ModelClass = t;
            EventAggregator = t.EventAggregator;
        }

        public string Class
        {
            get { return (ModelClass as WMIWrite).Class; }
            set
            {
                (ModelClass as WMIWrite).Class = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Class);
            }
        }

        public string Namespace
        {
            get { return (ModelClass as WMIWrite).Namespace; }
            set
            {
                (ModelClass as WMIWrite).Namespace = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Namespace);
            }
        }

        public string Condition
        {
            get { return (ModelClass as WMIWrite).Condition; }
            set
            {
                (ModelClass as WMIWrite).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
