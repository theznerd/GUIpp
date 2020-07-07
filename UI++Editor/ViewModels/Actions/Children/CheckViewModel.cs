using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;
using UI__Editor.ViewModels.Preview.Children;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class CheckViewModel : PropertyChangedBase, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Check"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public CheckViewModel(Check c)
        {
            ModelClass = c;
            PreviewViewModel = new Preview.Children.CheckViewModel();
        }

        private List<string> _PreviewAs = new List<string>()
        {
            "Pass",
            "Warn",
            "Fail"
        };
        public List<string> PreviewAs
        {
            get { return _PreviewAs; }
            set
            {
                _PreviewAs = value;
                NotifyOfPropertyChange(() => PreviewAs);
            }
        }

        private string _SelectedPreviewA = "Pass";
        public string SelectedPreviewA
        {
            get { return _SelectedPreviewA; }
            set
            {
                _SelectedPreviewA = value;
                (PreviewViewModel as Preview.Children.CheckViewModel).PreviewAs = value;
            }
        }

        public string Description
        {
            get { return (ModelClass as Check).Description; }
            set
            {
                (ModelClass as Check).Description = value;
                (PreviewViewModel as Preview.Children.CheckViewModel).Description = value;
            }
        }

        public string ErrorDescription
        {
            get { return (ModelClass as Check).ErrorDescription; }
            set
            {
                (ModelClass as Check).ErrorDescription = value;
                (PreviewViewModel as Preview.Children.CheckViewModel).ErrorDescription = value;
            }
        }

        public string WarnDescription
        {
            get { return (ModelClass as Check).WarnDescription; }
            set
            {
                (ModelClass as Check).WarnDescription = value;
                (PreviewViewModel as Preview.Children.CheckViewModel).WarnDescription = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as Check).Condition; }
            set
            {
                (ModelClass as Check).Condition = value;
            }
        }

        public string CheckCondition
        {
            get { return (ModelClass as Check).CheckCondition; }
            set
            {
                (ModelClass as Check).CheckCondition = value;
            }
        }

        public string WarnCondition
        {
            get { return (ModelClass as Check).WarnCondition; }
            set
            {
                (ModelClass as Check).WarnCondition = value;
            }
        }

        public string Text
        {
            get { return (ModelClass as Check).Text; }
            set
            {
                (ModelClass as Check).Text = value;
                (PreviewViewModel as Preview.Children.CheckViewModel).Text = value;
            }
        }
    }
}
