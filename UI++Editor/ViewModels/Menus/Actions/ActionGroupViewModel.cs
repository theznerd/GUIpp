using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class ActionGroupViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return true; } }
        public string ActionTitle { get { return "Action Group"; } }
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

        private string _ActionGroupName;
        public string ActionGroupName
        {
            get { return _ActionGroupName; }
            set
            {
                _ActionGroupName = value;
                NotifyOfPropertyChange(() => ActionGroupName);
            }
        }

        private string _ActionGroupCondition;
        public string ActionGroupCondition
        {
            get { return _ActionGroupCondition; }
            set
            {
                _ActionGroupCondition = value;
                NotifyOfPropertyChange(() => ActionGroupCondition);
            }
        }
    }
}
