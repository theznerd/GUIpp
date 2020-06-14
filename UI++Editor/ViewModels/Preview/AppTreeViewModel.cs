using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Preview
{
    public class AppTreeViewModel : PropertyChangedBase, IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewBackButtonVisible { get { return false; } }
        public bool PreviewCancelButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }
        public string WindowHeight { get; set; } = "Regular";
        public string Font { get; set; } = "Tahoma";
        public bool HasCustomPreview { get; } = false;

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
    }
}
