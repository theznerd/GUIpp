using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class TextInputViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Text Input"; } }
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

        private bool _ADValidate;
        public bool ADValidate
        {
            get { return _ADValidate; }
            set
            {
                _ADValidate = value;
                NotifyOfPropertyChange(() => ADValidate);
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

        private string _ForceCase;
        public string ForceCase
        {
            get { return _ForceCase; }
            set
            {
                _ForceCase = value;
                NotifyOfPropertyChange(() => ForceCase);
            }
        }

        private string _Hint;
        public string Hint
        {
            get { return _Hint; }
            set
            {
                _Hint = value;
                NotifyOfPropertyChange(() => Hint);
            }
        }

        private bool _HScroll;
        public bool HScroll
        {
            get { return _HScroll; }
            set
            {
                _HScroll = value;
                NotifyOfPropertyChange(() => HScroll);
            }
        }

        private bool _Password;
        public bool Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        private string _Prompt;
        public string Prompt
        {
            get { return _Prompt; }
            set
            {
                _Prompt = value;
                NotifyOfPropertyChange(() => Prompt);
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

        private string _RegEx;
        public string RegEx
        {
            get { return _RegEx; }
            set
            {
                _RegEx = value;
                NotifyOfPropertyChange(() => RegEx);
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
