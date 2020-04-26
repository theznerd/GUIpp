using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class ValueType : PropertyChangedBase
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class DefaultValuesViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "Default Values"; } }
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

        public DefaultValuesViewModel()
        {
            ADefaultValuesValueTypes = new ObservableCollection<ValueType>
            {
                new ValueType{ Name = "All", IsSelected = false },
                new ValueType{ Name = "Asset", IsSelected = false },
                new ValueType{ Name = "Domain", IsSelected = false },
                new ValueType{ Name = "Mgmt", IsSelected = false },
                new ValueType{ Name = "Net", IsSelected = false },
                new ValueType{ Name = "OS", IsSelected = false },
                new ValueType{ Name = "Security", IsSelected = false },
                new ValueType{ Name = "User", IsSelected = false },
                new ValueType{ Name = "VM", IsSelected = false }
            };
        }

        private bool _ADefaultValuesShowProgress;
        public bool ADefaultValuesShowProgress
        {
            get { return _ADefaultValuesShowProgress; }
            set
            {
                _ADefaultValuesShowProgress = value;
                NotifyOfPropertyChange(() => ADefaultValuesShowProgress);
            }
        }


        private ObservableCollection<ValueType> _ADefaultValuesValueTypes;
        public ObservableCollection<ValueType> ADefaultValuesValueTypes
        {
            get { return _ADefaultValuesValueTypes; }
            set
            {
                _ADefaultValuesValueTypes = value;
                NotifyOfPropertyChange(() => ADefaultValuesValueTypes);
            }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        public IEnumerable<ValueType> SelectedValueTypes
        {
            get
            {
                return ADefaultValuesValueTypes.Where(vt => vt.IsSelected);
            }
        }
    }
}
