using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    public class InfoViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview.InfoViewModel();
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Info"; } }

        public InfoViewModel(Info info)
        {
            ModelClass = info;
            PreviewViewModel.EventAggregator = info.EventAggregator;
        }

        public bool? ShowBack
        {
            get { return (ModelClass as Info).ShowBack; }
            set 
            { 
                (ModelClass as Info).ShowBack = value;
                (PreviewViewModel as Preview.InfoViewModel).PreviewBackButtonVisible = value == true ? true : false;
            }
        }
        public bool? ShowCancel
        {
            get { return (ModelClass as Info).ShowCancel; }
            set 
            { 
                (ModelClass as Info).ShowCancel = value;
                (PreviewViewModel as Preview.InfoViewModel).PreviewCancelButtonVisible = value == true ? true : false;
            }
        }
        public string Name
        {
            get { return (ModelClass as Info).Name; }
            set { (ModelClass as Info).Name = value; }
        }
        public string Image
        {
            get { return (ModelClass as Info).Image; }
            set { (ModelClass as Info).Image = value; }
        }
        public string InfoImage
        {
            get { return (ModelClass as Info).InfoImage; }
            set { (ModelClass as Info).InfoImage = value; }
        }
        public string Title
        {
            get { return (ModelClass as Info).Title; }
            set 
            {
                (ModelClass as Info).Title = value;
                (PreviewViewModel as Preview.InfoViewModel).Title = value;
            }
        }
        public int? Timeout
        {
            get { return (ModelClass as Info).Timeout; }
            set { (ModelClass as Info).Timeout = value; }
        }
        public string TimeoutAction
        {
            get { return (ModelClass as Info).TimeoutAction; }
            set { (ModelClass as Info).TimeoutAction = value; }
        }
        public string Content
        {
            get { return (ModelClass as Info).Content; }
            set 
            { 
                (ModelClass as Info).Content = value;
                (PreviewViewModel as Preview.InfoViewModel).InfoViewText = value;
            }
        }
        public string Condition
        {
            get { return (ModelClass as Info).Condition; }
            set { (ModelClass as Info).Condition = value; }
        }
    }
}
