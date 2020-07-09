using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;
using UI__Editor.ViewModels.Preview.Children;
namespace UI__Editor.ViewModels.Actions.Children
{
    public class ChoiceViewModel : PropertyChangedBase, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Choice"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public ChoiceViewModel(Choice c)
        {
            ModelClass = c;
            EventAggregator = Globals.EventAggregator;
        }

        public string Option
        {
            get { return (ModelClass as Choice).Option; }
            set
            {
                (ModelClass as Choice).Option = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
            }
        }

        public string Value
        {
            get { return (ModelClass as Choice).Value; }
            set
            {
                (ModelClass as Choice).Value = value;
            }
        }

        public string AlternateValue
        {
            get { return (ModelClass as Choice).AlternateValue; }
            set
            {
                (ModelClass as Choice).AlternateValue = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as Choice).Condition; }
            set
            {
                (ModelClass as Choice).Condition = value;
            }
        }
    }
}
