using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class UserAuthViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return true; } }
        public string ActionTitle { get { return "User Auth"; } }
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

        private string _UserAuthTitle;
        public string UserAuthTitle
        {
            get { return _UserAuthTitle; }
            set
            {
                _UserAuthTitle = value;
                NotifyOfPropertyChange(() => UserAuthTitle);
            }
        }

        private string _UserAuthDomain;
        public string UserAuthDomain
        {
            get { return _UserAuthDomain; }
            set
            {
                _UserAuthDomain = value;
                NotifyOfPropertyChange(() => UserAuthDomain);
            }
        }

        private string _UserAuthGroups;
        public string UserAuthGroups
        {
            get { return _UserAuthGroups; }
            set
            {
                _UserAuthGroups = value;
                NotifyOfPropertyChange(() => UserAuthGroups);
            }
        }

        private string _UserAuthMaxRetry;
        public string UserAuthMaxRetry
        {
            get { return _UserAuthMaxRetry; }
            set
            {
                _UserAuthMaxRetry = value;
                NotifyOfPropertyChange(() => UserAuthMaxRetry);
            }
        }

        private string _UserAuthAttributes;
        public string UserAuthAttributes
        {
            get { return _UserAuthAttributes; }
            set
            {
                _UserAuthAttributes = value;
                NotifyOfPropertyChange(() => UserAuthAttributes);
            }
        }

        private bool _UserAuthShowBack;
        public bool UserAuthShowBack
        {
            get { return _UserAuthShowBack; }
            set
            {
                _UserAuthShowBack = value;
                NotifyOfPropertyChange(() => UserAuthShowBack);
            }
        }

        private bool _UserAuthDisableCancel;
        public bool UserAuthDisableCancel
        {
            get { return _UserAuthDisableCancel; }
            set
            {
                _UserAuthDisableCancel = value;
                NotifyOfPropertyChange(() => UserAuthDisableCancel);
            }
        }
    }
}
