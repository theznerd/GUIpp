using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class AppTreeViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return true; } }
        public string ActionTitle { get { return "App Tree"; } }
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

        private string _AppTreeTitle;
        public string AppTreeTitle
        {
            get { return _AppTreeTitle; }
            set
            {
                _AppTreeTitle = value;
                NotifyOfPropertyChange(() => AppTreeTitle);
            }
        }

        private string _AppTreeAppVar;
        public string AppTreeAppVar
        {
            get { return _AppTreeAppVar; }
            set
            {
                _AppTreeAppVar = value;
                NotifyOfPropertyChange(() => AppTreeAppVar);
            }
        }

        private string _AppTreePkgVar;
        public string AppTreePkgVar
        {
            get { return _AppTreePkgVar; }
            set
            {
                _AppTreePkgVar = value;
                NotifyOfPropertyChange(() => AppTreePkgVar);
            }
        }

        private string _AppTreeSize;
        public string AppTreeSize
        {
            get { return _AppTreeSize; }
            set
            {
                _AppTreeSize = value;
                NotifyOfPropertyChange(() => AppTreeSize);
            }
        }

        private bool _AppTreeExpanded;
        public bool AppTreeExpanded
        {
            get { return _AppTreeExpanded; }
            set
            {
                _AppTreeExpanded = value;
                NotifyOfPropertyChange(() => AppTreeExpanded);
            }
        }

        private bool _AppTreeShowBack;
        public bool AppTreeShowBack
        {
            get { return _AppTreeShowBack; }
            set
            {
                _AppTreeShowBack = value;
                NotifyOfPropertyChange(() => AppTreeShowBack);
            }
        }

        private bool _AppTreeShowCancel;
        public bool AppTreeShowCancel
        {
            get { return _AppTreeShowCancel; }
            set
            {
                _AppTreeShowCancel = value;
                NotifyOfPropertyChange(() => AppTreeShowCancel);
            }
        }
    }
}
