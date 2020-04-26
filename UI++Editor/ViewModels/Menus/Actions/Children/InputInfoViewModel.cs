using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public class InputInfoViewModel : PropertyChangedBase, IChild
    {
        public string ID { get; set; }
        public bool HasChildren { get { return false; } }
        public string ChildTitle { get { return "Info"; } }
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

        private string _Color;
        public string Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }

        private string _NumberofLines;
        public string NumberofLines
        {
            get { return _NumberofLines; }
            set
            {
                _NumberofLines = value;
                NotifyOfPropertyChange(() => NumberofLines);
            }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                NotifyOfPropertyChange(() => Text);
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
