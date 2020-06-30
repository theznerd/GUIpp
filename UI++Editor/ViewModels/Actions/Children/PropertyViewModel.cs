using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class PropertyViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Property"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public PropertyViewModel(Property p)
        {
            ModelClass = p;
        }

        public bool Key
        {
            get { return (ModelClass as Property).Key; }
            set
            {
                (ModelClass as Property).Key = value;
            }
        }

        public string Name
        {
            get { return (ModelClass as Property).Name; }
            set
            {
                (ModelClass as Property).Name = value;
            }
        }

        public string Value
        {
            get { return (ModelClass as Property).Value; }
            set
            {
                (ModelClass as Property).Value = value;
            }
        }

        private List<string> _Types = new List<string>()
        {
            "CIM_SINT8",
            "CIM_UINT8",
            "CIM_SINT16",
            "CIM_UINT16",
            "CIM_SINT32",
            "CIM_UINT32",
            "CIM_SINT64",
            "CIM_UINT64",
            "CIM_REAL32",
            "CIM_REAL64",
            "CIM_BOOLEAN",
            "CIM_STRING",
            "CIM_DATETIME",
            "CIM_REFERENCE",
            "CIM_CHAR16",
            "CIM_OBJECT",
            "CIM_ILLEGAL",
            "CIM_EMPTY",
            "CIM_FLAG_ARRAY"
        };

        public List<string> Types
        {
            get { return _Types; }
            set
            {
                _Types = value;
                NotifyOfPropertyChange(() => Types);
                NotifyOfPropertyChange(() => SelectedType);
            }
        }

        private string _SelectedType = "CIM_STRING";
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

        public string Type
        {
            get { return (ModelClass as Property).Type; }
            set
            {
                (ModelClass as Property).Type = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as Property).Condition; }
            set
            {
                (ModelClass as Property).Condition = value;
            }
        }
    }
}
