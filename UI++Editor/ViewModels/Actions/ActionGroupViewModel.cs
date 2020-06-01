using Caliburn.Micro;
using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    public class ActionGroupViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Action Group"; } }
        public string HiddenAttributes
        {
            get { return "Name: " + Name; }
        }

        public ActionGroupViewModel(ActionGroup actionGroup)
        {
            ModelClass = actionGroup;
            EventAggregator = (actionGroup as ActionGroup).EventAggregator;
        }

        public string Name
        {
            get { return (ModelClass as ActionGroup).Name; }
            set
            {
                (ModelClass as ActionGroup).Name = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Condition
        {
            get { return (ModelClass as ActionGroup).Condition; }
            set
            {
                (ModelClass as ActionGroup).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
