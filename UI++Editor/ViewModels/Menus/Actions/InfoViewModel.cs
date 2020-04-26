using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class InfoViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "Info"; } }
        public string XMLOutput()
        {
            return "";
        }
        private ObservableCollection<object> _Children;
        public ObservableCollection<object> Children
        {
            get { return _Children; }
            set
            {
                _Children = value;
                NotifyOfPropertyChange(() => Children);
            }
        }

        private string _ConditionString;
        public string ConditionString
        {
            get { return _ConditionString; }
            set
            {
                _ConditionString = value;
                NotifyOfPropertyChange(() => ConditionString);
            }
        }

        private string _InfoTitle;
        public string InfoTitle
        {
            get { return _InfoTitle; }
            set
            {
                _InfoTitle = value;
                NotifyOfPropertyChange(() => InfoTitle);
            }
        }

        private string _InfoName;
        public string InfoName
        {
            get { return _InfoName; }
            set
            {
                _InfoName = value;
                NotifyOfPropertyChange(() => InfoName);
            }
        }

        private string _InfoData;
        public string InfoData
        {
            get { return _InfoData; }
            set
            {
                _InfoData = value;
                NotifyOfPropertyChange(() => InfoData);
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

        private string _InfoInfoImage;
        public string InfoInfoImage
        {
            get { return _InfoInfoImage; }
            set
            {
                _InfoInfoImage = value;
                NotifyOfPropertyChange(() => InfoInfoImage);
            }
        }

        private string _InfoTimeout;
        public string InfoTimeout
        {
            get { return _InfoTimeout; }
            set
            {
                _InfoTimeout = value;
                NotifyOfPropertyChange(() => InfoTimeout);
            }
        }

        private string _InfoTimeoutAction;
        public string InfoTimeoutAction
        {
            get { return _InfoTimeoutAction; }
            set
            {
                _InfoTimeoutAction = value;
                NotifyOfPropertyChange(() => InfoTimeoutAction);
            }
        }

        private string _InfoCustomTimeoutAction;
        public string InfoCustomTimeoutAction
        {
            get { return _InfoCustomTimeoutAction; }
            set
            {
                _InfoCustomTimeoutAction = value;
                NotifyOfPropertyChange(() => InfoCustomTimeoutAction);
            }
        }

        private bool _InfoShowBack;
        public bool InfoShowBack
        {
            get { return _InfoShowBack; }
            set
            {
                _InfoShowBack = value;
                NotifyOfPropertyChange(() => InfoShowBack);
            }
        }

        private bool _InfoShowCancel;
        public bool InfoShowCancel
        {
            get { return _InfoShowCancel; }
            set
            {
                _InfoShowCancel = value;
                NotifyOfPropertyChange(() => InfoShowCancel);
            }
        }
    }
}
