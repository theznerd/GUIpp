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
    public class UserAuthViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "User Authentication"; } }
        public string HiddenAttributes
        {
            get 
            {
                string ha;
                ha = "Attributes: " + Attributes;
                ha += "\r\nGroup: " + Group;
                ha += "\r\nDomain Conroller: " + DomainController;
                ha += "\r\nDo Not Fallback: " + DoNotFallback;
                ha += "\r\nMax Retry Count: " + MaxRetryCount;
                return ha;
            }
        }

        public UserAuthViewModel(UserAuth t)
        {
            ModelClass = t;
            EventAggregator = t.EventAggregator;
            PreviewViewModel = new Preview.UserAuthViewModel(EventAggregator);
            (PreviewViewModel as Preview.UserAuthViewModel).PreviewBackButtonVisible = ShowBack;
            (PreviewViewModel as Preview.UserAuthViewModel).PreviewCancelButtonVisible = DisableCancel;
        }

        public string Attributes
        {
            get { return (ModelClass as UserAuth).Attributes; }
            set
            {
                (ModelClass as UserAuth).Attributes = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Attributes);
            }
        }

        public bool DisableCancel
        {
            get { return (ModelClass as UserAuth).DisableCancel; }
            set
            {
                (ModelClass as UserAuth).DisableCancel = value;
                (PreviewViewModel as Preview.UserAuthViewModel).PreviewCancelButtonVisible = value;
                NotifyOfPropertyChange(() => DisableCancel);
            }
        }

        public string Domain
        {
            get { return (ModelClass as UserAuth).Domain; }
            set
            {
                (ModelClass as UserAuth).Domain = value;
                (PreviewViewModel as Preview.UserAuthViewModel).Domain = value;
                NotifyOfPropertyChange(() => Domain);
            }
        }

        public string Group
        {
            get { return (ModelClass as UserAuth).Group; }
            set
            {
                (ModelClass as UserAuth).Group = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Group);
            }
        }

        public string Title
        {
            get { return (ModelClass as UserAuth).Title; }
            set
            {
                (ModelClass as UserAuth).Title = value;
                (PreviewViewModel as Preview.UserAuthViewModel).Title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public int? MaxRetryCount
        {
            get { return (ModelClass as UserAuth).MaxRetryCount; }
            set
            {
                (ModelClass as UserAuth).MaxRetryCount = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => MaxRetryCount);
            }
        }
        
        public bool GetGroups
        {
            get { return (ModelClass as UserAuth).GetGroups; }
            set
            {
                (ModelClass as UserAuth).GetGroups = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => ShowBack);
            }
        }

        public bool ShowBack
        {
            get { return (ModelClass as UserAuth).ShowBack; }
            set
            {
                (ModelClass as UserAuth).ShowBack = value;
                (PreviewViewModel as Preview.UserAuthViewModel).PreviewBackButtonVisible = value;
                NotifyOfPropertyChange(() => ShowBack);
            }
        }

        public string DomainController
        {
            get { return (ModelClass as UserAuth).DomainController; }
            set
            {
                (ModelClass as UserAuth).DomainController = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => DomainController);
            }
        }

        public bool DoNotFallback
        {
            get { return (ModelClass as UserAuth).DoNotFallback; }
            set
            {
                (ModelClass as UserAuth).DoNotFallback = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => DoNotFallback);
            }
        }

        public string Condition
        {
            get { return (ModelClass as UserAuth).Condition; }
            set
            {
                (ModelClass as UserAuth).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
