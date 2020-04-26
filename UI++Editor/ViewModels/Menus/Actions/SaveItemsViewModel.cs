using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class SaveItemsViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "Save Items"; } }
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

        private string _SaveItemsItems;
        public string SaveItemsItems
        {
            get { return _SaveItemsItems; }
            set
            {
                _SaveItemsItems = value;
                NotifyOfPropertyChange(() => SaveItemsItems);
            }
        }

        private string _SaveItemsPath;
        public string SaveItemsPath
        {
            get { return _SaveItemsPath; }
            set
            {
                _SaveItemsPath = value;
                NotifyOfPropertyChange(() => SaveItemsPath);
            }
        }
    }
}
