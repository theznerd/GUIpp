using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using UI__Editor.Interfaces;
using UI__Editor.Models.ActionClasses;

namespace UI__Editor.ViewModels.Preview
{
    public class AppTreeViewModel : PropertyChangedBase, IPreview, IHandle<EventAggregators.ChangeUI>
    {
        public IEventAggregator EventAggregator { get; set; }
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return true; } }
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

        public string WindowHeight { get; set; } = "Regular";
        public string Font { get { return Globals.DisplayFont; } }
        public bool HasCustomPreview { get; } = false;

        public void Handle(EventAggregators.ChangeUI message)
        {
            switch (message.Type)
            {
                case "PreviewChange":
                    NotifyOfPropertyChange(() => TreeView);
                    break;
            }
        }

        private AppTree appTree;

        private ObservableCollection<IChildElement> _TreeViewContent;
        public ObservableCollection<IChildElement> TreeViewContent
        {
            get { return _TreeViewContent; }
            set
            {
                _TreeViewContent = value;
                NotifyOfPropertyChange(() => TreeViewContent);
                NotifyOfPropertyChange(() => TreeView);
            }
        }

        public ObservableCollection<IChildElement> TreeView
        {
            get
            {
                ObservableCollection<IChildElement> treeview = new ObservableCollection<IChildElement>();
                foreach(Models.Set s in appTree.SubChildren)
                {
                    foreach(IChildElement c in s.SubChildren)
                    {
                        treeview.Add(c);
                    }
                }
                return treeview;
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

        public string CenterTitleConverter
        {
            get { return CenterTitle ? "Center" : "Left"; }
        }

        public AppTreeViewModel(AppTree a, IEventAggregator ea)
        {
            appTree = a;
            EventAggregator = ea;
            EventAggregator.Subscribe(this);
            // TreeViewContent = appTree.SubChildren;
        }
    }
}
