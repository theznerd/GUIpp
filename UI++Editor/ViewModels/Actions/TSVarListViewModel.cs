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
    public class TSVarListViewModel : PropertyChangedBase, IAction
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
                ha = "Application Variable Base: " + ApplicationVariableBase;
                ha += "\r\nPackage Variable Base: " + PackageVariableBase;
                return ha;
            }
        }

        public TSVarListViewModel(TSVarList t)
        {
            ModelClass = t;
            EventAggregator = t.EventAggregator;
        }

        public string ApplicationVariableBase
        {
            get { return (ModelClass as TSVarList).ApplicationVariableBase; }
            set
            {
                (ModelClass as TSVarList).ApplicationVariableBase = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => ApplicationVariableBase);
            }
        }

        public string PackageVariableBase
        {
            get { return (ModelClass as TSVarList).PackageVariableBase; }
            set
            {
                (ModelClass as TSVarList).PackageVariableBase = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => PackageVariableBase);
            }
        }

        public string Condition
        {
            get { return (ModelClass as TSVarList).Condition; }
            set
            {
                (ModelClass as TSVarList).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
