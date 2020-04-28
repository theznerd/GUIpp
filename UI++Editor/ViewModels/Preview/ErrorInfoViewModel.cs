using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using UI__Editor.Views.Menus.Actions;

namespace UI__Editor.ViewModels.Preview
{
    class ErrorInfoViewModel : PropertyChangedBase, IPreview
    {
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewBackButtonVisible { get { return false; } }
        public bool PreviewCancelButtonVisible { get { return true; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }

        private string title = "Welcome";
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private string _InfoViewText = "UI++ 2.0 includes all of the power of UI++ 1.0 combined with UI App Tree!<br>It's UI, interactive, evolved, and customized.<br><br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe<br>hehehehe";
        public string InfoViewText
        {
            get { return _InfoViewText; }
            set
            {
                _InfoViewText = value;
                NotifyOfPropertyChange(() => InfoViewText);
            }
        }
    }
}
