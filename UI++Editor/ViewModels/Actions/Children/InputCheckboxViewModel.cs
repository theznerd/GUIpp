using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.EventAggregators;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class InputCheckboxViewModel : IAction, IHandle<ChangeUI>
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Checkbox"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public InputCheckboxViewModel(InputCheckbox i)
        {
            ModelClass = i;
            PreviewViewModel = new ViewModels.Preview.Children.InputCheckboxViewModel();
            Globals.EventAggregator.Subscribe(this);
        }

        public string CheckedValue
        {
            get { return (ModelClass as InputCheckbox).CheckedValue; }
            set
            {
                (ModelClass as InputCheckbox).CheckedValue = value;
            }
        }

        public string Default
        {
            get { return (ModelClass as InputCheckbox).Default; }
            set
            {
                (ModelClass as InputCheckbox).Default = value;
            }
        }

        public string Question
        {
            get { return (ModelClass as InputCheckbox).Question; }
            set
            {
                (ModelClass as InputCheckbox).Question = value;
                (PreviewViewModel as Preview.Children.InputCheckboxViewModel).Question = value;
            }
        }

        public string Variable
        {
            get { return (ModelClass as InputCheckbox).Variable; }
            set
            {
                (ModelClass as InputCheckbox).Variable = value;
            }
        }

        public string UncheckedValue
        {
            get { return (ModelClass as InputCheckbox).UncheckedValue; }
            set
            {
                (ModelClass as InputCheckbox).UncheckedValue = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as InputCheckbox).Condition; }
            set
            {
                (ModelClass as InputCheckbox).Condition = value;
            }
        }

        public void Handle(ChangeUI message)
        {
            switch (message.Type)
            {
                case "ImportComplete":
                    (PreviewViewModel as Preview.Children.InputCheckboxViewModel).Question = Question;
                    break;
            }
        }
    }
}
