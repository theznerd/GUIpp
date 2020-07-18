using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.Controllers;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace UI__Editor.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase, IHandle<EventAggregators.SendMessage>
    {
        public IEventAggregator _eventAggregator = new EventAggregator();
        public UIpp uipp;

        Menus.AboutViewModel _aboutViewModel;
        Menus.ActionsViewModel _actionsViewModel;
        Menus.ConfigurationViewModel _configurationViewModel;
        Menus.LoadSaveViewModel _loadSaveViewModel;
        Menus.SettingsViewModel _settingsViewModel;
        Menus.SoftwareViewModel _softwareViewModel;
        Menus.StatusMessageViewModel _statusMessageViewModel;

        public MainWindowViewModel()
        {
            _aboutViewModel = new Menus.AboutViewModel();
            _loadSaveViewModel = new Menus.LoadSaveViewModel(_eventAggregator);
            NewXML();
            _eventAggregator.Subscribe(this);
            Globals.MainWindowViewModel = this;
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
                            _softwareViewModel.RefreshSynchronizationData();
                            break;
                        case "_statusMessageViewModel":
                            ContentControl = _statusMessageViewModel;
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
                    NewXML();
                    break;
                case "loadFile":
                    LoadXML((string)message.Data);
                    break;
                case "saveFile":
                    SaveXML((string)message.Data);
                    break;
                default:
                    break;
            }
        }

        private void NewXML()
        {
            uipp = new UIpp();
            _actionsViewModel = new Menus.ActionsViewModel(_eventAggregator, uipp);
            _settingsViewModel = new Menus.SettingsViewModel();
            _configurationViewModel = new Menus.ConfigurationViewModel(_eventAggregator, uipp);
            _softwareViewModel = new Menus.SoftwareViewModel(uipp, _settingsViewModel);
            _statusMessageViewModel = new Menus.StatusMessageViewModel(uipp);
            _settingsViewModel.svm = _softwareViewModel;
            _configurationViewModel.RefreshConfiguration();
            _softwareViewModel.RefreshSoftwareList();
        }

        private void LoadXML(string path)
        {
            // Load XML
            XmlDocument load = new XmlDocument();
            load.Load(path);

            // Convert XML to UIpp
            uipp = XMLToClassModel.GenerateUIpp(load);

            // Reload Children
            _actionsViewModel = new Menus.ActionsViewModel(_eventAggregator, uipp);
            _configurationViewModel = new Menus.ConfigurationViewModel(_eventAggregator, uipp);
            _softwareViewModel = new Menus.SoftwareViewModel(uipp, _settingsViewModel);
            Globals.SoftwareViewModel = _softwareViewModel;
            _statusMessageViewModel = new Menus.StatusMessageViewModel(uipp);
            _softwareViewModel.RefreshSoftwareList();
            _configurationViewModel.RefreshConfiguration();
        }

        private void SaveXML(string path)
        {
            // Convert UIpp to XML
            var xml = new XmlDocument();
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlNode rootNode = uipp.GenerateXML();
            XmlNode importNode = xml.ImportNode(rootNode, true);
            xml.AppendChild(importNode); // add UIpp
            xml.InsertBefore(dec, importNode); // add the declaration
            xml.Save(path);
        }
    }
}