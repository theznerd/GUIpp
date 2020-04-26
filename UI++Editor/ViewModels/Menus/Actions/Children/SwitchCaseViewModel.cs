using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class SwitchCaseViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Switch Case"; } }
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

        private bool _CaseInsensitive;
        public bool CaseInsensitive
        {
            get { return _CaseInsensitive; }
            set
            {
                _CaseInsensitive = value;
                NotifyOfPropertyChange(() => CaseInsensitive);
            }
        }

        private bool _DontEval;
        public bool DontEval
        {
            get { return _DontEval; }
            set
            {
                _DontEval = value;
                NotifyOfPropertyChange(() => DontEval);
            }
        }

        private string _SwitchRegEx;
        public string SwitchRegEx
        {
            get { return _SwitchRegEx; }
            set
            {
                _SwitchRegEx = value;
                NotifyOfPropertyChange(() => SwitchRegEx);
            }
        }

        private ObservableCollection<object> _Variables;
        public ObservableCollection<object> Variables
        {
            get { return _Variables; }
            set
            {
                _Variables = value;
                NotifyOfPropertyChange(() => Variables);
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
