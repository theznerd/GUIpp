using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using UI__Editor.EventAggregators;
using UI__Editor.Interfaces;
using UI__Editor.Models;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Actions.Children;
using UI__Editor.ViewModels.Preview.Children;
using UI__Editor.Views.Actions;

namespace UI__Editor.ViewModels.Preview
{
    class PreflightViewModel : PropertyChangedBase, IPreview, IHandle<ChangeUI>
    {
        private IEventAggregator _EventAggregator;
        public IEventAggregator EventAggregator
        {
            get { return _EventAggregator; }
            set
            {
                _EventAggregator = value;
                EventAggregator.Subscribe(this);
            }
        }
        public string WindowHeight { get; set; } = "Regular";
        public string Font { get { return Globals.DisplayFont; } }
        public bool HasCustomPreview { get; } = false;
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return true; } }
        private bool _PreviewBackButtonVisible = false;
        public bool PreviewBackButtonVisible
        {
            get { return _PreviewBackButtonVisible; }
            set
            {
                _PreviewBackButtonVisible = value;
                NotifyOfPropertyChange(() => PreviewBackButtonVisible);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ButtonChange", ""));
            }
        }
        private bool _PreviewCancelButtonVisible = false;
        public bool PreviewCancelButtonVisible
        {
            get { return _PreviewCancelButtonVisible; }
            set
            {
                _PreviewCancelButtonVisible = value;
                NotifyOfPropertyChange(() => PreviewCancelButtonVisible);
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ButtonChange", ""));
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private bool _CenterTitle;
        public bool CenterTitle
        {
            get { return _CenterTitle; }
            set
            {
                _CenterTitle = value;
                NotifyOfPropertyChange(() => CenterTitle);
                NotifyOfPropertyChange(() => CenterTitleConverter);
            }
        }

        public string CenterTitleConverter
        {
            get { return CenterTitle ? "Center" : "Left"; }
        }

        private Preflight _parent;
        public Preflight parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                NotifyOfPropertyChange(() => parent);
                NotifyOfPropertyChange(() => Checks);
            }
        }

        public List<IPreview> Checks
        {
            get
            {
                List<IPreview> returnList = new List<IPreview>();
                if(null != parent.SubChildren)
                {
                    foreach (IChildElement c in parent.SubChildren)
                    {
                        returnList.Add((c as Check).ViewModel.PreviewViewModel);
                        (c as Check).ViewModel.PreviewViewModel.EventAggregator = EventAggregator;
                    }
                }
                return returnList;
            }
        }

        public string ResultColor
        {
            get
            {
                string returnText = "Black";
                foreach (IPreview c in Checks)
                {
                    if ((c as Children.CheckViewModel).PreviewAs == "Fail")
                    {
                        returnText = "Red";
                        break;
                    }
                    else if ((c as Children.CheckViewModel).PreviewAs == "Warn")
                    {
                        returnText = "#e9b51a";
                    }
                }
                return returnText;
            }
        }

        public string ResultText
        {
            get
            {
                string returnText = Globals.PREFLIGHTPASSED;
                foreach(IPreview c in Checks)
                {
                    if((c as Children.CheckViewModel).PreviewAs == "Fail")
                    {
                        returnText = Globals.PREFLIGHTFAILED;
                        break;
                    }
                    else if ((c as Children.CheckViewModel).PreviewAs == "Warn")
                    {
                        returnText = Globals.PREFLIGHTPASSEDWITHWARNING;
                    }
                }
                return returnText;
            }
        }

        public void Handle(ChangeUI message)
        {
            switch (message.Type)
            {
                case "PreviewChange":
                    NotifyOfPropertyChange(() => Checks);
                    NotifyOfPropertyChange(() => ResultText);
                    NotifyOfPropertyChange(() => ResultColor);
                    break;
            }
        }
    }
}
