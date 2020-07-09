using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class InputTextViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Text"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public InputTextViewModel(InputText i)
        {
            ModelClass = i;
            PreviewViewModel = new Preview.Children.InputTextViewModel();
        }

        private List<string> _ADValidations = new List<string>()
        {
            "",
            "Computer",
            "ComputerWarning",
            "User",
            "UserWarning"
        };
        public List<string> ADValidations
        {
            get { return _ADValidations; }
            set
            {
                _ADValidations = value;
                NotifyOfPropertyChange(() => ADValidations);
            }
        }

        private string _SelectedADValidation;
        public string SelectedADValidation
        {
            get { return _SelectedADValidation; }
            set
            {
                _SelectedADValidation = value;
                ADValidate = value;
            }
        }

        public string ADValidate
        {
            get { return (ModelClass as InputText).ADValidate; }
            set
            {
                (ModelClass as InputText).ADValidate = value;
            }
        }

        public string Default
        {
            get { return (ModelClass as InputText).Default; }
            set
            {
                (ModelClass as InputText).Default = value;
                (PreviewViewModel as Preview.Children.InputTextViewModel).Default = value;
            }
        }

        private List<string> _ForceCases = new List<string>()
        {
            "No",
            "Upper",
            "Lower"
        };
        public List<string> ForceCases
        {
            get { return _ForceCases; }
            set
            {
                _ForceCases = value;
                NotifyOfPropertyChange(() => ForceCases);
            }
        }

        private string _SelectedForceCase;
        public string SelectedForceCase
        {
            get { return _SelectedForceCase; }
            set
            {
                _SelectedForceCase = value;
                ForceCase = value;
                switch (value)
                {
                    case "Upper":
                        (PreviewViewModel as Preview.Children.InputTextViewModel).CharCasing = "Upper";
                        break;
                    case "Lower":
                        (PreviewViewModel as Preview.Children.InputTextViewModel).CharCasing = "Lower";
                        break;
                    case "No":
                        (PreviewViewModel as Preview.Children.InputTextViewModel).CharCasing = "Normal";
                        break;
                }
                NotifyOfPropertyChange(() => SelectedForceCase);
            }
        }

        public string ForceCase
        {
            get { return (ModelClass as InputText).ForceCase; }
            set
            {
                (ModelClass as InputText).ForceCase = value;    
                NotifyOfPropertyChange(() => SelectedForceCase);
            }
        }

        public string Hint
        {
            get { return (ModelClass as InputText).Hint; }
            set
            {
                (ModelClass as InputText).Hint = value;
                (PreviewViewModel as Preview.Children.InputTextViewModel).Hint = value;
            }
        }

        public bool HScroll
        {
            get { return (ModelClass as InputText).HScroll; }
            set
            {
                (ModelClass as InputText).HScroll = value;
            }
        }

        public bool Password
        {
            get { return (ModelClass as InputText).Password; }
            set
            {
                (ModelClass as InputText).Password = value;
                (PreviewViewModel as Preview.Children.InputTextViewModel).Password = value;
            }
        }

        public string Prompt
        {
            get { return (ModelClass as InputText).Prompt; }
            set
            {
                (ModelClass as InputText).Prompt = value;
                (PreviewViewModel as Preview.Children.InputTextViewModel).Prompt = value;
            }
        }

        public string Question
        {
            get { return (ModelClass as InputText).Question; }
            set
            {
                (ModelClass as InputText).Question = value;
                (PreviewViewModel as Preview.Children.InputTextViewModel).Question = value;
            }
        }

        public string RegEx
        {
            get { return (ModelClass as InputText).RegEx; }
            set
            {
                (ModelClass as InputText).RegEx = value;
            }
        }

        public bool Required
        {
            get { return (ModelClass as InputText).Required; }
            set
            {
                (ModelClass as InputText).Required = value;
            }
        }

        public string Variable
        {
            get { return (ModelClass as InputText).Variable; }
            set
            {
                (ModelClass as InputText).Variable = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as InputText).Condition; }
            set
            {
                (ModelClass as InputText).Condition = value;
            }
        }
    }
}
