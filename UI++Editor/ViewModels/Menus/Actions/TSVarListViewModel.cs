using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class TSVarListViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "TS Variable List"; } }
        public string XMLOutput()
        {
            return "";
        }
        private ObservableCollection<object> _Children;
        public ObservableCollection<object> Children
        {
            get { return _Children; }
            set
            {
                _Children = value;
                NotifyOfPropertyChange(() => Children);
            }
        }

        private string _ConditionString;
        public string ConditionString
        {
            get { return _ConditionString; }
            set
            {
                _ConditionString = value;
                NotifyOfPropertyChange(() => ConditionString);
            }
        }

        private string _TSVarListApp;
        public string TSVarListApp
        {
            get { return _TSVarListApp; }
            set
            {
                _TSVarListApp = value;
                NotifyOfPropertyChange(() => TSVarListApp);
            }
        }

        private string _TSVarListPkg;
        public string TSVarListPkg
        {
            get { return _TSVarListPkg; }
            set
            {
                _TSVarListPkg = value;
                NotifyOfPropertyChange(() => TSVarListPkg);
            }
        }
    }
}
