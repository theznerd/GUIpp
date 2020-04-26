using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class ExternalCallViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "External Call"; } }
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

        private string _ExternalCallTitle;
        public string ExternalCallTitle
        {
            get { return _ExternalCallTitle; }
            set
            {
                _ExternalCallTitle = value;
                NotifyOfPropertyChange(() => ExternalCallTitle);
            }
        }

        private string _ExternalCallData;
        public string ExternalCallData
        {
            get { return _ExternalCallData; }
            set
            {
                _ExternalCallData = value;
                NotifyOfPropertyChange(() => ExternalCallData);
            }
        }

        private string _ExternalCallMaxRuntime;
        public string ExternalCallMaxRuntime
        {
            get { return _ExternalCallMaxRuntime; }
            set
            {
                _ExternalCallMaxRuntime = value;
                NotifyOfPropertyChange(() => ExternalCallMaxRuntime);
            }
        }
    }
}
