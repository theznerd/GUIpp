using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
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

        private ObservableCollection<Interfaces.IChildElement> _SubActionsTreeView;
        public ObservableCollection<Interfaces.IChildElement> SubActionsTreeView
        {
            get { return _SubActionsTreeView; }
            set
            {
                _SubActionsTreeView = value;
                NotifyOfPropertyChange(() => SubActionsTreeView);
                NotifyOfPropertyChange(() => SelectedSubActionsTreeView);
            }
        }

        private IChildElement _SelectedSubActionsTreeView;
        public IChildElement SelectedSubActionsTreeView
        {
            get { return _SelectedSubActionsTreeView; }
            set
            {
                _SelectedSubActionsTreeView = value;
                NotifyOfPropertyChange(() => SelectedSubActionsTreeView);
                NotifyOfPropertyChange(() => AddSubActionList);
            }
        }

        public void SubActionsTreeViewChanged(IChildElement selectedSubAction)
        {
            SelectedSubActionsTreeView = selectedSubAction;
            NotifyOfPropertyChange(() => CanSubEditButton);
            NotifyOfPropertyChange(() => CanSubDeleteButton);
            NotifyOfPropertyChange(() => CanMoveSub);
        }

        public bool CanSubEditButton
        {
            get
            {
                return null != SelectedSubActionsTreeView;
            }
        }

        public void SubEditButton()
        {
            if (null != SelectedSubActionsTreeView)
            {
                FlyoutContent = SelectedSubActionsTreeView.ViewModel;
                FlyoutTitle = SelectedSubActionsTreeView.ViewModel.ActionTitle;
                ActionsFlyOutShown = true;
            }
        }

        public bool CanMoveSub
        {
            get { return null != SelectedSubActionsTreeView; }
        }

        private IParentElement findNewParent(IParentElement root, IChildElement child, bool searchUp = false, bool searchFromRoot = false)
        {
            if(searchFromRoot)
            {
                List<IChildElement> rootChildren;
                if (searchUp)
                {
                    rootChildren = root.SubChildren.Reverse().ToList();
                }
                else
                {
                    rootChildren = root.SubChildren.ToList();
                }
                foreach(IChildElement rootChild in rootChildren)
                {
                    if (rootChild is IParentElement)
                    {
                        if (rootChild.ValidChildren.Contains(child.ActionType))
                        {
                            return (rootChild as IParentElement);
                        }
                        else
                        {
                            IParentElement recurse = findNewParent((rootChild as IParentElement), child, searchUp, true);
                            if(null != recurse) { return recurse; }
                        }
                    }
                }
                return null; // Couldn't find a parent element from root
            }
            else
            {
                if(child.Parent is IAction)
                {
                    // this "child" is at the root of the Action, therefore it's parent will always be the Action
                    return (child.Parent as IParentElement);
                }
                else
                {
                    int parentIndex = (child.Parent.Parent as IParentElement).SubChildren.IndexOf(child.Parent as IChildElement); // get the index of the parent to search from

                    int[] indexesToSearch;
                    if (searchUp)
                    {
                        indexesToSearch = Enumerable.Range(0, parentIndex).Reverse().ToArray();
                    }
                    else
                    {
                        indexesToSearch = Enumerable.Range(parentIndex + 1, (child.Parent.Parent as IParentElement).SubChildren.Count - 1).ToArray();
                    }
                    foreach(int indexToSearch in indexesToSearch)
                    {
                        if(indexToSearch < (child.Parent.Parent as IParentElement).SubChildren.Count) // hacky bug fix to logic... should fix this
                        {
                            if ((child.Parent.Parent as IParentElement).SubChildren[indexToSearch] is IParentElement)
                            {
                                if (searchUp)
                                {
                                    // search through index and return first valid parent, else return index if it is a valid parent
                                    IParentElement recurse = findNewParent((child.Parent.Parent as IParentElement).SubChildren[indexToSearch] as IParentElement, child, searchUp, true);
                                    if (null != recurse) { return recurse; }
                                    else if (((child.Parent.Parent as IParentElement).SubChildren[indexToSearch] as IParentElement).ValidChildren.Contains(child.ActionType))
                                    {
                                        return (child.Parent.Parent as IParentElement).SubChildren[indexToSearch] as IParentElement;
                                    }
                                }
                                else
                                {
                                    if (((child.Parent.Parent as IParentElement).SubChildren[indexToSearch] as IParentElement).ValidChildren.Contains(child.ActionType))
                                    {
                                        return (child.Parent.Parent as IParentElement).SubChildren[indexToSearch] as IParentElement;
                                    }
                                    else
                                    {
                                        IParentElement recurse = findNewParent((child.Parent.Parent as IParentElement).SubChildren[indexToSearch] as IParentElement, child, searchUp, true);
                                        if (null != recurse) { return recurse; }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null; // Couldn't find a new parent element
        }

        public void MoveSub(string direction)
        {

            IChildElement selectedElement = SelectedSubActionsTreeView;
            int currentIndex = (selectedElement.Parent as IParentElement).SubChildren.IndexOf(selectedElement);
            int parentCount = (selectedElement.Parent as IParentElement).SubChildren.Count;
            switch (direction)
            {
                case "top":
                    if((SelectedActionsTreeView as IParentElement).ValidChildren.Contains(SelectedSubActionsTreeView.ActionType))
                    {
                        (selectedElement.Parent as IParentElement).SubChildren.Remove(selectedElement);
                        SubActionsTreeView.Insert(0, selectedElement);
                        selectedElement.Parent = SelectedActionsTreeView;
                        SubActionsTreeView[0].TVSelected = true;               
                    }
                    else
                    {
                        IParentElement newParent = findNewParent(SelectedActionsTreeView as IParentElement, SelectedSubActionsTreeView, false, true);
                        if(null != newParent)
                        {
                            (selectedElement.Parent as IParentElement).SubChildren.Remove(selectedElement);
                            newParent.SubChildren.Insert(0, selectedElement);
                            selectedElement.Parent = newParent;
                            selectedElement.TVSelected = true;
                        }
                    }
                    break;
                case "bottom":
                    if ((SelectedActionsTreeView as IParentElement).ValidChildren.Contains(SelectedSubActionsTreeView.ActionType))
                    {
                        (selectedElement.Parent as IParentElement).SubChildren.Remove(selectedElement);
                        SubActionsTreeView.Add(selectedElement);
                        selectedElement.Parent = SelectedActionsTreeView;
                        SubActionsTreeView[(SubActionsTreeView.Count - 1)].TVSelected = true;
                    }
                    else
                    {
                        IParentElement newParent = findNewParent(SelectedActionsTreeView as IParentElement, SelectedSubActionsTreeView, true, true);
                        if(null != newParent)
                        {
                            (selectedElement.Parent as IParentElement).SubChildren.Remove(selectedElement);
                            newParent.SubChildren.Add(selectedElement);
                            selectedElement.Parent = newParent;
                            selectedElement.TVSelected = true;
                        }
                    }
                    break;
                case "up":
                    if(currentIndex > 0)
                    {
                        (selectedElement.Parent as IParentElement).SubChildren.Move(currentIndex, currentIndex - 1);
                    }
                    else
                    {
                        IParentElement newParent = findNewParent(SelectedActionsTreeView as IParentElement, selectedElement, true, false);
                        if(null != newParent)
                        {
                            (selectedElement.Parent as IParentElement).SubChildren.Remove(selectedElement);
                            newParent.SubChildren.Add(selectedElement);
                            selectedElement.Parent = newParent;
                            selectedElement.TVSelected = true;
                        }
                    }
                    break;
                case "down":
                    if (currentIndex == parentCount - 1) // last element in parent
                    {
                        IParentElement newParent = findNewParent(SelectedActionsTreeView as IParentElement, selectedElement, false, false);
                        if (null != newParent)
                        {
                            (selectedElement.Parent as IParentElement).SubChildren.Remove(selectedElement);
                            newParent.SubChildren.Insert(0, selectedElement);
                            selectedElement.Parent = newParent;
                            selectedElement.TVSelected = true;
                        }
                    }
                    else
                    {
                        (selectedElement.Parent as IParentElement).SubChildren.Move(currentIndex, currentIndex + 1);
                    }
                    break;
                default:
                    break;
            }
            _eventAggregator.BeginPublishOnUIThread(new ChangeUI("PreviewChange", null));
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

        public void ActionsTreeViewChanged(IElement selectedAction)
        {
            SelectedActionsTreeView = selectedAction;
            if (null != selectedAction && selectedAction.HasSubChildren)
            {
                SubActionsTreeView = (selectedAction as IParentElement).SubChildren;
            }
            NotifyOfPropertyChange(() => PreviewAcceptButtonVisible);
            NotifyOfPropertyChange(() => PreviewBackButtonVisible);
            NotifyOfPropertyChange(() => PreviewCancelButtonVisible);
            NotifyOfPropertyChange(() => PreviewRefreshButtonVisible);
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

        public string CustomPreviewVisibilityConverter
        {
            get
            {
                return PreviewBox.HasCustomPreview ? "Visible" : "Collapsed";
            }
        }

        public string PreviewVisibilityConverter
        {
            get
            {
                return PreviewBox.HasCustomPreview ? "Collapsed" : "Visible";
            }
        }

        private ObservableCollection<IPreview> _CustomPreview;
        public ObservableCollection<IPreview> CustomPreview
        {
            get { return _CustomPreview; }
            set
            {
                _CustomPreview = value;
                NotifyOfPropertyChange(() => CustomPreview);
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
                if(null != _SelectedActionsTreeView && _SelectedActionsTreeView.ViewModel.PreviewViewModel.HasCustomPreview)
                {
                    CustomPreview = new ObservableCollection<IPreview>();
                    CustomPreview.Add(_SelectedActionsTreeView.ViewModel.PreviewViewModel);
                }
                NotifyOfPropertyChange(() => SelectedActionsTreeView);
                NotifyOfPropertyChange(() => CanEditButton);
                NotifyOfPropertyChange(() => PreviewBox);
                NotifyOfPropertyChange(() => CustomPreview);
                NotifyOfPropertyChange(() => CustomPreviewVisibilityConverter);
                NotifyOfPropertyChange(() => PreviewVisibilityConverter);
                NotifyOfPropertyChange(() => PreviewRefreshButtonVisible);
                NotifyOfPropertyChange(() => PreviewBackButtonVisible);
                NotifyOfPropertyChange(() => PreviewCancelButtonVisible);
                NotifyOfPropertyChange(() => PreviewAcceptButtonVisible);
                NotifyOfPropertyChange(() => WindowHeight);
                NotifyOfPropertyChange(() => CanMoveAction);
                NotifyOfPropertyChange(() => AddSubActionList);
            }
        }

        private IEventAggregator _eventAggregator = new EventAggregator();
        public ActionsViewModel(IEventAggregator ea, UIpp uipp)
        {
            _eventAggregator = ea;
            _eventAggregator.Subscribe(this);
            Globals.EventAggregator = ea;
            UIpp = uipp;

            FontFamilies = new List<string>();
            foreach(FontFamily f in Fonts.SystemFontFamilies)
            {
                FontFamilies.Add(f.Source);
            }
            
            ActionsTreeView = UIpp.Actions.actions;
        }

        private List<string> FontFamilies;


        private string _Font;
        public string Font
        {
            get { return _Font; }
            set
            {
                if(FontFamilies.Contains(value))
                {
                    _Font = value;
                }
                else
                {
                    _Font = "Tahoma";
                }
                
                NotifyOfPropertyChange(() => Font);
            }
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

        private string _SelectedAddSubActionList;
        public string SelectedAddSubActionList
        {
            get { return _SelectedAddSubActionList; }
            set
            {
                _SelectedAddSubActionList = value;
                NotifyOfPropertyChange(() => SelectedAddSubActionList);
            }
        }

        public BindableCollection<string> AddSubActionList
        {
            get
            {
                if(null != SelectedActionsTreeView && SelectedActionsTreeView is IParentElement)
                {
                    List<string> subActions = (SelectedActionsTreeView as IParentElement).ValidChildren.ToList();
                    if(null != SelectedSubActionsTreeView)
                    {
                        subActions.Add(SelectedSubActionsTreeView.ActionType);

                        if(null != SelectedSubActionsTreeView.ValidChildren)
                            subActions.AddRange(SelectedSubActionsTreeView.ValidChildren);
                    }
                    return new BindableCollection<string>(subActions.Distinct());
                }
                else
                {
                    return new BindableCollection<string>();
                }
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
                                (nextAction as ActionGroup).AddChild(SelectedActionsTreeView, -1);
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

        public string SubDialogVisibilityConverter
        {
            get { return SubDialogIsVisible ? "Visible" : "Collapsed"; }
        }

        private bool _SubDialogIsVisible = false;
        public bool SubDialogIsVisible
        {
            get { return _SubDialogIsVisible; }
            set
            {
                _SubDialogIsVisible = value;
                _eventAggregator.BeginPublishOnUIThread(new SendMessage("DialogVisible", value)); // to hide the webbrowser element on some previews
                NotifyOfPropertyChange(() => SubDialogIsVisible);
                NotifyOfPropertyChange(() => SubDialogVisibilityConverter);
            }
        }

        public void SubAddButton()
        {
            SubDialogIsVisible = true;
        }

        public bool CanSubDeleteButton
        {
            get { return null != SelectedSubActionsTreeView; }
        }

        public void SubDeleteButton()
        {
            if(null != SelectedSubActionsTreeView.Parent)
            {
                (SelectedSubActionsTreeView.Parent as IParentElement).SubChildren.Remove(SelectedSubActionsTreeView);
            }
            _eventAggregator.BeginPublishOnUIThread(new ChangeUI("PreviewChange", null));
        }
               
        public void AddSubActionOk()
        {
            if (null == SelectedAddSubActionList)
                return;
            
            if (null == SelectedSubActionsTreeView)
            {
                IChildElement newChild = (IChildElement)Activator.CreateInstance(Type.GetType("UI__Editor.Models." + SelectedAddSubActionList), (SelectedActionsTreeView as IElement));
                (SelectedActionsTreeView as IParentElement).SubChildren.Add(newChild);
            }
            else
            {
                if (null != SelectedSubActionsTreeView.ValidChildren && SelectedSubActionsTreeView.ValidChildren.Contains(SelectedAddSubActionList))
                {
                    // Add a new child to the selected subaction
                    IChildElement newChild = (IChildElement)Activator.CreateInstance(Type.GetType("UI__Editor.Models." + SelectedAddSubActionList), (SelectedSubActionsTreeView as IElement));
                    (SelectedSubActionsTreeView as IParentElement).SubChildren.Add(newChild);
                }
                else if (SelectedSubActionsTreeView.ActionType == SelectedAddSubActionList && null != SelectedSubActionsTreeView.Parent)
                {
                    // Add a new child to the parent after the index of the selected child
                    int ssaIndex = (SelectedSubActionsTreeView.Parent as IParentElement).SubChildren.IndexOf(SelectedSubActionsTreeView);
                    IChildElement newChild = (IChildElement)Activator.CreateInstance(Type.GetType("UI__Editor.Models." + SelectedAddSubActionList), (SelectedSubActionsTreeView.Parent as IElement));
                    (SelectedSubActionsTreeView.Parent as IParentElement).SubChildren.Insert(ssaIndex + 1, newChild);
                }
                else
                {
                    IChildElement newChild = (IChildElement)Activator.CreateInstance(Type.GetType("UI__Editor.Models." + SelectedAddSubActionList), (SelectedActionsTreeView as IElement));
                    (SelectedActionsTreeView as IParentElement).SubChildren.Add(newChild);
                }
            }
            SubDialogIsVisible = false;
            _eventAggregator.BeginPublishOnUIThread(new ChangeUI("PreviewChange", null));
        }

        public void AddSubActionCancel()
        {
            SubDialogIsVisible = false;
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

        private string _Icon;
        public string Icon
        {
            get { return _Icon; }
            set
            {
                _Icon = value;
                NotifyOfPropertyChange(() => Icon);
            }
        }

        public void Handle(EventAggregators.ChangeUI change)
        {
            switch(change.Type)
            {
                case "font":
                    Font = (string)change.Data;
                    break;
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
                case "icon":
                    Icon = (string)change.Data;
                    break;
                default:
                    break;
            }
        }
    }
}