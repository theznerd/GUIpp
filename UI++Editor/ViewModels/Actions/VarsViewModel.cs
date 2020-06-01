using Caliburn.Micro;
using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    public class VarsViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Variables"; } }
        public string HiddenAttributes
        {
            get 
            {
                string ha;
                ha = "Direction: " + SelectedDirection;
                ha += "\r\nFilename: " + Filename;
                return ha;
            }
        }

        public VarsViewModel(Vars t)
        {
            ModelClass = t;
            EventAggregator = t.EventAggregator;
        }

        private BindableCollection<string> _Direction = new BindableCollection<string>()
        {
            "Save",
            "Load"
        };
        public BindableCollection<string> Direction
        {
            get { return _Direction; }
            set
            {
                _Direction = value;
                NotifyOfPropertyChange(() => Direction);
            }
        }

        private string _SelectedDirection;
        public string SelectedDirection
        {
            get { return _SelectedDirection; }
            set
            {
                _SelectedDirection = value;
                (ModelClass as Vars).Direction = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", value));
                NotifyOfPropertyChange(() => SelectedDirection);
            }
        }

        public string Filename
        {
            get { return (ModelClass as Vars).Filename; }
            set
            {
                (ModelClass as Vars).Filename = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Filename);
            }
        }

        public string Condition
        {
            get { return (ModelClass as Vars).Condition; }
            set
            {
                (ModelClass as Vars).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }
    }
}
