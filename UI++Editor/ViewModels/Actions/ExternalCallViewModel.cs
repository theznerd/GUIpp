using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    class ExternalCallViewModel : IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "External Call"; } }
        public string HiddenAttributes
        {
            get
            {
                string output = "";
                output += "Title: " + Title;
                output += "\r\nCommand Line: " + CommandLine;
                output += "\r\nMax Runtime: " + MaxRuntime + " seconds";
                return output;
            }
        }

        public ExternalCallViewModel(ExternalCall ec)
        {
            ModelClass = ec;
            EventAggregator = (ec as ExternalCall).EventAggregator;
        }

        public string Title
        {
            get { return (ModelClass as ExternalCall).Title; }
            set
            {
                (ModelClass as ExternalCall).Title = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string CommandLine
        {
            get { return (ModelClass as ExternalCall).Content; }
            set
            {
                (ModelClass as ExternalCall).Content = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public int? MaxRuntime
        {
            get { return (ModelClass as ExternalCall).MaxRunTime; }
            set
            {
                (ModelClass as ExternalCall).MaxRunTime = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Condition
        {
            get { return (ModelClass as ExternalCall).Condition; }
            set
            {
                (ModelClass as ExternalCall).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }

        
    }
}
