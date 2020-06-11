using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using UI__Editor.Views.Actions;

namespace UI__Editor.ViewModels.Preview
{
    class ErrorInfoViewModel : PropertyChangedBase, IPreview, IHandle<EventAggregators.SendMessage>
    {
        public IEventAggregator EventAggregator { get; set; }
        public string WindowHeight { get; set; } = "Regular";
        public string Font { get; set; } = "Tahoma";
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }

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

        public ErrorInfoViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            EventAggregator.Subscribe(this);
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

        private string _Image;
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                NotifyOfPropertyChange(() => Image);
                NotifyOfPropertyChange(() => ImageVisibilityConverter);
            }
        }

        public void Handle(EventAggregators.SendMessage message)
        {
            switch (message.Type)
            {
                case "DialogVisible":
                    HideWebBrowser = (bool)message.Data;
                    break;
                default:
                    break;
            }
        }

        private bool _HideWebBrowser = false;
        public bool HideWebBrowser
        {
            get { return _HideWebBrowser; }
            set
            {
                _HideWebBrowser = value;
                NotifyOfPropertyChange(() => WebBrowserVisibilityConverter);
                NotifyOfPropertyChange(() => HideWebBrowser);
            }
        }
        public string WebBrowserVisibilityConverter
        {
            get
            {
                return HideWebBrowser ? "Hidden" : "Visible";
            }
        }

        public string ImageVisibilityConverter
        {
            get
            {
                return !string.IsNullOrEmpty(Image) ? "Visible" : "Collapsed";
            }
        }

        private string _InfoImage;
        public string InfoImage
        {
            get { return _InfoImage; }
            set
            {
                _InfoImage = value;
                NotifyOfPropertyChange(() => InfoImage);
            }
        }
    }
}
