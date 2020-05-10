using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using UI__Editor.Views.Actions;

namespace UI__Editor.ViewModels.Preview
{
    class InfoViewModel : PropertyChangedBase, IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public bool PreviewRefreshButtonVisible { get { return false; } }
        private bool _PreviewBackButtonVisible;
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
        private bool _PreviewCancelButtonVisible;
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
        public bool PreviewAcceptButtonVisible { get { return true; } }

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

        private string _InfoViewText;
        public string InfoViewText
        {
            get { return _InfoViewText; }
            set
            {
                _InfoViewText = value;
                NotifyOfPropertyChange(() => InfoViewText);
            }
        }
    }
}
