using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class SoftwareGroupViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return true; } }
        public string ChildTitle { get { return "Group"; } }
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

        private bool _Default;
        public bool Default
        {
            get { return _Default; }
            set
            {
                _Default = value;
                NotifyOfPropertyChange(() => Default);
            }
        }

        private string _Id;
        public string Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private string _Label;
        public string Label
        {
            get { return _Label; }
            set
            {
                _Label = value;
                NotifyOfPropertyChange(() => Label);
            }
        }

        private bool _Required;
        public bool Required
        {
            get { return _Required; }
            set
            {
                _Required = value;
                NotifyOfPropertyChange(() => Required);
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
