using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase, IHandle<EventAggregators.SendMessage>, IHandle<EventAggregators.XmlUpdater>
    {
        public IEventAggregator _eventAggregator = new EventAggregator();

        Menus.AboutViewModel _aboutViewModel;
        Menus.ActionsViewModel _actionsViewModel;
        Menus.ConfigurationViewModel _configurationViewModel;
        Menus.LoadSaveViewModel _loadSaveViewModel;
        Menus.SettingsViewModel _settingsViewModel;
        Menus.SoftwareViewModel _softwareViewModel;

        public MainWindowViewModel()
        {
            NewXML();
            _aboutViewModel = new Menus.AboutViewModel();
            _actionsViewModel = new Menus.ActionsViewModel(_eventAggregator);
            _configurationViewModel = new Menus.ConfigurationViewModel(_eventAggregator);
            _loadSaveViewModel = new Menus.LoadSaveViewModel(_eventAggregator);
            _settingsViewModel = new Menus.SettingsViewModel();
            _softwareViewModel = new Menus.SoftwareViewModel();
            _eventAggregator.Subscribe(this);
        }

        private MahApps.Metro.Controls.HamburgerMenuGlyphItem _hamburgerMenuItem;
        public MahApps.Metro.Controls.HamburgerMenuGlyphItem HamburgerMenuItem
        {
            get { return _hamburgerMenuItem; }
            set
            {
                _hamburgerMenuItem = value;
                if (value != null)
                {
                    Header = value.Label;
                    switch (value.Tag)
                    {
                        case "_loadSaveViewModel":
                            ContentControl = _loadSaveViewModel;
                            break;
                        case "_actionsViewModel":
                            ContentControl = _actionsViewModel;
                            break;
                        case "_configurationViewModel":
                            ContentControl = _configurationViewModel;
                            break;
                        case "_softwareViewModel":
                            ContentControl = _softwareViewModel;
                            break;
                        default:
                            break;
                    }
                }
                NotifyOfPropertyChange(() => HamburgerMenuItem);
            }
        }

        private MahApps.Metro.Controls.HamburgerMenuGlyphItem _hamburgerMenuOption;
        public MahApps.Metro.Controls.HamburgerMenuGlyphItem HamburgerMenuOption
        {
            get { return _hamburgerMenuOption; }
            set
            {
                _hamburgerMenuOption = value;
                if (value != null)
                {
                    Header = value.Label;
                    switch (value.Tag)
                    {
                        case "_aboutViewModel":
                            ContentControl = _aboutViewModel;
                            break;
                        case "_settingsViewModel":
                            ContentControl = _settingsViewModel;
                            break;
                        default:
                            break;
                    }
                }
                NotifyOfPropertyChange(() => HamburgerMenuOption);
            }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                NotifyOfPropertyChange(() => Header);
            }
        }

        private object _contentControl;
        public object ContentControl
        {
            get { return _contentControl; }
            set
            {
                _contentControl = value;
                NotifyOfPropertyChange(() => ContentControl);
            }
        }

        public void Handle(EventAggregators.SendMessage message)
        {
            switch (message.Type)
            {
                case "newFile":
                    // to add - "Are You Sure"
                    NewXML();
                    break;
                case "loadFile":
                    // to add - "Are You Sure" if changes not saved
                    LoadXML((string)message.Data);
                    break;
                case "saveFile":
                    SaveXML((string)message.Data);
                    break;
                default:
                    break;
            }
        }

        public void Handle(EventAggregators.XmlUpdater x)
        {

        }

        private void NewXML()
        {
            _actionsViewModel = new Menus.ActionsViewModel(_eventAggregator);
            _configurationViewModel = new Menus.ConfigurationViewModel(_eventAggregator);
            _loadedXML = new XmlDocument();
            XmlNode headerNode = _loadedXML.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlNode rootNode = _loadedXML.CreateElement("UIpp");
            _loadedXML.AppendChild(headerNode);
            _loadedXML.AppendChild(rootNode);
        }

        private XmlDocument _loadedXML;
        private void LoadXML(string path)
        {
            _loadedXML = new XmlDocument();
            _loadedXML.Load(path);
        }

        private void SaveXML(string path)
        {
            _loadedXML.Save(path);
        }
    }
}