using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class TSVarViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "TS Variable"; } }
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

        private string _TSVarVariable;
        public string TSVarVariable
        {
            get { return _TSVarVariable; }
            set
            {
                _TSVarVariable = value;
                NotifyOfPropertyChange(() => TSVarVariable);
            }
        }

        private string _TSVarValue;
        public string TSVarValue
        {
            get { return _TSVarValue; }
            set
            {
                _TSVarValue = value;
                NotifyOfPropertyChange(() => TSVarValue);
            }
        }

        private bool _SwitchDontEval;
        public bool SwitchDontEval
        {
            get { return _SwitchDontEval; }
            set
            {
                _SwitchDontEval = value;
                NotifyOfPropertyChange(() => SwitchDontEval);
            }
        }
    }
}
