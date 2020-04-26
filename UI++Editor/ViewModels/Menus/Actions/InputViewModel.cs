using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class InputViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return true; } }
        public string ActionTitle { get { return "Input"; } }
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

        private string _InputTitle;
        public string InputTitle
        {
            get { return _InputTitle; }
            set
            {
                _InputTitle = value;
                NotifyOfPropertyChange(() => InputTitle);
            }
        }

        private string _InputName;
        public string InputName
        {
            get { return _InputName; }
            set
            {
                _InputName = value;
                NotifyOfPropertyChange(() => InputName);
            }
        }

        private string _InputSize;
        public string InputSize
        {
            get { return _InputSize; }
            set
            {
                _InputSize = value;
                NotifyOfPropertyChange(() => InputSize);
            }
        }

        private bool _InputADValidate;
        public bool InputADValidate
        {
            get { return _InputADValidate; }
            set
            {
                _InputADValidate = value;
                NotifyOfPropertyChange(() => InputADValidate);
            }
        }

        private bool _InputShowBack;
        public bool InputShowBack
        {
            get { return _InputShowBack; }
            set
            {
                _InputShowBack = value;
                NotifyOfPropertyChange(() => InputShowBack);
            }
        }

        private bool _InputShowCancel;
        public bool InputShowCancel
        {
            get { return _InputShowCancel; }
            set
            {
                _InputShowCancel = value;
                NotifyOfPropertyChange(() => InputShowCancel);
            }
        }
    }
}
