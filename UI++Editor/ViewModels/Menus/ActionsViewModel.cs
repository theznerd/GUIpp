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
using System.Collections.Specialized;
using System.Collections;

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

        private List<IChildElement> GetChildElements(IParentElement p)
        {
            List<IChildElement> childElements = new List<IChildElement>();
            foreach(IChildElement c in p.SubChildren)
            {
                childElements.Add(c);
                if(c is IParentElement)
                {
                    foreach(IChildElement rc in GetChildElements(c as IParentElement))
                    {
                        childElements.Add(rc);
                    }
                }
            }
            return childElements;
        }

        private List<IElement> GetParentElements(IElement c)
        {
            List<IElement> parentElements = new List<IElement>();
            if(null != c && null != c.Parent)
            {
                parentElements.Add(c.Parent);
                if (c.Parent is IAction)
                {
                    return parentElements;
                }
                else
                {
                    List<IElement> recurseParents = GetParentElements(c.Parent);
                    foreach (IElement rp in recurseParents)
                    {
                        parentElements.Add(rp);
                    }
                }
            }
            return parentElements;
        }

        private IParentElement findNewParent(IParentElement searchRoot, IChildElement child, bool searchUp = false)
        {
            // if searchroot is a valid parent, return it
            if (searchRoot.ValidChildren.Contains(child.ActionType))
            {
                return searchRoot;
            }
            // otherwise find the topmost or bottommost parent
            if (searchUp)
            {
                // start from the top and move down
                foreach (IChildElement c in searchRoot.SubChildren)
                {
                    if (c is IParentElement)
                    {
                        // if the parent is valid, return it, otherwise recurse down the 
                        // parent's subchildren for other parents
                        if (c.ValidChildren.Contains(child.ActionType))
                        {
                            return (c as IParentElement);
                        }
                        else
                        {
                            foreach (IChildElement rc in (c as IParentElement).SubChildren)
                            {
                                if (rc is IParentElement)
                                {
                                    IParentElement rp = findNewParent(rc as IParentElement, child, true);
                                    if (null != rp)
                                    {
                                        return rp;
                                    }
                                }
                            }
                        }
                    }
                }
                return null; // couldn't find a parent (this technically should never return null, because by definition the child exists in a parent somewhere)
            }
            else
            {
                // start from the bottom and move up
                foreach (IChildElement c in searchRoot.SubChildren.Reverse())
                {
                    if (c.ValidChildren.Contains(child.ActionType))
                    {
                        return (c as IParentElement);
                    }
                    if (c is IParentElement)
                    {
                        foreach (IChildElement rc in (c as IParentElement).SubChildren.Reverse())
                        {
                            if (rc is IParentElement)
                            {
                                IParentElement rp = findNewParent(rc as IParentElement, child, false);
                                if (null != rp)
                                {
                                    return rp;
                                }
                            }
                        }
                    }
                }
                return null; // couldn't find a parent (this technically should never return null, because by definition the child exists in a parent somewhere) 
            }
        }

        private IParentElement findParentWithinParent(IParentElement parentToSearch, IChildElement child, bool searchUp)
        {
            if (parentToSearch.ValidChildren.Contains(child.ActionType))
            {
                // the parent we're searching is a valid parent, just return it
                return parentToSearch;
            }
            else
            {
                if (searchUp)
                {
                    foreach(IChildElement c in parentToSearch.SubChildren.Reverse())
                    {
                        if(c is IParentElement)
                        {
                            IParentElement rp = findParentWithinParent(c as IParentElement, child, searchUp);
                            if (null != rp)
                                return rp;
                        }
                    }
                }
                else
                {
                    // enumerate the parent's children, and search them too
                    foreach(IChildElement c in parentToSearch.SubChildren)
                    {
                        if(c is IParentElement)
                        {
                            IParentElement rp = findParentWithinParent(c as IParentElement, child, searchUp);
                            if (null != rp)
                                return rp;
                        }
                    }
                }
            }
            return null;
        }

        private IParentElement findParentInGrandparent(IParentElement grandParentToSearch, IChildElement child, bool searchUp, int startIndex)
        {
            if (grandParentToSearch.ValidChildren.Contains(child.ActionType))
            {
                // grandparent is a valid parent... return the grandparent
                return grandParentToSearch;
            }
            else
            {
                int[] rangeToSearch;
                if (searchUp)
                {
                    if(startIndex > 0)
                    {
                        rangeToSearch = Enumerable.Range(0, startIndex - 1).ToArray();
                    }
                    else
                    {
                        rangeToSearch = new int[] { 0 };
                    }
                }
                else
                {
                    rangeToSearch = Enumerable.Range(startIndex + 1, grandParentToSearch.SubChildren.Count - 1).ToArray();
                }
                foreach(int i in rangeToSearch)
                {
                    if(grandParentToSearch.SubChildren[i] is IParentElement)
                    {
                        IParentElement rp = findParentWithinParent(grandParentToSearch.SubChildren[i] as IParentElement, child, searchUp);
                        if (null != rp)
                            return rp;
                    }
                }
            }
            if(!(grandParentToSearch is IAction))
            {
                // can you take me hiiiiiigher...
                IParentElement ggp = findParentInGrandparent(grandParentToSearch.Parent as IParentElement, child, searchUp, grandParentToSearch.SubChildren.IndexOf(grandParentToSearch as IChildElement));
                if (null != ggp)
                {
                    return ggp;
                }
            }
            return null;
        }

        public void MoveSub(string direction)
        {
            IChildElement childElement = SelectedSubActionsTreeView;
            int childIndex = (childElement.Parent as IParentElement).SubChildren.IndexOf(childElement);
            int parentCount = (childElement.Parent as IParentElement).SubChildren.Count;
            switch(direction)
            {
                case "top":
                    IParentElement topParent = findNewParent(SelectedActionsTreeView as IParentElement, SelectedSubActionsTreeView, true);
                    (childElement.Parent as IParentElement).SubChildren.Remove(childElement);
                    topParent.SubChildren.Insert(0, childElement);
                    childElement.Parent = topParent;
                    break;
                case "bottom":
                    IParentElement bottomParent = findNewParent(SelectedActionsTreeView as IParentElement, SelectedSubActionsTreeView, false);
                    (childElement.Parent as IParentElement).SubChildren.Remove(childElement);
                    bottomParent.SubChildren.Add(childElement);
                    childElement.Parent = bottomParent;
                    break;
                case "down":
                    if(childIndex == parentCount - 1)
                    {
                        // if the parent is the action, we're as far down as we can go
                        if(!(childElement.Parent is IAction))
                        {
                            IParentElement nextParent = findParentInGrandparent((childElement.Parent.Parent as IParentElement), childElement, false, (childElement.Parent.Parent as IParentElement).SubChildren.IndexOf((childElement.Parent as IChildElement)) + 1);
                            if(null != nextParent)
                            {
                                (childElement.Parent as IParentElement).SubChildren.Remove(childElement);
                                nextParent.SubChildren.Insert((childElement.Parent.Parent as IParentElement).SubChildren.IndexOf((childElement.Parent as IChildElement)) + 1, childElement);
                                childElement.Parent = nextParent;
                            }
                        }
                    }
                    else if((childElement.Parent as IParentElement).SubChildren[childIndex + 1] is IParentElement)
                    {
                        // the next element after the child is a parent element, look for parents within
                        IParentElement nextParent = findParentWithinParent((childElement.Parent as IParentElement).SubChildren[childIndex + 1] as IParentElement, childElement, false);
                        if (null != nextParent)
                        {
                            (childElement.Parent as IParentElement).SubChildren.Remove(childElement);
                            nextParent.SubChildren.Insert(0, childElement);
                            childElement.Parent = nextParent;
                        }
                        else
                        {
                            // the next element isn't a valid parent, and has no valid parents... NEXT
                            (childElement.Parent as IParentElement).SubChildren.Move(childIndex, childIndex + 1);
                        }
                    }
                    else
                    {
                        // it's not at the end, and the next element isn't a parent, so move it
                        (childElement.Parent as IParentElement).SubChildren.Move(childIndex, childIndex + 1);
                    }
                    break;
                case "up":
                    if(childIndex == 0)
                    {
                        if (!(childElement.Parent is IAction))
                        {
                            IParentElement nextParent = findParentInGrandparent((childElement.Parent.Parent as IParentElement), childElement, true, (childElement.Parent.Parent as IParentElement).SubChildren.IndexOf((childElement.Parent as IChildElement)));
                            if (null != nextParent)
                            {
                                (childElement.Parent as IParentElement).SubChildren.Remove(childElement);
                                nextParent.SubChildren.Insert((childElement.Parent.Parent as IParentElement).SubChildren.IndexOf((childElement.Parent as IChildElement)), childElement);
                                childElement.Parent = nextParent;
                            }
                        }
                    }
                    else if((childElement.Parent as IParentElement).SubChildren[childIndex - 1] is IParentElement)
                    {
                        // the previous element is a parent element, look for parents within
                        IParentElement previousParent = findParentWithinParent((childElement.Parent as IParentElement).SubChildren[childIndex - 1] as IParentElement, childElement, true);
                        if(null != previousParent)
                        {
                            (childElement.Parent as IParentElement).SubChildren.Remove(childElement);
                            previousParent.SubChildren.Add(childElement);
                            childElement.Parent = previousParent;
                        }
                        else
                        {
                            // the previous element isn't a valid parent, and has no valid parents... PREVIOUS!
                            (childElement.Parent as IParentElement).SubChildren.Move(childIndex, childIndex - 1);
                        }
                    }
                    else
                    {
                        // it's not at the beginning, and the previous isn't a parent, so move it
                        (childElement.Parent as IParentElement).SubChildren.Move(childIndex, childIndex - 1);
                    }
                    // find previous parent
                    break;
            }
            foreach (IChildElement c in GetChildElements(SelectedActionsTreeView as IParentElement))
            {
                c.TVSelected = false;
            }
            foreach(IElement c in GetParentElements(childElement))
            {
                c.TVIsExpanded = true;
            }
            SelectedSubActionsTreeView = childElement;
            childElement.TVSelected = true;
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

        public IAction GetActionGrandparent(IAction child)
        {
            return null;
        }

        public List<IAction> GetChildActions(ActionGroup ag)
        {
            List<IAction> actions = new List<IAction>();
            foreach(IAction a in ag.Children)
            {
                actions.Add(a);
                if(a is ActionGroup)
                {
                    List<IAction> recursiveActions = new List<IAction>();
                    foreach(IAction ra in GetChildActions(a as ActionGroup))
                    {
                        actions.Add(ra);
                    }
                }
            }
            return actions;
        }

        public List<IAction> GetAllActions()
        {
            List<IAction> actions = new List<IAction>();
            foreach(IAction a in ActionsTreeView)
            {
                actions.Add(a);
                if(a is ActionGroup)
                {
                    foreach(IAction ra in GetChildActions(a as ActionGroup))
                    {
                        actions.Add(ra);
                    }
                }
            }
            return actions;
        }

        public void MoveAction(string position)
        {
            // TODO: Fix movements across groups. Movements are a touch buggy still
            bool isChild = (null != SelectedActionsTreeView.Parent);
            
            int parentIndex;
            int actionIndex;
            int parentCount;
            int actionCount = ActionsTreeView.Count;
            IElement selectedAction = SelectedActionsTreeView;

            switch(position)
            {
                case "top":
                    if (isChild)
                    {
                        (SelectedActionsTreeView.Parent as ActionGroup).RemoveChild(selectedAction);
                        ActionsTreeView.Insert(0, selectedAction);
                    }
                    else
                    {
                        actionIndex = ActionsTreeView.IndexOf(selectedAction);
                        ActionsTreeView.Move(actionIndex, 0);
                    }
                    break;
                case "bottom":
                    if (isChild)
                    {
                        (SelectedActionsTreeView.Parent as ActionGroup).RemoveChild(selectedAction);
                        ActionsTreeView.Add(selectedAction);
                    }
                    else
                    {
                        actionIndex = ActionsTreeView.IndexOf(selectedAction);
                        ActionsTreeView.Move(actionIndex, ActionsTreeView.Count - 1);
                    }
                    break;
                case "up":
                    if (isChild)
                    {
                        actionIndex = (SelectedActionsTreeView.Parent as ActionGroup).Children.IndexOf(selectedAction);
                        if(actionIndex == 0)
                        {
                            // move to the grandparent
                            if(SelectedActionsTreeView.Parent.Parent is ActionGroup)
                            {
                                parentIndex = (selectedAction.Parent.Parent as ActionGroup).Children.IndexOf(selectedAction.Parent);
                                ActionGroup newParent = selectedAction.Parent.Parent as ActionGroup;
                                newParent.Children.Insert(parentIndex, selectedAction);
                                (selectedAction.Parent as ActionGroup).Children.Remove(selectedAction);
                                selectedAction.Parent = newParent;
                            }
                            else
                            {
                                parentIndex = ActionsTreeView.IndexOf(selectedAction.Parent);
                                ActionsTreeView.Insert(parentIndex, selectedAction);
                                (selectedAction.Parent as ActionGroup).Children.Remove(selectedAction);
                                selectedAction.Parent = null;
                            }
                        }
                        else if((SelectedActionsTreeView.Parent as ActionGroup).Children[actionIndex - 1] is ActionGroup)
                        {
                            // next child up is an action group... stuff it in there
                            ActionGroup newParent = (selectedAction.Parent as ActionGroup).Children[actionIndex - 1] as ActionGroup;
                            newParent.Children.Add(selectedAction);
                            (selectedAction.Parent as ActionGroup).Children.Remove(selectedAction);
                            selectedAction.Parent = newParent;
                        }
                        else
                        {
                            // we can just move the action up
                            (SelectedActionsTreeView.Parent as ActionGroup).Children.Move(actionIndex, actionIndex - 1);
                        }
                    }
                    else
                    {
                        actionIndex = ActionsTreeView.IndexOf(selectedAction);
                        if(actionIndex == 0)
                        {
                            // we're as high as we can go...
                        }
                        else if (ActionsTreeView[actionIndex - 1] is ActionGroup)
                        {
                            // if the next child up is an action group
                            ActionGroup newParent = (ActionsTreeView[actionIndex - 1] as ActionGroup);
                            newParent.Children.Add(selectedAction);
                            ActionsTreeView.Remove(selectedAction);
                            selectedAction.Parent = newParent;
                        }
                        else
                        {
                            // we can just move the action up
                            ActionsTreeView.Move(actionIndex, actionIndex - 1);
                        }
                    }
                    break;
                case "down":
                    if (isChild)
                    {
                        actionIndex = (SelectedActionsTreeView.Parent as ActionGroup).Children.IndexOf(selectedAction);
                        parentCount = (SelectedActionsTreeView.Parent as ActionGroup).Children.Count;
                        if (actionIndex == parentCount - 1)
                        {
                            // move to the grandparent
                            if (SelectedActionsTreeView.Parent.Parent is ActionGroup)
                            {
                                parentIndex = (selectedAction.Parent.Parent as ActionGroup).Children.IndexOf(selectedAction.Parent);
                                ActionGroup newParent = selectedAction.Parent.Parent as ActionGroup;
                                newParent.Children.Insert(parentIndex + 1, selectedAction);
                                (selectedAction.Parent as ActionGroup).Children.Remove(selectedAction);
                                selectedAction.Parent = newParent;
                            }
                            else
                            {
                                parentIndex = ActionsTreeView.IndexOf(selectedAction.Parent);
                                ActionsTreeView.Insert(parentIndex + 1, selectedAction);
                                (selectedAction.Parent as ActionGroup).Children.Remove(selectedAction);
                                selectedAction.Parent = null;
                            }
                        }
                        else if ((SelectedActionsTreeView.Parent as ActionGroup).Children[actionIndex + 1] is ActionGroup)
                        {
                            // next child down is an action group... stuff it in there
                            ActionGroup newParent = (selectedAction.Parent as ActionGroup).Children[actionIndex + 1] as ActionGroup;
                            newParent.Children.Insert(0, selectedAction);
                            (selectedAction.Parent as ActionGroup).Children.Remove(selectedAction);
                            selectedAction.Parent = newParent;
                        }
                        else
                        {
                            // we can just move the action down
                            (SelectedActionsTreeView.Parent as ActionGroup).Children.Move(actionIndex, actionIndex + 1);
                        }
                    }
                    else
                    {
                        actionIndex = ActionsTreeView.IndexOf(selectedAction);
                        parentCount = ActionsTreeView.Count;
                        if (actionIndex == parentCount - 1)
                        {
                            // we're as low as we can go...
                        }
                        else if (ActionsTreeView[actionIndex + 1] is ActionGroup)
                        {
                            // if the next child up is an action group
                            ActionGroup newParent = (ActionsTreeView[actionIndex + 1] as ActionGroup);
                            newParent.Children.Insert(0, selectedAction);
                            ActionsTreeView.Remove(selectedAction);
                            selectedAction.Parent = newParent;
                        }
                        else
                        {
                            // we can just move the action up
                            ActionsTreeView.Move(actionIndex, actionIndex + 1);
                        }
                    }
                    break;
            }
            foreach (IElement e in GetAllActions())
            {
                e.TVSelected = false;
            }
            selectedAction.TVSelected = true;
            SelectedActionsTreeView = selectedAction;
            foreach (IElement c in GetParentElements(SelectedActionsTreeView))
            {
                c.TVIsExpanded = true;
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