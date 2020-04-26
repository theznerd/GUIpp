using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class CheckBoxInputViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Checkbox Input"; } }
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

        private string _CheckedValue;
        public string CheckedValue
        {
            get { return _CheckedValue; }
            set
            {
                _CheckedValue = value;
                NotifyOfPropertyChange(() => CheckedValue);
            }
        }

        private string _UncheckedValue;
        public string UncheckedValue
        {
            get { return _UncheckedValue; }
            set
            {
                _UncheckedValue = value;
                NotifyOfPropertyChange(() => UncheckedValue);
            }
        }

        private string _DefaultValue;
        public string DefaultValue
        {
            get { return _DefaultValue; }
            set
            {
                _DefaultValue = value;
                NotifyOfPropertyChange(() => DefaultValue);
            }
        }

        private string _Question;
        public string Question
        {
            get { return _Question; }
            set
            {
                _Question = value;
                NotifyOfPropertyChange(() => Question);
            }
        }

        private string _Variable;
        public string Variable
        {
            get { return _Variable; }
            set
            {
                _Variable = value;
                NotifyOfPropertyChange(() => Variable);
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
