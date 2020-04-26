using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    class UserAuthFieldViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Field"; } }
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

        private string _FieldName;
        public string FieldName
        {
            get { return _FieldName; }
            set
            {
                _FieldName = value;
                NotifyOfPropertyChange(() => FieldName);
            }
        }

        private string _FieldHint;
        public string FieldHint
        {
            get { return _FieldHint; }
            set
            {
                _FieldHint = value;
                NotifyOfPropertyChange(() => FieldHint);
            }
        }

        private string _FieldDomainList;
        public string FieldDomainList
        {
            get { return _FieldDomainList; }
            set
            {
                _FieldDomainList = value;
                NotifyOfPropertyChange(() => FieldDomainList);
            }
        }

        private string _FieldPrompt;
        public string FieldPrompt
        {
            get { return _FieldPrompt; }
            set
            {
                _FieldPrompt = value;
                NotifyOfPropertyChange(() => FieldPrompt);
            }
        }

        private string _FieldQuestion;
        public string FieldQuestion
        {
            get { return _FieldQuestion; }
            set
            {
                _FieldQuestion = value;
                NotifyOfPropertyChange(() => FieldQuestion);
            }
        }

        private bool _ReadOnly;
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                NotifyOfPropertyChange(() => ReadOnly);
            }
        }

        private string _FieldRegEx;
        public string FieldRegEx
        {
            get { return _FieldRegEx; }
            set
            {
                _FieldRegEx = value;
                NotifyOfPropertyChange(() => FieldRegEx);
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
