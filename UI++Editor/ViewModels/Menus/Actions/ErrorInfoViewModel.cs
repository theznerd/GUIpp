using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class ErrorInfoViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren{ get { return false; } }
        public string ActionTitle { get { return "Error Action"; } }
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


        private string _ErrorInfoName;
        public string ErrorInfoName
        {
            get { return _ErrorInfoName; }
            set
            {
                _ErrorInfoName = value;
                NotifyOfPropertyChange(() => ErrorInfoName);
            }
        }

        private string _ErrorInfoTitle;
        public string ErrorInfoTitle
        {
            get { return _ErrorInfoTitle; }
            set
            {
                _ErrorInfoTitle = value;
                NotifyOfPropertyChange(() => ErrorInfoTitle);
            }
        }

        private string _ErrorInfoImage;
        public string ErrorInfoImage
        {
            get { return _ErrorInfoImage; }
            set
            {
                _ErrorInfoImage = value;
                NotifyOfPropertyChange(() => ErrorInfoImage);
            }
        }

        private string _ErrorInfoInfoImage;
        public string ErrorInfoInfoImage
        {
            get { return _ErrorInfoInfoImage; }
            set
            {
                _ErrorInfoInfoImage = value;
                NotifyOfPropertyChange(() => ErrorInfoInfoImage);
            }
        }

        private string _ErrorInfoContent;
        public string ErrorInfoContent
        {
            get { return _ErrorInfoContent; }
            set
            {
                _ErrorInfoContent = value;
                NotifyOfPropertyChange(() => ErrorInfoContent);
            }
        }

        private bool _ErrorInfoShowBack;
        public bool ErrorInfoShowBack
        {
            get { return _ErrorInfoShowBack; }
            set
            {
                _ErrorInfoShowBack = value;
                NotifyOfPropertyChange(() => ErrorInfoShowBack);
            }
        }
    }
}
