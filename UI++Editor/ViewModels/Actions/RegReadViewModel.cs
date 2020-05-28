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
    class RegReadViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Registry Read"; } }
        public IEventAggregator EventAggregator;

        public string HiddenAttributes
        {
            get
            {
                string ha;
                ha = "Hive: " + SelectedHive;
                ha += "\r\nDefault: " + Default;
                ha += "\r\nKey: " + Key;
                ha += "\r\n64-Bit Registry: " + Reg64;
                ha += "\r\nVariable: " + Variable;
                ha += "\r\nValue: " + Value;
                return ha;
            }
        }

        public RegReadViewModel(RegRead rr)
        {
            ModelClass = rr;
            EventAggregator = rr.EventAggregator;
            PreviewViewModel.EventAggregator = rr.EventAggregator;
            SelectedHive = rr.Hive;
        }

        public string Condition
        {
            get { return (ModelClass as RegRead).Condition; }
            set
            {
                (ModelClass as RegRead).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
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
                (ModelClass as RegRead).Hive = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", value));
                NotifyOfPropertyChange(() => SelectedHive);
            }
        }

        public string Default 
        {
            get { return (ModelClass as RegRead).Default; }
            set
            {
                (ModelClass as RegRead).Default = value;
                NotifyOfPropertyChange(() => Default);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Key
        {
            get { return (ModelClass as RegRead).Key; }
            set
            {
                (ModelClass as RegRead).Key = value;
                NotifyOfPropertyChange(() => Key);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public bool? Reg64
        {
            get { return (ModelClass as RegRead).Reg64; }
            set
            {
                (ModelClass as RegRead).Reg64 = value;
                NotifyOfPropertyChange(() => Reg64);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Variable
        {
            get { return (ModelClass as RegRead).Variable; }
            set
            {
                (ModelClass as RegRead).Variable = value;
                NotifyOfPropertyChange(() => Variable);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Value
        {
            get { return (ModelClass as RegRead).Value; }
            set
            {
                (ModelClass as RegRead).Value = value;
                NotifyOfPropertyChange(() => Value);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }
    }
}
