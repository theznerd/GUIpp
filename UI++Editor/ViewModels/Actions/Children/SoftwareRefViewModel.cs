using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.Interfaces;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;
using UI__Editor.ViewModels.Preview.Children;
using UI__Editor.Views.Preview.Children;
using UI__Editor.EventAggregators;
using MahApps.Metro.Controls.Dialogs;
using UI__Editor.ViewModels.Dialogs;
using UI__Editor.Views.Dialogs;
using System.Collections.ObjectModel;
using UI__Editor.ViewModels.Menus;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class SoftwareRefViewModel : PropertyChangedBase, IAction, IHandle<EventAggregators.ChangeUI>
    {
        public IPreview PreviewViewModel { get; set; }
        private SoftwareViewModel svm;

        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Software Ref"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }
        private IEventAggregator eventAggregator;

        public SoftwareRefViewModel(SoftwareRef r)
        {
            ModelClass = r;
            eventAggregator = Globals.EventAggregator;
            eventAggregator.Subscribe(this);
            svm = Globals.SoftwareViewModel;
        }

        public bool Hidden
        {
            get { return (ModelClass as SoftwareRef).Hidden; }
            set
            {
                (ModelClass as SoftwareRef).Hidden = value;
            }
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
                return Enums.AppTreeEnum.IconStyle.AppLock;
            }

            if (Required)
            {
                return Enums.AppTreeEnum.IconStyle.AppLock;
            }
            else
            {
                return Enums.AppTreeEnum.IconStyle.App;
            }
        }

        public void Handle(ChangeUI message)
        {
            switch (message.Type)
            {
                case "AppTreeChange":
                    (ModelClass as SoftwareRef).CheckStyle = GetCheckStyle();
                    (ModelClass as SoftwareRef).IconStyle = GetIconStyle();
                    break;
                case "SoftwareChange":
                    (ModelClass as SoftwareRef).Label = GetSoftwareLabel((ModelClass as SoftwareRef).Id);
                    break;
            }
        }

        public bool Default
        {
            get { return (ModelClass as SoftwareRef).Default; }
            set
            {
                (ModelClass as SoftwareRef).Default = value;
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("AppTreeChange", null));
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
            }
        }

        public bool Required
        {
            get { return (ModelClass as SoftwareRef).Required; }
            set
            {
                (ModelClass as SoftwareRef).Required = value;
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("AppTreeChange", null));
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
            }
        }

        public string Id
        {
            get { return (ModelClass as SoftwareRef).Id; }
            set
            {
                (ModelClass as SoftwareRef).Id = value;
                (ModelClass as SoftwareRef).Label = GetSoftwareLabel(value);
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("AppTreeChange", null));
                eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("PreviewChange", null));
                NotifyOfPropertyChange(() => Id);
            }
        }

        public string AppSearchVisibility
        {
            get
            {
                return AppSearch ? "Visible" : "Collapsed";
            }
        }
        private bool _AppSearch = false;
        public bool AppSearch
        {
            get { return _AppSearch; }
            set
            {
                _AppSearch = value;
                NotifyOfPropertyChange(() => AppSearch);
                NotifyOfPropertyChange(() => AppSearchVisibility);
            }
        }

        public void GetSoftwareList()
        {
            AppSearch = !AppSearch;
        }

        public List<ISoftware> FilteredXMLSoftware
        {
            get
            {
                if(null != SoftwareSearcher)
                {
                    return XMLSoftware.Where(s => s.Label.Contains(SoftwareSearcher)).ToList();
                }
                else
                {
                    return XMLSoftware.ToList();
                }
            }
        }

        private ISoftware _SelectedFilteredXMLSoftware;
        public ISoftware SelectedFilteredXMLSoftware
        {
            get { return _SelectedFilteredXMLSoftware; }
            set
            {
                _SelectedFilteredXMLSoftware = value;
                NotifyOfPropertyChange(() => SelectedFilteredXMLSoftware);
                if(null != value)
                {
                    Id = value.Id;
                }
            }
        }

        private string _SoftwareSearcher;
        public string SoftwareSearcher
        {
            get { return _SoftwareSearcher; }
            set
            {
                _SoftwareSearcher = value;
                NotifyOfPropertyChange(() => FilteredXMLSoftware);
            }
        }

        public List<ISoftware> XMLSoftware
        {
            get
            {
                return svm.XMLSoftware.ToList();
            }
        }

        private string GetSoftwareLabel(string id)
        {
            if(XMLSoftware.Where(s => s.Id == id).Count() > 0)
            {
                return XMLSoftware.Where(s => s.Id == id).FirstOrDefault().Label;
            }
            else
            {
                return "";
            }
        }

        public string Condition
        {
            get { return (ModelClass as SoftwareRef).Condition; }
            set
            {
                (ModelClass as SoftwareRef).Condition = value;
            }
        }
    }
}
