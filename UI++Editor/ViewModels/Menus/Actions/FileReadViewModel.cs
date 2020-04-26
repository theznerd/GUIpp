using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public class FileReadViewModel : PropertyChangedBase, IAction
    {
        public string ID { get; set; }
        public object _PreviewViewModel = new Preview._NoPreviewViewModel();
        public object PreviewViewModel { get { return _PreviewViewModel; } }
        public bool HasChildren { get { return false; } }
        public string ActionTitle { get { return "File Read"; } }
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

        private string _FileReadFileName;
        public string FileReadFileName
        {
            get { return _FileReadFileName; }
            set
            {
                _FileReadFileName = value;
                NotifyOfPropertyChange(() => FileReadFileName);
            }
        }

        private string _FileReadVariable;
        public string FileReadVariable
        {
            get { return _FileReadVariable; }
            set
            {
                _FileReadVariable = value;
                NotifyOfPropertyChange(() => FileReadVariable);
            }
        }

        private bool _FileReadDeleteLine;
        public bool FileReadDeleteLine
        {
            get { return _FileReadDeleteLine; }
            set
            {
                _FileReadDeleteLine = value;
                NotifyOfPropertyChange(() => FileReadDeleteLine);
            }
        }
    }
}
