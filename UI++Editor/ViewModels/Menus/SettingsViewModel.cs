using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            var dispatcher = Application.Current.Dispatcher;

            Task.Run(() => {
                try
                {
                    XmlDocument softwareList = Controllers.ConfigMgrScanner.GenerateSoftwareList(SettingsCMFQDN, SettingsSiteCode);
                    ProgressText = "Saving updated software list...";
                    if (!Directory.Exists(Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++"))
                    {
                        Directory.CreateDirectory(Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++");
                    }
                    softwareList.Save(Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++\\SynchronizedSoftware.xml");
                }
                catch(UnauthorizedAccessException)
                {
                    dispatcher.BeginInvoke(DispatcherPriority.Render, new System.Action(() => 
                    { 
                        (Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Error!", "Unauthorized access exception.\r\nDo you have the correct permissions for WMI to " + SettingsCMFQDN + "?"); 
                    }));
                    // System.Windows.MessageBox.Show("Unauthorized access exception.\r\nDo you have the correct permissions for WMI to " + SettingsCMFQDN + "?");
                }
                catch(System.Runtime.InteropServices.COMException)
                {
                    dispatcher.BeginInvoke(DispatcherPriority.Render, new System.Action(() =>
                    {
                        (Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Error!", "Cannot connect to " + SettingsCMFQDN + "\r\nCan you ping the server?");
                    }));
                    // System.Windows.MessageBox.Show("Cannot connect to " + SettingsCMFQDN + "\r\nCan you ping the server?");
                }
                catch(Exception ex)
                {
                    dispatcher.BeginInvoke(DispatcherPriority.Render, new System.Action(() =>
                    {
                        (Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Error!", "Unknown Error.\r\n\r\nException Type: " + ex.GetType() + "\r\nMessage: " + ex.Message);
                    }));
                    // System.Windows.MessageBox.Show("Unknown Error.\r\n\r\nException Type: " + ex.GetType() + "\r\nMessage: " + ex.Message);
                }
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
