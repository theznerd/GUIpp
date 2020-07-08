using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Preview.Children
{
    class InputInfoViewModel : PropertyChangedBase, IChild, IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewBackButtonVisible { get { return false; } }
        public bool PreviewCancelButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }
        public bool HasCustomPreview { get { return false; } }
        public string WindowHeight { get; set; }

        public string Font
        {
            get { return Globals.DisplayFont; }
        }

        private string _Color = "#000000";
        public string Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }

        private int _NumberOfLines = 1;
        public int NumberOfLines
        {
            get { return _NumberOfLines; }
            set
            {
                _NumberOfLines = value;
                NotifyOfPropertyChange(() => NumberOfLines);
                NotifyOfPropertyChange(() => LineHeight);
            }
        }

        public string LineHeight
        {
            get 
            {
                if (NumberOfLines <= 2 && NumberOfLines >= 1)
                    return (NumberOfLines * 22).ToString();
                else
                    return "22";
            }
        }

        private string _Content;
        public string Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                NotifyOfPropertyChange(() => Content);
            }
        }
    }
}
