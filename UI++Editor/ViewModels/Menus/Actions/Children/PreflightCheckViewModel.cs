using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class PreflightCheckViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Preflight Check"; } }
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

        private string _CheckCondition;
        public string CheckCondition
        {
            get { return _CheckCondition; }
            set
            {
                _CheckCondition = value;
                NotifyOfPropertyChange(() => CheckCondition);
            }
        }

        private string _CheckDescription;
        public string CheckDescription
        {
            get { return _CheckDescription; }
            set
            {
                _CheckDescription = value;
                NotifyOfPropertyChange(() => CheckDescription);
            }
        }

        private string _CheckErrorDescription;
        public string CheckErrorDescription
        {
            get { return _CheckErrorDescription; }
            set
            {
                _CheckErrorDescription = value;
                NotifyOfPropertyChange(() => CheckErrorDescription);
            }
        }

        private string _CheckText;
        public string CheckText
        {
            get { return _CheckText; }
            set
            {
                _CheckText = value;
                NotifyOfPropertyChange(() => CheckText);
            }
        }

        private string _CheckWarnCondition;
        public string CheckWarnCondition
        {
            get { return _CheckWarnCondition; }
            set
            {
                _CheckWarnCondition = value;
                NotifyOfPropertyChange(() => CheckWarnCondition);
            }
        }

        private string _CheckWarnDescription;
        public string CheckWarnDescription
        {
            get { return _CheckWarnDescription; }
            set
            {
                _CheckWarnDescription = value;
                NotifyOfPropertyChange(() => CheckWarnDescription);
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
    }
}
