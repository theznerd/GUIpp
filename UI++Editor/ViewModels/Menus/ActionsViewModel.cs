using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.ViewModels;

namespace UI__Editor.ViewModels.Menus
{
    public class ActionsViewModel : PropertyChangedBase, IHandle<EventAggregators.ChangeUI>
    {
        private enum WindowWidths
        {
            WithSidebar = 532,
            WithoutSidebar = 483
        }

        private enum WindowHeights
        {
            Normal = 327,
            Tall = 500,
            InfoWithLogo = 413
        }

        private ObservableCollection<Actions.IAction> _Actions;
        public ObservableCollection<Actions.IAction> Actions
        {
            get { return _Actions; }
            set
            {
                _Actions = value;
                NotifyOfPropertyChange(() => Actions);
            }
        }

        private IEventAggregator _eventAggregator;
        public ActionsViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            _eventAggregator.Subscribe(this);
        }

        private object _previewBox = new Preview.PreflightViewModel();
        public object PreviewBox
        {
            get { return _previewBox; }
            set
            {
                _previewBox = value;
                NotifyOfPropertyChange(() => PreviewBox);
            }
        }

        private string _actionPreviewTitle;
        public string ActionPreviewTitle
        {
            get { return _actionPreviewTitle; }
            set
            {
                _actionPreviewTitle = value;
                NotifyOfPropertyChange(() => ActionPreviewTitle);
            }
        }

        private bool _actionsFlyOutShown = false;
        public bool ActionsFlyOutShown
        {
            get { return _actionsFlyOutShown; }
            set
            {
                _actionsFlyOutShown = value;
                NotifyOfPropertyChange(() => ActionsFlyOutShown);
            }
        }

        private string _leftBorderColor = "#002147";
        public string LeftBorderColor
        {
            get { return _leftBorderColor; }
            set
            {
                _leftBorderColor = value;
                NotifyOfPropertyChange(() => LeftBorderColor);
            }
        }

        private bool _flatViewEnabled = false;
        public bool FlatViewEnabled
        {
            get { return _flatViewEnabled; }
            set
            {
                _flatViewEnabled = value;
                NotifyOfPropertyChange(() => FlatViewEnabled);
                NotifyOfPropertyChange(() => FlatView);
            }
        }

        public int FlatView
        {
            get { return FlatViewEnabled ? 0 : 20; }
        }

        public int WindowWidth
        {
            get { return ShowSideBar ? (int)WindowWidths.WithSidebar : (int)WindowWidths.WithoutSidebar; }
        }

        public string CollapseSideBar
        {
            get { return ShowSideBar ? "Visible" : "Collapsed"; }
        }

        public int SetGridColumn
        {
            get { return ShowSideBar ? 1 : 0; }
        }

        public int SetGridColumnSpan
        {
            get { return ShowSideBar ? 1 : 2; }
        }

        private bool _showSideBar = true;
        public bool ShowSideBar
        {
            get { return _showSideBar; }
            set
            {
                _showSideBar = value;
                NotifyOfPropertyChange(() => ShowSideBar);
                NotifyOfPropertyChange(() => CollapseSideBar);
                NotifyOfPropertyChange(() => WindowWidth);
                NotifyOfPropertyChange(() => SetGridColumn);
                NotifyOfPropertyChange(() => SetGridColumnSpan);
            }
        }

        public void AddButton()
        {

        }

        public void EditButton()
        {
            var vm = new Actions.DefaultValuesViewModel();
            FlyoutContent = vm;
            FlyoutTitle = vm.ActionTitle;
            ActionsFlyOutShown = true;
        }

        public void DeleteButton()
        {

        }

        private string _flyoutTitle;
        public string FlyoutTitle
        {
            get { return _flyoutTitle; }
            set
            {
                _flyoutTitle = value;
                NotifyOfPropertyChange(() => FlyoutTitle);
            }
        }

        private object _flyoutContent;
        public object FlyoutContent
        {
            get { return _flyoutContent; }
            set
            {
                _flyoutContent = value;
                NotifyOfPropertyChange(() => FlyoutContent);
            }
        }

        public string PreviewRefreshButtonVisible
        {
            get { return (PreviewBox as Preview.IPreview).PreviewRefreshButtonVisible ? "Visible" : "Collapsed"; }
        }

        public string PreviewBackButtonVisible
        {
            get { return (PreviewBox as Preview.IPreview).PreviewBackButtonVisible ? "Visible" : "Collapsed"; }
        }

        public string PreviewCancelButtonVisible
        {
            get { return (PreviewBox as Preview.IPreview).PreviewCancelButtonVisible ? "Visible" : "Collapsed"; }
        }

        public string PreviewAcceptButtonVisible
        {
            get { return (PreviewBox as Preview.IPreview).PreviewAcceptButtonVisible ? "Visible" : "Collapsed"; }
        }

        public void Handle(EventAggregators.ChangeUI change)
        {
            switch(change.Type)
            {
                case "color":
                    LeftBorderColor = (string)change.Data;
                    break;
                case "flatview":
                    FlatViewEnabled = (bool)change.Data;
                    break;
                case "showsidebar":
                    ShowSideBar = (bool)change.Data;
                    break;
                case "title":
                    ActionPreviewTitle = (string)change.Data;
                    break;
                default:
                    break;
            }
        }
    }
}