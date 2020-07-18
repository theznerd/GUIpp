using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;
using UI__Editor.ViewModels.Preview.Children;
using UI__Editor.Views.Preview.Children;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class SetViewModel : IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Set"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public SetViewModel(Set s)
        {
            ModelClass = s;
        }

        public string Name
        {
            get { return (ModelClass as Set).Name; }
            set
            {
                (ModelClass as Set).Name = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as Set).Condition; }
            set
            {
                (ModelClass as Set).Condition = value;
            }
        }
    }
}
