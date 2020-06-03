using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI__Editor.EventAggregators;
using UI__Editor.Interfaces;
using UI__Editor.Models;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels;
using UI__Editor.ViewModels.Preview;

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
            Regular = 327,
            Tall = 500,
            ExtraTall = 672,
            InfoWithLogo = 413
        }

        private ObservableCollection<Interfaces.IElement> _ActionsTreeView;
        public ObservableCollection<Interfaces.IElement> ActionsTreeView
        {
            get { return _ActionsTreeView; }
            set
            {
                _ActionsTreeView = value;
                NotifyOfPropertyChange(() => ActionsTreeView);
            }
        }

        public string SubElementsVisibliltyConverter
        {
            get
            {
                if(null != SelectedActionsTreeView && SelectedActionsTreeView.HasSubChildren)
                {
                    return "Visible";
                }
                else
                {
                    return "Hidden";
                }
            }
        }

        public void ActionsTreeViewChanged(Interfaces.IElement selectedAction)
        {
            SelectedActionsTreeView = selectedAction;
            NotifyOfPropertyChange(() => SelectedActionName);
            NotifyOfPropertyChange(() => SelectedActionCondition);
            NotifyOfPropertyChange(() => SelectedActionHiddenAttributes);
            NotifyOfPropertyChange(() => InfoPaneVisibilityConverter);
            NotifyOfPropertyChange(() => SubElementsVisibliltyConverter);
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

        private Interfaces.IElement _SelectedActionsTreeView;
        public Interfaces.IElement SelectedActionsTreeView
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
                NotifyOfPropertyChange(() => WindowHeight);
                NotifyOfPropertyChange(() => CanMoveAction);
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
            ActionsTreeView = UIpp.Actions.actions;
        }

        public IPreview PreviewBox
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

        public int WindowHeight
        {
            get
            {
                return (int)Enum.Parse(typeof(WindowHeights), PreviewBox.WindowHeight, true);
            }
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

        private BindableCollection<string> AvailableActions
        {
            get
            {
                List<string> actions = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .Where(x => typeof(IAction).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .OrderBy(x => x.Name)
                    .Select(x => x.Name).ToList();
                return new BindableCollection<string>(actions);
            }
        }            

        private BindableCollection<string> _AddActionList = new BindableCollection<string>();
        public BindableCollection<string> AddActionList
        {
            get { return _AddActionList; }
            set
            {
                _AddActionList = value;
                NotifyOfPropertyChange(() => AddActionList);
            }
        }

        private string _SelectedAddActionList;
        public string SelectedAddActionList
        {
            get { return _SelectedAddActionList; }
            set
            {
                _SelectedAddActionList = value;
                NotifyOfPropertyChange(() => SelectedAddActionList);
            }
        }

        private bool _DialogIsVisible = false;
        public bool DialogIsVisible
        {
            get { return _DialogIsVisible; }
            set
            {
                _DialogIsVisible = value;
                _eventAggregator.BeginPublishOnUIThread(new SendMessage("DialogVisible", value));
                NotifyOfPropertyChange(() => DialogIsVisible);
                NotifyOfPropertyChange(() => DialogVisibilityConverter);
            }
        }

        public void AddActionCancel()
        {
            DialogIsVisible = false;
        }

        public void AddActionOk()
        {
            IAction newAction = (IAction)Activator.CreateInstance(Type.GetType("UI__Editor.Models.ActionClasses." + SelectedAddActionList), _eventAggregator);
            if (null == SelectedActionsTreeView)
            {
                // If no selected index, append to the end
                ActionsTreeView.Add(newAction);
            }
            else if (SelectedActionsTreeView.ActionType == "Action Group")
            {
                // If selected index is an action group, dump it in the action group
                (SelectedActionsTreeView as ActionGroup).AddChild(newAction);
            }
            else if(null == SelectedActionsTreeView.Parent)
            {
                // If selected index is not within group, add to UIpp directly
                ActionsTreeView.Insert(ActionsTreeView.IndexOf(SelectedActionsTreeView) + 1, newAction);
            }
            else if(null != SelectedActionsTreeView.Parent)
            {
                // If selected index is within group, find and add to parent
                int childIndex = (SelectedActionsTreeView.Parent as ActionGroup).Children.IndexOf(SelectedActionsTreeView);
                (SelectedActionsTreeView.Parent as ActionGroup).AddChild(newAction, childIndex);
            }
            DialogIsVisible = false;
        }

        public bool CanMoveAction
        {
            get 
            { 
                return (null != SelectedActionsTreeView); 
            }
        }

        public void MoveAction(string position)
        {
            // TODO: Fix movements across groups. Movements are a touch buggy still
            bool isChild = (null != SelectedActionsTreeView.Parent);
            
            int parentIndex;
            int actionIndex;
            int parentCount;
            int actionCount = ActionsTreeView.Count;

            if (isChild)
            {
                parentIndex = ActionsTreeView.IndexOf(SelectedActionsTreeView.Parent);
                actionIndex = (SelectedActionsTreeView.Parent as ActionGroup).Children.IndexOf(SelectedActionsTreeView);
                parentCount = (SelectedActionsTreeView.Parent as ActionGroup).Children.Count;
                switch (position)
                {
                    case "top":
                        ActionsTreeView.Insert(0, SelectedActionsTreeView);
                        (SelectedActionsTreeView.Parent as ActionGroup).RemoveChild(SelectedActionsTreeView);
                        break;
                    case "bottom":
                        ActionsTreeView.Add(SelectedActionsTreeView);
                        (SelectedActionsTreeView.Parent as ActionGroup).RemoveChild(SelectedActionsTreeView);
                        break;
                    case "up":
                        if (actionIndex == 0)
                        {
                            ActionsTreeView.Insert(parentIndex, SelectedActionsTreeView);
                            (SelectedActionsTreeView.Parent as ActionGroup).RemoveChild(SelectedActionsTreeView);
                        }
                        else
                        {
                            (SelectedActionsTreeView.Parent as ActionGroup).Children.Move(actionIndex, actionIndex - 1);
                        }
                        break;
                    case "down":
                        if (actionIndex == parentCount - 1)
                        {
                            ActionsTreeView.Insert(parentIndex + 1, SelectedActionsTreeView);
                            (SelectedActionsTreeView.Parent as ActionGroup).RemoveChild(SelectedActionsTreeView);
                        }
                        else
                        {
                            (SelectedActionsTreeView.Parent as ActionGroup).Children.Move(actionIndex, actionIndex + 1);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                actionIndex = ActionsTreeView.IndexOf(SelectedActionsTreeView);
                switch (position)
                {
                    case "top":
                        ActionsTreeView.Move(actionIndex, 0);
                        break;
                    case "bottom":
                        ActionsTreeView.Move(actionIndex, actionCount - 1);
                        break;
                    case "up":
                        if(actionIndex != 0)
                        {
                            IElement previousAction = ActionsTreeView[actionIndex - 1];
                            if(previousAction.ActionType == "Action Group")
                            {
                                (previousAction as ActionGroup).AddChild(SelectedActionsTreeView);
                                ActionsTreeView.Remove(SelectedActionsTreeView);
                            }
                            else
                            {
                                ActionsTreeView.Move(actionIndex, actionIndex - 1);
                            }
                        }
                        break;
                    case "down":
                        if(actionIndex != actionCount - 1)
                        {
                            IElement nextAction = ActionsTreeView[actionIndex + 1];
                            if(nextAction.ActionType == "Action Group")
                            {
                                (nextAction as ActionGroup).AddChild(SelectedActionsTreeView, 0);
                                ActionsTreeView.Remove(SelectedActionsTreeView);
                            }
                            else
                            {
                                ActionsTreeView.Move(actionIndex, actionIndex + 1);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public string DialogVisibilityConverter
        {
            get { return DialogIsVisible ? "Visible" : "Collapsed"; }
        }

        public void AddButton()
        {
            // Display Modal Dialog
            AddActionList = AvailableActions;
            DialogIsVisible = true;
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
            if(null != SelectedActionsTreeView)
            {
                if(null != SelectedActionsTreeView.Parent)
                {
                    (SelectedActionsTreeView.Parent as ActionGroup).RemoveChild(SelectedActionsTreeView);
                }
                else
                {
                    ActionsTreeView.Remove(SelectedActionsTreeView);
                }
                NotifyOfPropertyChange(() => ActionsTreeView);
            }
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
            get { return PreviewBox.PreviewRefreshButtonVisible ? "Visible" : "Collapsed"; }
        }

        public string PreviewBackButtonVisible
        {
            get { return PreviewBox.PreviewBackButtonVisible ? "Visible" : "Collapsed"; }
        }

        public string PreviewCancelButtonVisible
        {
            get { return PreviewBox.PreviewCancelButtonVisible ? "Visible" : "Collapsed"; }
        }

        public string PreviewAcceptButtonVisible
        {
            get { return PreviewBox.PreviewAcceptButtonVisible ? "Visible" : "Collapsed"; }
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
                case "SizeChange":
                    NotifyOfPropertyChange(() => WindowHeight);
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