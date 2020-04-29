using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Menus
{
    class SoftwareViewModel : PropertyChangedBase
    {
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

        private string _DataFile = Environment.GetEnvironmentVariable("ProgramData") + "\\GUI++\\SynchronizedSoftware.xml";
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
