using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class TextViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Text"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public TextViewModel(Text t)
        {
            ModelClass = t;
        }

        private List<string> _Types = new List<string>()
        {
            "Asset",
            "Domain",
            "Mgmt",
            "Net",
            "OS",
            "Security",
            "User",
            "VM"
        };
        public List<string> Types
        {
            get { return _Types; }
            set
            {
                _Types = value;
            }
        }

        private string _SelectedType;
        public string SelectedType
        {
            get { return _SelectedType; }
            set
            {
                _SelectedType = value;
                Type = value;
                NotifyOfPropertyChange(() => SelectedType);
            }
        }

        public string Value
        {
            get { return (ModelClass as Text).Value; }
            set
            {
                (ModelClass as Text).Value = value;
            }
        }

        public string Type
        {
            get { return (ModelClass as Text).Type; }
            set
            {
                (ModelClass as Text).Type = value;
                NotifyOfPropertyChange(() => Type);
                NotifyOfPropertyChange(() => SelectedType);
            }
        }

        public string Condition
        {
            get { return (ModelClass as Text).Condition; }
            set
            {
                (ModelClass as Text).Condition = value;
            }
        }
    }
}
