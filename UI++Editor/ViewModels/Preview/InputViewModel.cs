using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.EventAggregators;
using UI__Editor.Interfaces;
using UI__Editor.Models.ActionClasses;

namespace UI__Editor.ViewModels.Preview
{
    public class InputViewModel : PropertyChangedBase, IPreview, IHandle<ChangeUI>
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
        private bool _PreviewBackButtonVisible;
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
        private bool _PreviewCancelButtonVisible;
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

        public InputViewModel(Input i)
        {
            Parent = i;
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private Input _Parent;
        public Input Parent
        {
            get { return _Parent; }
            set
            {
                _Parent = value;
                NotifyOfPropertyChange(() => Parent);
            }
        }

        public List<IPreview> Inputs
        {
            get
            {
                List<IPreview> returnList = new List<IPreview>();
                if (null != Parent.SubChildren)
                {
                    foreach (IChildElement c in Parent.SubChildren)
                    {
                        returnList.Add(c.ViewModel.PreviewViewModel);
                        c.ViewModel.PreviewViewModel.EventAggregator = EventAggregator;
                    }
                }
                return returnList;
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

        public void Handle(ChangeUI message)
        {
            switch (message.Type)
            {
                case "PreviewChange":
                    NotifyOfPropertyChange(() => Inputs);
                    break;
            }
        }
    }
}
