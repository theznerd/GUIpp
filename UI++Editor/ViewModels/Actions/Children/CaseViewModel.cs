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
    public class CaseViewModel : IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Case"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public CaseViewModel(Case c)
        {
            ModelClass = c;
        }

        public string RegEx
        {
            get { return (ModelClass as Case).RegEx; }
            set
            {
                (ModelClass as Case).RegEx = value;
            }
        }

        public bool DontEval
        {
            get { return (ModelClass as Case).DontEval; }
            set
            {
                (ModelClass as Case).DontEval = value;
            }
        }

        public bool CaseInsensitive
        {
            get { return (ModelClass as Case).CaseInsensitive; }
            set
            {
                (ModelClass as Case).CaseInsensitive = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as Case).Condition; }
            set
            {
                (ModelClass as Case).Condition = value;
            }
        }

    }
}
