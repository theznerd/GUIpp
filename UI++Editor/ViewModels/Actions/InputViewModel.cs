using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.EventAggregators;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;
using UI__Editor.Views.Actions;

namespace UI__Editor.ViewModels.Actions
{
    class InputViewModel : PropertyChangedBase, IAction, IHandle<EventAggregators.ChangeUI>
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Input"; } }
        public IEventAggregator EventAggregator;

        public string HiddenAttributes
        {
            get
            {
                string ha;
                ha = "Name: " + Name;
                return ha;
            }
        }

        public InputViewModel(Input input)
        {
            ModelClass = input;
            PreviewViewModel = new Preview.InputViewModel(ModelClass as Input);
            EventAggregator = input.EventAggregator;
            PreviewViewModel.EventAggregator = input.EventAggregator;
            EventAggregator.Subscribe(this);
            SelectedSize = string.IsNullOrEmpty(input.Size) ? "Regular" : input.Size;
            (PreviewViewModel as Preview.InputViewModel).PreviewBackButtonVisible = ShowBack == true ? true : false;
            (PreviewViewModel as Preview.InputViewModel).PreviewCancelButtonVisible = ShowCancel == true ? true : false;
            (PreviewViewModel as Preview.InputViewModel).WindowHeight = SelectedSize;
            (PreviewViewModel as Preview.InputViewModel).Title = Title;
            (PreviewViewModel as Preview.InputViewModel).CenterTitle = CenterTitle;
        }

        public string Title
        {
            get { return (ModelClass as Input).Title; }
            set
            {
                (ModelClass as Input).Title = value;
                (PreviewViewModel as Preview.InputViewModel).Title = value;
            }
        }

        public string Name
        {
            get { return (ModelClass as Input).Name; }
            set
            {
                (ModelClass as Input).Name = value;
                NotifyOfPropertyChange(() => HiddenAttributes);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
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
                (ModelClass as Input).Size = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("SizeChange", value));
                NotifyOfPropertyChange(() => SelectedSize);
            }
        }

        public bool? ShowBack
        {
            get { return (ModelClass as Input).ShowBack; }
            set
            {
                (ModelClass as Input).ShowBack = value;
                (PreviewViewModel as Preview.InputViewModel).PreviewBackButtonVisible = value == true ? true : false;
            }
        }

        public bool? ShowCancel
        {
            get { return (ModelClass as Input).ShowCancel; }
            set
            {
                (ModelClass as Input).ShowCancel = value;
                (PreviewViewModel as Preview.InputViewModel).PreviewCancelButtonVisible = value == true ? true : false;
            }
        }

        public bool CenterTitle
        {
            get { return (ModelClass as Input).CenterTitle; }
            set
            {
                (ModelClass as Input).CenterTitle = value;
                (PreviewViewModel as Preview.InputViewModel).CenterTitle = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as Input).Condition; }
            set
            {
                (ModelClass as Input).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }

        public void Handle(ChangeUI message)
        {
            switch (message.Type)
            {
                case "ImportComplete":
                    (PreviewViewModel as Preview.InputViewModel).PreviewBackButtonVisible = ShowBack == true ? true : false;
                    (PreviewViewModel as Preview.InputViewModel).PreviewCancelButtonVisible = ShowCancel == true ? true : false;
                    (PreviewViewModel as Preview.InputViewModel).WindowHeight = SelectedSize;
                    (PreviewViewModel as Preview.InputViewModel).Title = Title;
                    (PreviewViewModel as Preview.InputViewModel).CenterTitle = CenterTitle;
                    break;
            }
        }
    }
}
