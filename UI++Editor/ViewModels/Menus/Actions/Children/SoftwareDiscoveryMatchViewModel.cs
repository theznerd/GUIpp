using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class SoftwareDiscoveryMatchViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Match"; } }
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

        private string _DisplayName;
        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                _DisplayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        private string _Variable;
        public string Variable
        {
            get { return _Variable; }
            set
            {
                _Variable = value;
                NotifyOfPropertyChange(() => Variable);
            }
        }

        private string _Version;
        public string Version
        {
            get { return _Version; }
            set
            {
                _Version = value;
                NotifyOfPropertyChange(() => Version);
            }
        }

        private string _VersionOperator;
        public string VersionOperator
        {
            get { return _VersionOperator; }
            set
            {
                _VersionOperator = value;
                NotifyOfPropertyChange(() => VersionOperator);
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
    }
}
