using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml;

namespace UI__Editor.ViewModels.Menus
{
    public class SettingsViewModel : PropertyChangedBase
    {
        public SoftwareViewModel svm;

        public SettingsViewModel()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Z-Nerd\\(G)UI++");
            if(null != key)
            {
                SettingsCMFQDN = (string)key.GetValue("CMFQDN", "");
                SettingsSiteCode = (string)key.GetValue("SiteCode", "");
                key.Close();
            }
            else
            {
                SettingsCMFQDN = "";
                SettingsSiteCode = "";
            }
        }

        private string _settingsCMFQDN;
        public string SettingsCMFQDN
        {
            get { return _settingsCMFQDN; }
            set
            {
                _settingsCMFQDN = value;
                NotifyOfPropertyChange(() => SettingsCMFQDN);
            }
        }

        private string _settingsSiteCode;
        public string SettingsSiteCode
        {
            get { return _settingsSiteCode; }
            set
            {
                _settingsSiteCode = value;
                NotifyOfPropertyChange(() => SettingsSiteCode);
            }
        }

        private bool _CurrentlyScanning;
        public bool CurrentlyScanning
        {
            get { return _CurrentlyScanning; }
            set
            {
                _CurrentlyScanning = value;
                NotifyOfPropertyChange(() => CurrentlyScanning);
                NotifyOfPropertyChange(() => CanSettingsScanAll);
                NotifyOfPropertyChange(() => CurrentlyScanningVis);
            }
        }

        public bool CanSettingsScanAll
        {
            get { return !CurrentlyScanning; }
        }

        public string CurrentlyScanningVis
        {
            get { return CurrentlyScanning ? "Visible" : "Collapsed"; }
        }

        private string _ProgressText;
        public string ProgressText
        {
            get { return _ProgressText; }
            set
            {
                _ProgressText = value;
                NotifyOfPropertyChange(() => ProgressText);
            }
        }

        public void SettingsScanAll()
        {
            CurrentlyScanning = true;
            ProgressText = "Scanning " + SettingsCMFQDN + " via WMI...";
            Task.Run(() => {
                XmlDocument softwareList = Controllers.ConfigMgrScanner.GenerateSoftwareList(SettingsCMFQDN, SettingsSiteCode);
                ProgressText = "Saving updated software list...";
                if(!Directory.Exists(Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++"))
                {
                    Directory.CreateDirectory(Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++");
                }
                softwareList.Save(Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++\\SynchronizedSoftware.xml");
                CurrentlyScanning = false;
            });
        }

        public void SaveSettings()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Z-Nerd\\(G)UI++");
            key.SetValue("CMFQDN", SettingsCMFQDN);
            key.SetValue("SiteCode", SettingsSiteCode);
            key.Close();
        }
    }
}
