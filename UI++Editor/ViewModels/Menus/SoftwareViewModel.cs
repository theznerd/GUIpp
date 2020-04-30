using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Caliburn.Micro;
using UI__Editor.Classes;
using UI__Editor.Interfaces;

namespace UI__Editor.ViewModels.Menus
{
    class SoftwareViewModel : PropertyChangedBase
    {
        public UIpp UIpp;
        XmlDocument SynchronizationInfo;
        SettingsViewModel SV;
        Software SoftwareNode;

        public SoftwareViewModel(UIpp uipp, SettingsViewModel sv)
        {
            UIpp = uipp;
            SV = sv;
            ConfigMgrServer = SV.SettingsCMFQDN + " (" + SV.SettingsSiteCode + ")";
            SynchronizationInfo = new XmlDocument();
            string SIPath = Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++\\SynchronizedSoftware.xml";
            if (File.Exists(SIPath))
            {
                SynchronizationInfo.Load(SIPath);
                XmlNodeList applications = SynchronizationInfo.SelectNodes("/SynchronizedSoftware/Applications");
                XmlNodeList packages = SynchronizationInfo.SelectNodes("/SynchronizedSoftware/Packages");
                AvailableApplications = applications.Count;
                AvailablePackages = packages.Count;
                LastSynchronization = File.GetLastWriteTime(SIPath).ToString();
            }
        }

        public void RefreshSoftwareList()
        {
            Software softwareNode = (Software)UIpp.Elements.Where(x => x.RootElementType == "Software").FirstOrDefault();
            if (null != softwareNode)
            {
                XMLSoftware = softwareNode.Softwares;
            }
        }

        private ObservableCollection<ISoftware> _XMLSoftware;
        public ObservableCollection<ISoftware> XMLSoftware
        {
            get { return _XMLSoftware; }
            set
            {
                _XMLSoftware = value;
                NotifyOfPropertyChange(() => XMLSoftware);
            }
        }

        private string _ConfigMgrServer = "(not defined)";
        public string ConfigMgrServer
        {
            get { return _ConfigMgrServer; }
            set
            {
                _ConfigMgrServer = value;
                NotifyOfPropertyChange(() => ConfigMgrServer);
            }
        }

        private string _LastSynchronization = new DateTime(0).ToString();
        public string LastSynchronization
        {
            get { return _LastSynchronization; }
            set
            {
                _LastSynchronization = value;
                NotifyOfPropertyChange(() => LastSynchronization);
            }
        }

        private string _DataFile = Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++\\SynchronizedSoftware.xml";
        public string DataFile
        {
            get { return _DataFile; }
            set
            {
                _DataFile = value;
                NotifyOfPropertyChange(() => DataFile);
            }
        }

        private int _AvailableApplications;
        public int AvailableApplications
        {
            get { return _AvailableApplications; }
            set
            {
                _AvailableApplications = value;
                NotifyOfPropertyChange(() => AvailableApplications);
            }
        }

        private int _AvailablePackages;
        public int AvailablePackages
        {
            get { return _AvailablePackages; }
            set
            {
                _AvailablePackages = value;
                NotifyOfPropertyChange(() => AvailablePackages);
            }
        }


    }
}
