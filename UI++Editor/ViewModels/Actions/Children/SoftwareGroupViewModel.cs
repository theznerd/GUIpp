using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.Interfaces;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;
using UI__Editor.ViewModels.Preview.Children;
using UI__Editor.Views.Preview.Children;
using UI__Editor.EventAggregators;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class SoftwareGroupViewModel : PropertyChangedBase, IAction, IHandle<EventAggregators.ChangeUI>
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Software Group"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }
        private IEventAggregator eventAggregator;

        public SoftwareGroupViewModel(SoftwareGroup g)
        {
            ModelClass = g;
            eventAggregator = Globals.EventAggregator;
            eventAggregator.Subscribe(this);
        }

        private Enums.AppTreeEnum.CheckStyle GetCheckStyle()
        {
            if (((ModelClass as IAppTreeSubChild).Parent is IAppTreeSubChild) && ((ModelClass as IAppTreeSubChild).Parent as IAppTreeSubChild).Required)
            {
                return Enums.AppTreeEnum.CheckStyle.DisabledChecked;
            }
            else if (((ModelClass as IAppTreeSubChild).Parent is IAppTreeSubChild) && ((ModelClass as IAppTreeSubChild).Parent as IAppTreeSubChild).Default)
            {
                return Enums.AppTreeEnum.CheckStyle.Checked;
            }

            if (Required)
            {
                return Enums.AppTreeEnum.CheckStyle.DisabledChecked;
            }
            else if (Default)
            {
                return Enums.AppTreeEnum.CheckStyle.Checked;
            }
            else
            {
                return Enums.AppTreeEnum.CheckStyle.Unchecked;
            }
        }

        private Enums.AppTreeEnum.IconStyle GetIconStyle()
        {
            if (((ModelClass as IAppTreeSubChild).Parent is IAppTreeSubChild) && ((ModelClass as IAppTreeSubChild).Parent as IAppTreeSubChild).Required)
            {
                return Enums.AppTreeEnum.IconStyle.FolderLock;
            }

            if (Required)
            {
                return Enums.AppTreeEnum.IconStyle.FolderLock;
            }
            else
            {
                return Enums.AppTreeEnum.IconStyle.Folder;
            }
        }

        public void Handle(ChangeUI message)
        {
            switch (message.Type)
            {
                case "AppTreeChange":
                    (ModelClass as SoftwareGroup).CheckStyle = GetCheckStyle();
                    (ModelClass as SoftwareGroup).IconStyle = GetIconStyle();
                    break;
            }
        }

        public bool Default
        {
            get { return (ModelClass as SoftwareGroup).Default; }
            set
            {
                (ModelClass as SoftwareGroup).Default = value;
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("AppTreeChange", null));
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
            }
        }

        public string Id
        {
            get { return (ModelClass as SoftwareGroup).Id; }
            set
            {
                (ModelClass as SoftwareGroup).Id = value;
            }
        }

        public string Label
        {
            get { return (ModelClass as SoftwareGroup).Label; }
            set
            {
                (ModelClass as SoftwareGroup).Label = value;
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
            }
        }

        public bool Required
        {
            get { return (ModelClass as SoftwareGroup).Required; }
            set
            {
                (ModelClass as SoftwareGroup).Required = value;
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("AppTreeChange", null));
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
            }
        }

        public string Condition
        {
            get { return (ModelClass as SoftwareGroup).Condition; }
            set
            {
                (ModelClass as SoftwareGroup).Condition = value;
            }
        }
    }
}
