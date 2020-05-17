using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    class FileReadViewModel : IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview._NoPreviewViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "File Read"; } }
        public string HiddenAttributes
        {
            get
            {
                string output = "";
                output += "File Name: " + FileName;
                output += "\r\nVariable: " + Variable;
                output += "\r\nDelete Line after Read: " + DeleteLine;
                return output;
            }
        }

        public FileReadViewModel(FileRead fr)
        {
            ModelClass = fr;
            EventAggregator = fr.EventAggregator;
        }

        public string FileName
        {
            get { return (ModelClass as FileRead).FileName; }
            set
            {
                (ModelClass as FileRead).FileName = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Variable
        {
            get { return (ModelClass as FileRead).Variable; }
            set
            {
                (ModelClass as FileRead).Variable = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public bool? DeleteLine
        {
            get { return (ModelClass as FileRead).DeleteLine; }
            set
            {
                (ModelClass as FileRead).DeleteLine = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
            }
        }

        public string Condition
        {
            get { return (ModelClass as FileRead).Condition; }
            set
            {
                (ModelClass as FileRead).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
            }
        }

        
    }
}
