using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Menus
{
    public class ConfigurationViewModel : PropertyChangedBase
    {
        private IEventAggregator _eventAggregator;
        public ConfigurationViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
        }

        private string _configTitle;
        public string ConfigTitle
        {
            get { return _configTitle; }
            set
            {
                _configTitle = value;
                _eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("title", value));
                NotifyOfPropertyChange(() => ConfigTitle);
            }
        }

        private string _configRootXMLPath;
        public string ConfigRootXMLPath
        {
            get { return _configRootXMLPath; }
            set
            {
                _configRootXMLPath = value;
                NotifyOfPropertyChange(() => ConfigRootXMLPath);
            }
        }

        private string _configIcon;
        public string ConfigIcon
        {
            get { return _configIcon; }
            set
            {
                _configIcon = value;
                NotifyOfPropertyChange(() => ConfigIcon);
            }
        }

        private string _configColor = "#002147";
        public string ConfigColor
        {
            get { return _configColor; }
            set
            {
                _configColor = value;
                _eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("color", value));
                NotifyOfPropertyChange(() => ConfigColor);
            }
        }

        private bool _configAlwaysOnTop;
        public bool ConfigAlwaysOnTop
        {
            get { return _configAlwaysOnTop; }
            set
            {
                _configAlwaysOnTop = value;
                NotifyOfPropertyChange(() => ConfigAlwaysOnTop);
            }
        }

        private bool _configDialogSideBar = true;
        public bool ConfigDialogSideBar
        {
            get { return _configDialogSideBar; }
            set
            {
                _configDialogSideBar = value;
                _eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("showsidebar", value));
                NotifyOfPropertyChange(() => ConfigDialogSideBar);
            }
        }

        private bool _configFlatView;
        public bool ConfigFlatView
        {
            get { return _configFlatView; }
            set
            {
                _configFlatView = value;
                _eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("flatview", value));
                NotifyOfPropertyChange(() => ConfigFlatView);
            }
        }
    }
}
