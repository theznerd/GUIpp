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
    class VariableViewModel : IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Variable"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public VariableViewModel (Variable v)
        {
            ModelClass = v;
        }

        public string Name
        {
            get { return (ModelClass as Variable).Name; }
            set
            {
                (ModelClass as Variable).Name = value;
            }
        }

        public bool DontEval
        {
            get { return (ModelClass as Variable).DontEval; }
            set
            {
                (ModelClass as Variable).DontEval = value;
            }
        }

        public string Value
        {
            get { return (ModelClass as Variable).Content; }
            set
            {
                (ModelClass as Variable).Content = value;
            }
        } 

        public string Condition
        {
            get { return (ModelClass as Variable).Condition; }
            set
            {
                (ModelClass as Variable).Condition = value;
            }
        }
    }
}
