using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class RegWriteViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "Reg Write"; } }
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

        private string _RegWriteHive;
        public string RegWriteHive
        {
            get { return _RegWriteHive; }
            set
            {
                _RegWriteHive = value;
                NotifyOfPropertyChange(() => RegWriteHive);
            }
        }

        private string _RegWriteKey;
        public string RegWriteKey
        {
            get { return _RegWriteKey; }
            set
            {
                _RegWriteKey = value;
                NotifyOfPropertyChange(() => RegWriteKey);
            }
        }

        private string _RegWriteValue;
        public string RegWriteValue
        {
            get { return _RegWriteValue; }
            set
            {
                _RegWriteValue = value;
                NotifyOfPropertyChange(() => RegWriteValue);
            }
        }

        private string _RegWriteType;
        public string RegWriteType
        {
            get { return _RegWriteType; }
            set
            {
                _RegWriteType = value;
                NotifyOfPropertyChange(() => RegWriteType);
            }
        }

        private bool _RegWrite64Bit;
        public bool RegWrite64Bit
        {
            get { return _RegWrite64Bit; }
            set
            {
                _RegWrite64Bit = value;
                NotifyOfPropertyChange(() => RegWrite64Bit);
            }
        }
    }
}
