using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Preview
{
    public class InputViewModel : PropertyChangedBase, IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public string WindowHeight { get; set; } = "Regular";
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return true; } }
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

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
    }
}
