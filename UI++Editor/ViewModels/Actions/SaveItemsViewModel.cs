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
    public class SaveItemsViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Save Items"; } }
        public string HiddenAttributes
        {
            get 
            {
                string ha;
                ha = "Items: " + Items;
                ha += "\r\nPath: " + Path;
                return ha;
            }
        }

        public SaveItemsViewModel(SaveItems si)
        {
            ModelClass = si;
            EventAggregator = si.EventAggregator;
        }

        public string Items
        {
            get { return (ModelClass as SaveItems).Items; }
            set
            {
                (ModelClass as SaveItems).Items = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Path
        {
            get { return (ModelClass as SaveItems).Path; }
            set
            {
                (ModelClass as SaveItems).Path = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Condition
        {
            get { return (ModelClass as SaveItems).Condition; }
            set
            {
                (ModelClass as SaveItems).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
