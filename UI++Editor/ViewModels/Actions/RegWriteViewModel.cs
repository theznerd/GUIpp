using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    class RegWriteViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Registry Write"; } }
        public IEventAggregator EventAggregator;

        public string HiddenAttributes
        {
            get
            {
                string ha;
                ha = "Hive: " + SelectedHive;
                ha += "\r\nKey: " + Key;
                ha += "\r\n64-Bit Registry: " + Reg64;
                ha += "\r\nContent: " + Content;
                ha += "\r\nValue: " + Value;
                ha += "\r\nValue Type: " + SelectedRegType;
                return ha;
            }
        }

        public RegWriteViewModel(RegWrite rr)
        {
            ModelClass = rr;
            EventAggregator = rr.EventAggregator;
            PreviewViewModel.EventAggregator = rr.EventAggregator;
            SelectedHive = rr.Hive;
            SelectedRegType = rr.ValueType;
        }

        public string Condition
        {
            get { return (ModelClass as RegWrite).Condition; }
            set
            {
                (ModelClass as RegWrite).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }

        private BindableCollection<string> _RegType = new BindableCollection<string>()
        {
            "REG_SZ",
            "REG_MULTI_SZ",
            "REG_EXPAND_SZ",
            "REG_DWORD",
            "REG_QWORD",
            "REG_BINARY",
            "REG_NONE"
        };
        public BindableCollection<string> RegType
        {
            get { return _RegType; }
            set
            {
                _RegType = value;
                NotifyOfPropertyChange(() => RegType);
            }
        }
        private string _SelectedRegType;
        public string SelectedRegType
        {
            get { return _SelectedRegType; }
            set
            {
                _SelectedRegType = value;
                (ModelClass as RegWrite).ValueType = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", value));
                NotifyOfPropertyChange(() => SelectedRegType);
            }
        }

        private BindableCollection<string> _Hive = new BindableCollection<string>()
        {
            "HKLM",
            "HKCU"
        };
        public BindableCollection<string> Hive
        {
            get { return _Hive; }
            set
            {
                _Hive = value;
                NotifyOfPropertyChange(() => Hive);
            }
        }

        private string _SelectedHive;
        public string SelectedHive
        {
            get { return _SelectedHive; }
            set
            {
                _SelectedHive = value;
                (ModelClass as RegWrite).Hive = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", value));
                NotifyOfPropertyChange(() => SelectedHive);
            }
        }

        public string Key
        {
            get { return (ModelClass as RegWrite).Key; }
            set
            {
                (ModelClass as RegWrite).Key = value;
                NotifyOfPropertyChange(() => Key);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public bool? Reg64
        {
            get { return (ModelClass as RegWrite).Reg64; }
            set
            {
                (ModelClass as RegWrite).Reg64 = value;
                NotifyOfPropertyChange(() => Reg64);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Content
        {
            get { return (ModelClass as RegWrite).Content; }
            set
            {
                (ModelClass as RegWrite).Content = value;
                NotifyOfPropertyChange(() => Content);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Value
        {
            get { return (ModelClass as RegWrite).Value; }
            set
            {
                (ModelClass as RegWrite).Value = value;
                NotifyOfPropertyChange(() => Value);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
    }
}
