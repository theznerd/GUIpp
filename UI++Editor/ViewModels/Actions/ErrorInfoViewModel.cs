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
    public class ErrorInfoViewModel : PropertyChangedBase, IAction
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
                return ha;
            }
        }

        public ErrorInfoViewModel(ErrorInfo info)
        {
            ModelClass = info;
            EventAggregator = info.EventAggregator;
            PreviewViewModel = new Preview.ErrorInfoViewModel(info.EventAggregator);
            (PreviewViewModel as Preview.ErrorInfoViewModel).InfoViewText = Content;
            (PreviewViewModel as Preview.ErrorInfoViewModel).Title = Title;
            (PreviewViewModel as Preview.ErrorInfoViewModel).PreviewBackButtonVisible = ShowBack == true ? true : false;
            (PreviewViewModel as Preview.ErrorInfoViewModel).PreviewCancelButtonVisible = ShowCancel;
        }

        public bool? ShowBack
        {
            get { return (ModelClass as ErrorInfo).ShowBack; }
            set
            {
                (ModelClass as ErrorInfo).ShowBack = value;
                (PreviewViewModel as Preview.ErrorInfoViewModel).PreviewBackButtonVisible = value == true ? true : false;
            }
        }

        public bool ShowCancel
        {
            get { return (ModelClass as ErrorInfo).ShowCancel; }
            set
            {
                (ModelClass as ErrorInfo).ShowCancel = value;
                (PreviewViewModel as Preview.ErrorInfoViewModel).PreviewCancelButtonVisible = value;
            }
        }

        public string Name
        {
            get { return (ModelClass as ErrorInfo).Name; }
            set
            {
                (ModelClass as ErrorInfo).Name = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
        public string Image
        {
            get { return (ModelClass as ErrorInfo).Image; }
            set
            {
                (ModelClass as ErrorInfo).Image = value;
                (PreviewViewModel as Preview.ErrorInfoViewModel).Image = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
        public string InfoImage
        {
            get { return (ModelClass as ErrorInfo).InfoImage; }
            set
            {
                (ModelClass as ErrorInfo).InfoImage = value;
                (PreviewViewModel as Preview.ErrorInfoViewModel).InfoImage = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
        public string Title
        {
            get { return (ModelClass as ErrorInfo).Title; }
            set
            {
                (ModelClass as ErrorInfo).Title = value;
                (PreviewViewModel as Preview.ErrorInfoViewModel).Title = value;
            }
        }

        public string Content
        {
            get { return (ModelClass as ErrorInfo).Content; }
            set 
            { 
                (ModelClass as ErrorInfo).Content = value;
                (PreviewViewModel as Preview.ErrorInfoViewModel).InfoViewText = value;
            }
        }
        public string Condition
        {
            get { return (ModelClass as ErrorInfo).Condition; }
            set 
            { 
                (ModelClass as ErrorInfo).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
