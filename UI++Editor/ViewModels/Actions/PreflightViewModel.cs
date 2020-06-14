using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;
using UI__Editor.Views.Actions;

namespace UI__Editor.ViewModels.Actions
{
    class PreflightViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview.PreflightViewModel();
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Preflight"; } }
        public IEventAggregator EventAggregator;

        public string HiddenAttributes
        {
            get
            {
                string ha;
                if(Timeout == 0)
                {
                    ha = "Timeout: No Timeout";
                }
                else
                {
                    ha = "Timeout: " + Timeout + " seconds";
                }
                
                ha += "\r\nTimeout Action: " + SelectedTimeoutAction;
                if(SelectedTimeoutAction == "Custom")
                {
                    ha += "\r\nTimeout Return Code: " + CustomTimeoutAction;
                }
                ha += "\r\nShow on Failure Only: " + ShowOnFailureOnly;
                return ha;
            }
        }

        public PreflightViewModel(Preflight pf)
        {
            ModelClass = pf;
            EventAggregator = pf.EventAggregator;
            PreviewViewModel.EventAggregator = pf.EventAggregator;
            SelectedSize = string.IsNullOrEmpty(pf.Size) ? "Regular" : pf.Size;
            (PreviewViewModel as Preview.PreflightViewModel).PreviewBackButtonVisible = ShowBack == true ? true : false;
            (PreviewViewModel as Preview.PreflightViewModel).PreviewCancelButtonVisible = ShowCancel == true ? true : false;
        }

        public string Title
        {
            get { return (ModelClass as Preflight).Title; }
            set
            {
                (ModelClass as Preflight).Title = value;
                (PreviewViewModel as Preview.PreflightViewModel).Title = value;
            }
        }

        private BindableCollection<string> _Size = new BindableCollection<string>()
        {
            "Regular",
            "Tall"
        };
        public BindableCollection<string> Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                NotifyOfPropertyChange(() => Size);
            }
        }

        private string _SelectedSize;
        public string SelectedSize
        {
            get { return _SelectedSize; }
            set
            {
                _SelectedSize = value;
                PreviewViewModel.WindowHeight = value;
                (ModelClass as Preflight).Size = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("SizeChange", value));
                NotifyOfPropertyChange(() => SelectedSize);
            }
        }

        public int Timeout
        {
            get { return (ModelClass as Preflight).Timeout; }
            set
            {
                (ModelClass as Preflight).Timeout = value;
                NotifyOfPropertyChange(() => Timeout);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        private BindableCollection<string> _TimeoutAction = new BindableCollection<string>()
        {
            "Continue",
            "ContinueOnWarning",
            "Custom"
        };
        public BindableCollection<string> TimeoutAction
        {
            get { return _TimeoutAction; }
            set
            {
                _TimeoutAction = value;
                NotifyOfPropertyChange(() => TimeoutAction);
            }
        }

        private string _SelectedTimeoutAction;
        public string SelectedTimeoutAction
        {
            get { return _SelectedTimeoutAction; }
            set
            {
                _SelectedTimeoutAction = value;
                (ModelClass as Preflight).TimeoutAction = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => SelectedTimeoutAction);
                NotifyOfPropertyChange(() => CustomTimeoutActionGroupVisibility);
            }
        }

        public string CustomTimeoutActionGroupVisibility
        {
            get { return SelectedTimeoutAction == "Custom" ? "Visible" : "Collapsed"; }
        }

        public int CustomTimeoutAction
        {
            get { return (ModelClass as Preflight).TimeoutReturnCode; }
            set
            {
                (ModelClass as Preflight).TimeoutReturnCode = value;
                NotifyOfPropertyChange(() => CustomTimeoutAction);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public bool? ShowOnFailureOnly
        {
            get { return (ModelClass as Preflight).ShowOnFailureOnly; }
            set
            {
                (ModelClass as Preflight).ShowOnFailureOnly = value;
                NotifyOfPropertyChange(() => ShowOnFailureOnly);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public bool? ShowBack
        {
            get { return (ModelClass as Preflight).ShowBack; }
            set
            {
                (ModelClass as Preflight).ShowBack = value;
                (PreviewViewModel as Preview.PreflightViewModel).PreviewBackButtonVisible = value == true ? true : false;
            }
        }

        public bool? ShowCancel
        {
            get { return (ModelClass as Preflight).ShowCancel; }
            set
            {
                (ModelClass as Preflight).ShowCancel = value;
                (PreviewViewModel as Preview.PreflightViewModel).PreviewCancelButtonVisible = value == true ? true : false;
            }
        }

        public bool CenterTitle
        {
            get { return (ModelClass as Preflight).CenterTitle; }
            set
            {
                (ModelClass as Preflight).CenterTitle = value;
                (PreviewViewModel as Preview.PreflightViewModel).CenterTitle = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as Preflight).Condition; }
            set
            {
                (ModelClass as Preflight).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
