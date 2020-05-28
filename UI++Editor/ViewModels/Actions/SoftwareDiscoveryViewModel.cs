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
    public class SoftwareDiscoveryViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Software Discovery"; } }
        public string HiddenAttributes
        {
            get 
            {
                string ha;
                ha = "Action has no hidden attributes";
                return ha;
            }
        }

        public SoftwareDiscoveryViewModel(SoftwareDiscovery si)
        {
            ModelClass = si;
            EventAggregator = si.EventAggregator;
        }

        public string Condition
        {
            get { return (ModelClass as SoftwareDiscovery).Condition; }
            set
            {
                (ModelClass as SoftwareDiscovery).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
