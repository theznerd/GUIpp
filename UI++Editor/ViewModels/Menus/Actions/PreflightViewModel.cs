using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class PreflightViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return true; } }
        public string ActionTitle { get { return "Preflight"; } }
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

        private string _PreflightTitle;
        public string PreflightTitle
        {
            get { return _PreflightTitle; }
            set
            {
                _PreflightTitle = value;
                NotifyOfPropertyChange(() => PreflightTitle);
            }
        }

        private string _PreflightSize;
        public string PreflightSize
        {
            get { return _PreflightSize; }
            set
            {
                _PreflightSize = value;
                NotifyOfPropertyChange(() => PreflightSize);
            }
        }

        private string _PreflightTimeout;
        public string PreflightTimeout
        {
            get { return _PreflightTimeout; }
            set
            {
                _PreflightTimeout = value;
                NotifyOfPropertyChange(() => PreflightTimeout);
            }
        }

        private string _PreflightTimeoutAction;
        public string PreflightTimeoutAction
        {
            get { return _PreflightTimeoutAction; }
            set
            {
                _PreflightTimeoutAction = value;
                NotifyOfPropertyChange(() => PreflightTimeoutAction);
            }
        }

        private string _PreflightCustomTimeoutAction;
        public string PreflightCustomTimeoutAction
        {
            get { return _PreflightCustomTimeoutAction; }
            set
            {
                _PreflightCustomTimeoutAction = value;
                NotifyOfPropertyChange(() => PreflightCustomTimeoutAction);
            }
        }

        private bool _PreflightShowOnFailureOnly;
        public bool PreflightShowOnFailureOnly
        {
            get { return _PreflightShowOnFailureOnly; }
            set
            {
                _PreflightShowOnFailureOnly = value;
                NotifyOfPropertyChange(() => PreflightShowOnFailureOnly);
            }
        }

        private bool _PreflightShowBack;
        public bool PreflightShowBack
        {
            get { return _PreflightShowBack; }
            set
            {
                _PreflightShowBack = value;
                NotifyOfPropertyChange(() => PreflightShowBack);
            }
        }

        private bool _PreflightShowCancel;
        public bool PreflightShowCancel
        {
            get { return _PreflightShowCancel; }
            set
            {
                _PreflightShowCancel = value;
                NotifyOfPropertyChange(() => PreflightShowCancel);
            }
        }
    }
}
