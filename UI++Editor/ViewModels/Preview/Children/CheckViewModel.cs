using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Preview.Children
{
    public class CheckViewModel : PropertyChangedBase, IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewBackButtonVisible { get { return false; } }
        public bool PreviewCancelButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }
        public bool HasCustomPreview { get { return false; } }
        public string WindowHeight { get; set; }

        private string _Font;
        public string Font
        {
            get { return _Font; }
            set
            {
                _Font = value;
                NotifyOfPropertyChange(() => Font);
            }
        }

        private string _PreviewAs;
        public string PreviewAs
        {
            get { return _PreviewAs; }
            set
            {
                _PreviewAs = value;
                NotifyOfPropertyChange(() => ResultIcon);
                NotifyOfPropertyChange(() => PreviewAs);
                NotifyOfPropertyChange(() => ResultDescription);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
            }
        }

        public string ResultDescription
        {
            get
            {
                switch (PreviewAs)
                {
                    case "Pass":
                        return null;
                    case "Fail":
                        return ErrorDescription;
                    case "Warn":
                        return WarnDescription;
                    default:
                        return null;
                }
            }
        }

        public string ResultIcon
        {
            get
            {
                switch (PreviewAs)
                {
                    case "Pass":
                        return "pack://application:,,,/Images/Pass.ico";
                    case "Fail":
                        return "pack://application:,,,/Images/Fail.ico";
                    case "Warn":
                        return "pack://application:,,,/Images/Warn.ico";
                    default:
                        return "pack://application:,,,/Images/Pass.ico";
                }
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                NotifyOfPropertyChange(() => Description);
                NotifyOfPropertyChange(() => DescriptionSet);
            }
        }

        public string DescriptionSet
        {
            get { return string.IsNullOrEmpty(Description) ? "Collapsed" : "Visible"; }
        }

        private string _ErrorDescription;
        public string ErrorDescription
        {
            get { return _ErrorDescription; }
            set
            {
                _ErrorDescription = value;
                NotifyOfPropertyChange(() => ErrorDescription);
            }
        }

        private string _WarnDescription;
        public string WarnDescription
        {
            get { return _WarnDescription; }
            set
            {
                _WarnDescription = value;
                NotifyOfPropertyChange(() => WarnDescription);
            }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }
    }
}
