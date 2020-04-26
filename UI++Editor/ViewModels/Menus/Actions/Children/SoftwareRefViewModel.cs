using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class SoftwareRefViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Software Ref"; } }
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

        private bool _Hidden;
        public bool Hidden
        {
            get { return _Hidden; }
            set
            {
                _Hidden = value;
                NotifyOfPropertyChange(() => Hidden);
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
