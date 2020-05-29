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
    public class TSVarViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Task Sequence Variable"; } }
        public string HiddenAttributes
        {
            get 
            {
                string ha;
                ha = "Don't Eval: " + DontEval;
                ha += "\r\nVariable: " + Variable;
                ha += "\r\nVariable Value: " + Content;
                return ha;
            }
        }

        public TSVarViewModel(TSVar t)
        {
            ModelClass = t;
            EventAggregator = t.EventAggregator;
        }

        public string Variable
        {
            get { return (ModelClass as TSVar).Variable; }
            set
            {
                (ModelClass as TSVar).Variable = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Variable);
            }
        }

        public bool? DontEval
        {
            get { return (ModelClass as TSVar).DontEval; }
            set
            {
                (ModelClass as TSVar).DontEval = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => DontEval);
            }
        }

        public string Content
        {
            get { return (ModelClass as TSVar).Content; }
            set
            {
                (ModelClass as TSVar).Content = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Content);
            }
        }

        public string Condition
        {
            get { return (ModelClass as TSVar).Condition; }
            set
            {
                (ModelClass as TSVar).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
