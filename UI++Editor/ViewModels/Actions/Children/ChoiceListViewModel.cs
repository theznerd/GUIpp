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
    public class ChoiceListViewModel : PropertyChangedBase, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Choice List"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public ChoiceListViewModel(ChoiceList c)
        {
            ModelClass = c;
            EventAggregator = Globals.EventAggregator;
        }

        public string OptionList
        {
            get { return (ModelClass as ChoiceList).OptionList; }
            set
            {
                (ModelClass as ChoiceList).OptionList = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
            }
        }

        public string ValueList
        {
            get { return (ModelClass as ChoiceList).ValueList; }
            set
            {
                (ModelClass as ChoiceList).ValueList = value;
            }
        }

        public string AlternateValueList
        {
            get { return (ModelClass as ChoiceList).AlternateValueList; }
            set
            {
                (ModelClass as ChoiceList).AlternateValueList = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as ChoiceList).Condition; }
            set
            {
                (ModelClass as ChoiceList).Condition = value;
            }
        }
    }
}
