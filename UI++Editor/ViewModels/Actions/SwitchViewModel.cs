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
    public class SwitchViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Switch"; } }
        public string HiddenAttributes
        {
            get 
            {
                string ha;
                ha = "Don't Eval: " + DontEval;
                ha += "\r\nOn Value: " + OnValue;
                return ha;
            }
        }

        public SwitchViewModel(Switch s)
        {
            ModelClass = s;
            EventAggregator = s.EventAggregator;
        }

        public bool? DontEval
        {
            get { return (ModelClass as Switch).DontEval; }
            set
            {
                (ModelClass as Switch).DontEval = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string OnValue
        {
            get { return (ModelClass as Switch).OnValue; }
            set
            {
                (ModelClass as Switch).OnValue = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Condition
        {
            get { return (ModelClass as Switch).Condition; }
            set
            {
                (ModelClass as Switch).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
