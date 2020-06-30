using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;
namespace UI__Editor.ViewModels.Actions.Children
{
    public class MatchViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Match"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public MatchViewModel(Match m)
        {
            ModelClass = m;
        }

        private List<string> _VersionOperators = new List<string>()
        {
            "eq",
            "ne",
            "lt",
            "lte",
            "gt",
            "gte",
            "re"
        };

        public List<string> VersionOperators
        {
            get { return _VersionOperators; }
            set
            {
                _VersionOperators = value;
                NotifyOfPropertyChange(() => VersionOperators);
            }
        }

        private string _SelectedVersionOperator = "eq";
        public string SelectedVersionOperator
        {
            get { return _SelectedVersionOperator; }
            set
            {
                _SelectedVersionOperator = value;
                VersionOperator = value;
                NotifyOfPropertyChange(() => SelectedVersionOperator);
            }
        }

        public string VersionOperator
        {
            get { return (ModelClass as Match).VersionOperator; }
            set
            {
                (ModelClass as Match).VersionOperator = value;
            }
        }

        public string DisplayName
        {
            get { return (ModelClass as Match).DisplayName; }
            set
            {
                (ModelClass as Match).DisplayName = value;
            }
        }

        public string Variable
        {
            get { return (ModelClass as Match).Variable; }
            set
            {
                (ModelClass as Match).Variable = value;
            }
        }

        public string Version
        {
            get { return (ModelClass as Match).Version; }
            set
            {
                (ModelClass as Match).Version = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as Match).Condition; }
            set
            {
                (ModelClass as Match).Condition = value;
            }
        }
    }
}
