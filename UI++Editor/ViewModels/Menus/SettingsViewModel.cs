using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Menus
{
    public class SettingsViewModel : PropertyChangedBase
    {
        public SettingsViewModel()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Z-Nerd\\(G)UI++");
            SettingsCMFQDN = (string)key.GetValue("CMFQDN", "");
            SettingsSiteCode = (string)key.GetValue("SiteCode", "");
            key.Close();
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
