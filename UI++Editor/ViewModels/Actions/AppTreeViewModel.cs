using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.ViewModels.Preview;
using UI__Editor.Models.ActionClasses;
using UI__Editor.EventAggregators;

namespace UI__Editor.ViewModels.Actions
{
    public class AppTreeViewModel : PropertyChangedBase, IAction, IHandle<EventAggregators.ChangeUI>
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "AppTree"; } }
        public IEventAggregator EventAggregator;

        public string HiddenAttributes
        {
            get
            {
                return "";
            }
        }

        public AppTreeViewModel(AppTree appTree)
        {
            ModelClass = appTree;
            EventAggregator = Globals.EventAggregator;
            EventAggregator.Subscribe(this);
            PreviewViewModel = new Preview.AppTreeViewModel(appTree, appTree.EventAggregator);
            SelectedSize = string.IsNullOrEmpty(appTree.Size) ? "Regular" : appTree.Size;
            (PreviewViewModel as Preview.AppTreeViewModel).PreviewBackButtonVisible = ShowBack;
            (PreviewViewModel as Preview.AppTreeViewModel).PreviewCancelButtonVisible = ShowCancel;
            (PreviewViewModel as Preview.AppTreeViewModel).Title = Title;
            (PreviewViewModel as Preview.AppTreeViewModel).CenterTitle = CenterTitle;
            if(null != SelectedSize)
            {
                (PreviewViewModel as Preview.AppTreeViewModel).WindowHeight = SelectedSize;
            }
        }

        public string ApplicationVariableBase
        {
            get { return (ModelClass as AppTree).ApplicationVariableBase; }
            set
            {
                (ModelClass as AppTree).ApplicationVariableBase = value;
            }
        }
        public string PackageVariableBase
        {
            get { return (ModelClass as AppTree).PackageVariableBase; }
            set { (ModelClass as AppTree).PackageVariableBase = value; }
        }
        public bool ShowBack
        {
            get { return (ModelClass as AppTree).ShowBack; }
            set 
            { 
                (ModelClass as AppTree).ShowBack = value;
                (PreviewViewModel as Preview.AppTreeViewModel).PreviewBackButtonVisible = value;
            }
        }
        public bool ShowCancel
        {
            get { return (ModelClass as AppTree).ShowCancel; }
            set 
            { 
                (ModelClass as AppTree).ShowCancel = value;
                (PreviewViewModel as Preview.AppTreeViewModel).PreviewCancelButtonVisible = value;
            }
        }
        public string Title
        {
            get { return (ModelClass as AppTree).Title; }
            set { 
                (ModelClass as AppTree).Title = value;
                (PreviewViewModel as Preview.AppTreeViewModel).Title = value;
            }
        }

        public string WindowHeight
        {
            get { return (ModelClass as AppTree).Size; }
            set
            {
                (ModelClass as AppTree).Size = value;
                (PreviewViewModel as Preview.AppTreeViewModel).WindowHeight = value;
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
                (ModelClass as AppTree).Size = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("SizeChange", value));
                NotifyOfPropertyChange(() => SelectedSize);
            }
        }

        private List<string> _Size = new List<string>()
        {
            "Regular",
            "Tall",
            "ExtraTall"
        };
        public List<string> Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                NotifyOfPropertyChange(() => Size);
            }
        }
        public bool Expanded
        {
            get { return (ModelClass as AppTree).Expanded; }
            set { (ModelClass as AppTree).Expanded = value; }
        }

        public bool CenterTitle
        {
            get { return (ModelClass as AppTree).CenterTitle; }
            set
            {
                (ModelClass as AppTree).CenterTitle = value;
                (PreviewViewModel as Preview.AppTreeViewModel).CenterTitle = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as AppTree).Condition; }
            set { (ModelClass as AppTree).Condition = value; }
        }

        public void Handle(ChangeUI message)
        {
            switch (message.Type)
            {
                case "ImportComplete":
                    (PreviewViewModel as Preview.AppTreeViewModel).PreviewBackButtonVisible = ShowBack;
                    (PreviewViewModel as Preview.AppTreeViewModel).PreviewCancelButtonVisible = ShowCancel;
                    (PreviewViewModel as Preview.AppTreeViewModel).Title = Title;
                    (PreviewViewModel as Preview.AppTreeViewModel).CenterTitle = CenterTitle;
                    if (null != SelectedSize)
                    {
                        (PreviewViewModel as Preview.AppTreeViewModel).WindowHeight = SelectedSize;
                    }
                    break;
            }
        }
    }
}
