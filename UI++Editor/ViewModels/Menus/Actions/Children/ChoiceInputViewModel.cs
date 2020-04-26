using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class ChoiceInputViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Choice Input"; } }
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

        private string _AlternateVariable;
        public string AlternateVariable
        {
            get { return _AlternateVariable; }
            set
            {
                _AlternateVariable = value;
                NotifyOfPropertyChange(() => AlternateVariable);
            }
        }

        private bool _AutoComplete;
        public bool AutoComplete
        {
            get { return _AutoComplete; }
            set
            {
                _AutoComplete = value;
                NotifyOfPropertyChange(() => AutoComplete);
            }
        }

        private string _Default;
        public string Default
        {
            get { return _Default; }
            set
            {
                _Default = value;
                NotifyOfPropertyChange(() => Default);
            }
        }

        private string _DropDownSize;
        public string DropDownSize
        {
            get { return _DropDownSize; }
            set
            {
                _DropDownSize = value;
                NotifyOfPropertyChange(() => DropDownSize);
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

        private bool _Required;
        public bool Required
        {
            get { return _Required; }
            set
            {
                _Required = value;
                NotifyOfPropertyChange(() => Required);
            }
        }

        private bool _Sort;
        public bool Sort
        {
            get { return _Sort; }
            set
            {
                _Sort = value;
                NotifyOfPropertyChange(() => Sort);
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

        private ObservableCollection<object> _Choices;
        public ObservableCollection<object> Choices
        {
            get { return _Choices; }
            set
            {
                _Choices = value;
                NotifyOfPropertyChange(() => Choices);
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
