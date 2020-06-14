using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    public class InfoViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Info"; } }
        public IEventAggregator EventAggregator;

        public string HiddenAttributes
        {
            get
            {
                string ha;
                ha = "Name: " + Name;
                ha += "\r\nImage Path: " + Image;
                ha += "\r\nInfo Image Path: " + InfoImage;
                ha += "\r\nTimeout: " + Timeout + " seconds";
                ha += "\r\nTimeout Action: " + SelectedTimeoutAction;
                return ha;
            }
        }

        public InfoViewModel(Info info)
        {
            ModelClass = info;
            EventAggregator = info.EventAggregator;
            PreviewViewModel = new Preview.InfoViewModel(info.EventAggregator);
            (PreviewViewModel as Preview.InfoViewModel).InfoViewText = Content;
            (PreviewViewModel as Preview.InfoViewModel).Title = Title;
            (PreviewViewModel as Preview.InfoViewModel).PreviewBackButtonVisible = ShowBack == true ? true : false;
            (PreviewViewModel as Preview.InfoViewModel).PreviewCancelButtonVisible = ShowCancel == true ? true : false;
        }

        public bool? ShowBack
        {
            get { return (ModelClass as Info).ShowBack; }
            set
            {
                (ModelClass as Info).ShowBack = value;
                (PreviewViewModel as Preview.InfoViewModel).PreviewBackButtonVisible = value == true ? true : false;
            }
        }
        public bool? ShowCancel
        {
            get { return (ModelClass as Info).ShowCancel; }
            set
            {
                (ModelClass as Info).ShowCancel = value;
                (PreviewViewModel as Preview.InfoViewModel).PreviewCancelButtonVisible = value == true ? true : false;
            }
        }
        public string Name
        {
            get { return (ModelClass as Info).Name; }
            set
            {
                (ModelClass as Info).Name = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
        public string Image
        {
            get { return (ModelClass as Info).Image; }
            set
            {
                (ModelClass as Info).Image = value;
                (PreviewViewModel as Preview.InfoViewModel).Image = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
        public string InfoImage
        {
            get { return (ModelClass as Info).InfoImage; }
            set
            {
                (ModelClass as Info).InfoImage = value;
                (PreviewViewModel as Preview.InfoViewModel).InfoImage = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
        public string Title
        {
            get { return (ModelClass as Info).Title; }
            set
            {
                (ModelClass as Info).Title = value;
                (PreviewViewModel as Preview.InfoViewModel).Title = value;
            }
        }
        public int? Timeout
        {
            get { return (ModelClass as Info).Timeout; }
            set
            {
                (ModelClass as Info).Timeout = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public List<string> TimeoutAction
        {
            get
            {
                return new List<string>()
                {
                    "Continue",
                    "ContinueOnWarning",
                    "ContinueNoPreempt",
                    "Custom"
                };
            }
        }

        public string SelectedTimeoutAction
        {
            get { return (ModelClass as Info).TimeoutAction; }
            set 
            { 
                (ModelClass as Info).TimeoutAction = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                NotifyOfPropertyChange(() => SelectedTimeoutAction);
                NotifyOfPropertyChange(() => TimeoutActionVisibilityConverter);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
        public string TimeoutActionVisibilityConverter
        {
            get
            {
                return SelectedTimeoutAction == "Custom" ? "Visible" : "Collapsed";
            }
        }

        public string CustomTimeoutAction
        {
            get { return (ModelClass as Info).TimeoutAction; }
            set
            {
                (ModelClass as Info).TimeoutAction = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public bool CenterTitle
        {
            get { return (ModelClass as Info).CenterTitle; }
            set
            {
                (ModelClass as Info).CenterTitle = value;
                (PreviewViewModel as Preview.InfoViewModel).CenterTitle = value;
            }
        }

        public string Content
        {
            get { return (ModelClass as Info).Content; }
            set 
            { 
                (ModelClass as Info).Content = value;
                (PreviewViewModel as Preview.InfoViewModel).InfoViewText = value;
            }
        }
        public string Condition
        {
            get { return (ModelClass as Info).Condition; }
            set 
            { 
                (ModelClass as Info).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
