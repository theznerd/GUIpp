using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.ViewModels.Preview;
using UI__Editor.Models.ActionClasses;

namespace UI__Editor.ViewModels.Actions
{
    public class RandomStringViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Random String"; } }
        public IEventAggregator EventAggregator;

        public string HiddenAttributes
        {
            get
            {
                string ha;
                ha = "Allowed Characters: " + AllowedChars;
                ha += "\r\nLength: " + Length;
                ha += "\r\nVariable: " + Variable;
                return ha;
            }
        }

        public RandomStringViewModel(RandomString rs)
        {
            ModelClass = rs;
            EventAggregator = rs.EventAggregator;
            PreviewViewModel.EventAggregator = rs.EventAggregator;
        }

        public string AllowedChars
        {
            get { return (ModelClass as RandomString).AllowedChars; }
            set
            {
                (ModelClass as RandomString).AllowedChars = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => AllowedChars);
            }
        }

        public int Length
        {
            get { return (ModelClass as RandomString).Length; }
            set
            {
                (ModelClass as RandomString).Length = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Length);
            }
        }

        public string Variable
        {
            get { return (ModelClass as RandomString).Variable; }
            set
            {
                (ModelClass as RandomString).Variable = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Variable);
            }
        }

        public string Condition
        {
            get { return (ModelClass as RandomString).Condition; }
            set
            {
                (ModelClass as RandomString).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
