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
    }
}
