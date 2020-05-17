using Caliburn.Micro;
using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    public class DefaultValuesViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Default Values"; } }
        public string HiddenAttributes
        {
            get
            {
                string ha = "Show Progress: " + ShowProgress;
                ha += "\r\nValue Types: " + ValueTypes;
                return ha;
            }
        }

        public DefaultValuesViewModel(DefaultValues actionGroup)
        {
            ModelClass = actionGroup;
            EventAggregator = (actionGroup as DefaultValues).EventAggregator;
            ValueTypes = (ModelClass as DefaultValues).GenerateValueTypes();
        }

        public bool? ShowProgress
        {
            get { return (ModelClass as DefaultValues).ShowProgress; }
            set
            {
                (ModelClass as DefaultValues).ShowProgress = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public List<DefaultValues.ValueType> ValueTypesList
        {
            get { return (ModelClass as DefaultValues).ValueTypeList; }
            set
            {
                (ModelClass as DefaultValues).ValueTypeList = value;
                NotifyOfPropertyChange(() => ValueTypesList);
            }
        }

        public void SelectedValueTypeListChanged()
        {
            ValueTypes = (ModelClass as DefaultValues).GenerateValueTypes();
        }

        private string _ValueTypes;
        public string ValueTypes
        {
            get
            {
                return _ValueTypes;
            }
            set
            {
                _ValueTypes = value;
                NotifyOfPropertyChange(() => ValueTypes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Condition
        {
            get { return (ModelClass as DefaultValues).Condition; }
            set
            {
                (ModelClass as DefaultValues).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
