using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.ViewModels.Preview;
using UI__Editor.Models.ActionClasses;

namespace UI__Editor.ViewModels.Actions
{
    public class AppTreeViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview.AppTreeViewModel();
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "AppTree"; } }

        public string HiddenAttributes
        {
            get
            {
                return "";
            }
        }

        public AppTreeViewModel(AppTree appTree)
        {
            ModelClass = appTree;
        }

        public string ApplicationVariableBase
        {
            get { return (ModelClass as AppTree).ApplicationVariableBase; }
            set
            {
                (ModelClass as AppTree).ApplicationVariableBase = value;
            }
        }
        public string PackageVariableBase
        {
            get { return (ModelClass as AppTree).PackageVariableBase; }
            set { (ModelClass as AppTree).PackageVariableBase = value; }
        }
        public bool? ShowBack
        {
            get { return (ModelClass as AppTree).ShowBack; }
            set { (ModelClass as AppTree).ShowBack = value; }
        }
        public bool? ShowCancel
        {
            get { return (ModelClass as AppTree).ShowCancel; }
            set { (ModelClass as AppTree).ShowCancel = value; }
        }
        public string Title
        {
            get { return (ModelClass as AppTree).Title; }
            set { (ModelClass as AppTree).Title = value; }
        }
        public string Size
        {
            get { return (ModelClass as AppTree).Size; }
            set { (ModelClass as AppTree).Size = value; }
        }
        public bool? Expanded
        {
            get { return (ModelClass as AppTree).Expanded; }
            set { (ModelClass as AppTree).Expanded = value; }
        }

        public bool CenterTitle
        {
            get { return (ModelClass as AppTree).CenterTitle; }
            set
            {
                (ModelClass as AppTree).CenterTitle = value;
                (PreviewViewModel as Preview.AppTreeViewModel).CenterTitle = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as AppTree).Condition; }
            set { (ModelClass as AppTree).Condition = value; }
        }
    }
}
