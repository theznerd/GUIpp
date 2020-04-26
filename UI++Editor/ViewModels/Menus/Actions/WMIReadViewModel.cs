using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class WMIReadViewModel : PropertyChangedBase, IAction
    {
        // Back End Data
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "WMI Read"; } }
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

        // GUI Data
        private string _SelectedWMIReadType;
        public string SelectedWMIReadType
        {
            get { return _SelectedWMIReadType; }
            set
            {
                _SelectedWMIReadType = value;
                NotifyOfPropertyChange(() => SelectedWMIReadType);
            }
        }

        private string _WMIReadVariable;
        public string WMIReadVariable
        {
            get { return _WMIReadVariable; }
            set
            {
                _WMIReadVariable = value;
                NotifyOfPropertyChange(() => WMIReadVariable);
            }
        }

        private string _WMIReadDefaultValue;
        public string WMIReadDefaultValue
        {
            get { return _WMIReadDefaultValue; }
            set
            {
                _WMIReadDefaultValue = value;
                NotifyOfPropertyChange(() => WMIReadDefaultValue);
            }
        }

        private string _WMIReadNamespace;
        public string WMIReadNamespace
        {
            get { return _WMIReadNamespace; }
            set
            {
                _WMIReadNamespace = value;
                NotifyOfPropertyChange(() => WMIReadNamespace);
            }
        }

        private string _WMIReadClass;
        public string WMIReadClass
        {
            get { return _WMIReadClass; }
            set
            {
                _WMIReadClass = value;
                NotifyOfPropertyChange(() => WMIReadClass);
            }
        }

        private string _WMIReadKeyQualifier;
        public string WMIReadKeyQualifier
        {
            get { return _WMIReadKeyQualifier; }
            set
            {
                _WMIReadKeyQualifier = value;
                NotifyOfPropertyChange(() => WMIReadKeyQualifier);
            }
        }

        private string _WMIReadQuery;
        public string WMIReadQuery
        {
            get { return _WMIReadQuery; }
            set
            {
                _WMIReadQuery = value;
                NotifyOfPropertyChange(() => WMIReadQuery);
            }
        }

        private string _WMIReadProperty;
        public string WMIReadProperty
        {
            get { return _WMIReadProperty; }
            set
            {
                _WMIReadProperty = value;
                NotifyOfPropertyChange(() => WMIReadProperty);
            }
        }
    }
}
