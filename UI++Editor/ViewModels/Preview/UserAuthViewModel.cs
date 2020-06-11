using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using UI__Editor.Views.Actions;

namespace UI__Editor.ViewModels.Preview
{
    class UserAuthViewModel : PropertyChangedBase, IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public string WindowHeight { get; set; } = "Regular";
        public string Font { get; set; } = "Tahoma";
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return true; } }
        private bool _PreviewBackButtonVisible = true;
        public bool PreviewBackButtonVisible
        {
            get { return _PreviewBackButtonVisible; }
            set
            {
                _PreviewBackButtonVisible = value;
                NotifyOfPropertyChange(() => PreviewBackButtonVisible);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ButtonChange", ""));
            }
        }
        private bool _PreviewCancelButtonVisible = false;
        public bool PreviewCancelButtonVisible
        {
            get { return _PreviewCancelButtonVisible; }
            set
            {
                _PreviewCancelButtonVisible = value;
                NotifyOfPropertyChange(() => PreviewCancelButtonVisible);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ButtonChange", ""));
            }
        }

        public UserAuthViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        private string domain;
        public string Domain
        {
            get { return domain; }
            set
            {
                domain = value;
                NotifyOfPropertyChange(() => Domain);
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
    }
}
