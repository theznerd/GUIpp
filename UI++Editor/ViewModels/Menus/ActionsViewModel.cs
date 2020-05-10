using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels;

namespace UI__Editor.ViewModels.Menus
{
    public class ActionsViewModel : PropertyChangedBase, IHandle<EventAggregators.ChangeUI>, IHandle<EventAggregators.SendMessage>
    {
        private UIpp UIpp;

        private enum WindowWidths
        {
            WithSidebar = 532,
            WithoutSidebar = 483
        }

        private enum WindowHeights
        {
            Normal = 327,
            Tall = 500,
            ExtraTall = 672,
            InfoWithLogo = 413
        }

        private ObservableCollection<Interfaces.IAction> _ActionsTreeView;
        public ObservableCollection<Interfaces.IAction> ActionsTreeView
        {
            get { return _ActionsTreeView; }
            set
            {
                _ActionsTreeView = value;
                NotifyOfPropertyChange(() => ActionsTreeView);
            }
        }

        public void ActionsTreeViewChanged(Interfaces.IAction selectedAction)
        {
            SelectedActionsTreeView = selectedAction;
            NotifyOfPropertyChange(() => SelectedActionName);
            NotifyOfPropertyChange(() => SelectedActionCondition);
            NotifyOfPropertyChange(() => SelectedActionHiddenAttributes);
            NotifyOfPropertyChange(() => InfoPaneVisibilityConverter);
        }

        public string SelectedActionName
        {
            get
            {
                if(null != SelectedActionsTreeView)
                {
                    return "Attributes for " + SelectedActionsTreeView.ActionType;
                }
                else
                {
                    return "";
                }
                
            }
        }

        public string InfoPaneVisibilityConverter
        {
            get
            {
                return null != SelectedActionsTreeView ? "Visible" : "Collapsed";
            }
        }

        public string SelectedActionCondition
        {
            get
            {
                if(null != SelectedActionsTreeView && !string.IsNullOrEmpty(SelectedActionsTreeView.ViewModel.Condition))
                {
                    return SelectedActionsTreeView.ViewModel.Condition;
                }
                else
                {
                    return "No Condition Defined...";
                }
                
            }
        }

        public string SelectedActionHiddenAttributes
        {
            get
            {
                if(null != SelectedActionsTreeView)
                {
                    return SelectedActionsTreeView.ViewModel.HiddenAttributes;
                }
                else
                {
                    return "";
                }
                
            }
        }

        private Interfaces.IAction _SelectedActionsTreeView;
        public Interfaces.IAction SelectedActionsTreeView
        {
            get { return _SelectedActionsTreeView; }
            set
            {
                _SelectedActionsTreeView = value;
                NotifyOfPropertyChange(() => SelectedActionsTreeView);
                NotifyOfPropertyChange(() => CanEditButton);
                NotifyOfPropertyChange(() => PreviewBox);
                NotifyOfPropertyChange(() => PreviewRefreshButtonVisible);
                NotifyOfPropertyChange(() => PreviewBackButtonVisible);
                NotifyOfPropertyChange(() => PreviewCancelButtonVisible);
                NotifyOfPropertyChange(() => PreviewAcceptButtonVisible);
            }
        }

        private IEventAggregator _eventAggregator = new EventAggregator();
        private IEventAggregator _actionEventAggregator;
        public ActionsViewModel(IEventAggregator ea, UIpp uipp)
        {
            _eventAggregator = ea;
            _eventAggregator.Subscribe(this);
            UIpp = uipp;
            _actionEventAggregator = new EventAggregator();
            ActionsTreeView = new ObservableCollection<Interfaces.IAction>();
            ActionsTreeView.Add(new Info(_eventAggregator));
            ActionsTreeView.Add(new Info(_eventAggregator));
            ActionsTreeView.Add(new Info(_eventAggregator));
        }

        public object PreviewBox
        {
            get {
                if(null != SelectedActionsTreeView)
                {
                    return SelectedActionsTreeView.ViewModel.PreviewViewModel;
                }
                else
                {
                    return new Preview._NoPreviewViewModel();
                }
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

        public bool CanEditButton
        {
            get
            {
                return null != SelectedActionsTreeView;
            }
        }

        public void EditButton()
        {
            if(null != SelectedActionsTreeView)
            {
                FlyoutContent = SelectedActionsTreeView.ViewModel;
                FlyoutTitle = SelectedActionsTreeView.ViewModel.ActionTitle;
                ActionsFlyOutShown = true;
            }
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

        public void Handle(EventAggregators.SendMessage message)
        {
            switch(message.Type)
            {
                case "ButtonChange":
                    NotifyOfPropertyChange(() => PreviewRefreshButtonVisible);
                    NotifyOfPropertyChange(() => PreviewBackButtonVisible);
                    NotifyOfPropertyChange(() => PreviewCancelButtonVisible);
                    NotifyOfPropertyChange(() => PreviewAcceptButtonVisible);
                    break;
                case "ConditionChange":
                    NotifyOfPropertyChange(() => SelectedActionCondition);
                    break;
                case "AttributeChange":
                    NotifyOfPropertyChange(() => SelectedActionHiddenAttributes);
                    break;
                default:
                    break;
            }
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