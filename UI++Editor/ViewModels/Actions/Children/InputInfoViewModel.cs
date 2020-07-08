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
    public class InputInfoViewModel : IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Info"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public InputInfoViewModel(InputInfo i)
        {
            ModelClass = i;
            PreviewViewModel = new Preview.Children.InputInfoViewModel();
        }

        public string Color
        {
            get { return (ModelClass as InputInfo).Color; }
            set
            {
                (ModelClass as InputInfo).Color = value.Replace("#FF", "#");
                (PreviewViewModel as Preview.Children.InputInfoViewModel).Color = value.Replace("#FF", "#");
            }
        }

        public string Content
        {
            get { return (ModelClass as InputInfo).Content; }
            set
            {
                (ModelClass as InputInfo).Content = value;
                (PreviewViewModel as Preview.Children.InputInfoViewModel).Content = value;
            }
        }

        public int NumberOfLines
        {
            get { return (ModelClass as InputInfo).NumberOfLines; }
            set
            {
                (ModelClass as InputInfo).NumberOfLines = value;
                (PreviewViewModel as Preview.Children.InputInfoViewModel).NumberOfLines = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as InputInfo).Condition; }
            set
            {
                (ModelClass as InputInfo).Condition = value;
            }
        }
    }
}
