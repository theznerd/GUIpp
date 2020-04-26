using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class RegReadViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "Reg Read"; } }
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

        private string _RegReadHive;
        public string RegReadHive
        {
            get { return _RegReadHive; }
            set
            {
                _RegReadHive = value;
                NotifyOfPropertyChange(() => RegReadHive);
            }
        }

        private string _RegReadKey;
        public string RegReadKey
        {
            get { return _RegReadKey; }
            set
            {
                _RegReadKey = value;
                NotifyOfPropertyChange(() => RegReadKey);
            }
        }

        private string _RegReadValue;
        public string RegReadValue
        {
            get { return _RegReadValue; }
            set
            {
                _RegReadValue = value;
                NotifyOfPropertyChange(() => RegReadValue);
            }
        }

        private string _RegReadDefaultValue;
        public string RegReadDefaultValue
        {
            get { return _RegReadDefaultValue; }
            set
            {
                _RegReadDefaultValue = value;
                NotifyOfPropertyChange(() => RegReadDefaultValue);
            }
        }

        private string _RegReadVaraiable;
        public string RegReadVaraiable
        {
            get { return _RegReadVaraiable; }
            set
            {
                _RegReadVaraiable = value;
                NotifyOfPropertyChange(() => RegReadVaraiable);
            }
        }

        private bool _RegRead64Bit;
        public bool RegRead64Bit
        {
            get { return _RegRead64Bit; }
            set
            {
                _RegRead64Bit = value;
                NotifyOfPropertyChange(() => RegRead64Bit);
            }
        }
    }
}
