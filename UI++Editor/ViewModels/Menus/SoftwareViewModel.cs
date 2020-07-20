using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using Caliburn.Micro;
using UI__Editor.Models;
using UI__Editor.Interfaces;

namespace UI__Editor.ViewModels.Menus
{
    public class SoftwareViewModel : PropertyChangedBase
    {
        private UIpp UIpp;
        XmlDocument SynchronizationInfo;
        IEventAggregator eventAggregator;
        SettingsViewModel SV;

        public SoftwareViewModel(UIpp uipp, SettingsViewModel sv)
        {
            UIpp = uipp;
            SV = sv;
            ConfigMgrServer = SV.SettingsCMFQDN + " (" + SV.SettingsSiteCode + ")";
            AvailableSoftwareViewSource = new CollectionViewSource();
            AvailableSoftwareViewSource.Filter += AvailableApplicationsFilter;
            Globals.SoftwareViewModel = this;
            eventAggregator = Globals.EventAggregator;

            RefreshSynchronizationData();
        }

        public void RefreshSynchronizationData()
        {
            // Get Synchronization XML Data
            SynchronizationInfo = new XmlDocument();
            string SIPath = Environment.GetEnvironmentVariable("ProgramData") + "\\(G)UI++\\SynchronizedSoftware.xml";
            if (File.Exists(SIPath))
            {
                SynchronizationInfo.Load(SIPath);
                XmlNodeList applications = SynchronizationInfo.SelectNodes("/SynchronizedSoftware/Applications/Application");
                XmlNodeList packages = SynchronizationInfo.SelectNodes("/SynchronizedSoftware/Packages/Package");
                AvailableApplications = applications.Count;
                AvailablePackages = packages.Count;

                AvailableSoftware = new List<ISoftware>();
                foreach (XmlElement a in applications)
                {
                    Application app = new Application
                    {
                        Name = a.GetAttribute("Name")
                    };
                    AvailableSoftware.Add(app);
                }
                foreach(XmlElement p in packages)
                {
                    XmlNodeList programs = p.ChildNodes;
                    foreach(XmlElement prog in programs)
                    {
                        Package pkg = new Package
                        {
                            PkgId = p.GetAttribute("PkgId"),
                            Name = p.GetAttribute("Name"),
                            ProgramName = prog.GetAttribute("Name")
                        };
                        AvailableSoftware.Add(pkg);
                    }
                }

                AvailableSoftwareViewSource.Source = AvailableSoftware;
                LastSynchronization = File.GetLastWriteTime(SIPath).ToString();
            }
        }

        private CollectionViewSource _AvailableSoftwareViewSource;
        public CollectionViewSource AvailableSoftwareViewSource
        {
            get { return _AvailableSoftwareViewSource; }
            set
            {
                _AvailableSoftwareViewSource = value;
                 NotifyOfPropertyChange(() => AvailableSoftwareViewSource);
            }
        }
        private string _SASearcher;
        public string SASearcher
        {
            get { return _SASearcher; }
            set
            {
                _SASearcher = value;
                if(null != AvailableSoftwareViewSource.View)
                {
                    AvailableSoftwareViewSource.View.Refresh();
                }
                NotifyOfPropertyChange(() => SASearcher);
            }
        }
        public ICollectionView AvailableSoftwareView
        {
            get
            {
                return AvailableSoftwareViewSource.View;
            }
        }

        void AvailableApplicationsFilter(object sender, FilterEventArgs e)
        {
            if(string.IsNullOrEmpty(SASearcher))
            {
                e.Accepted = true;
                return;
            }

            ISoftware s = e.Item as ISoftware;
            e.Accepted = (s.Name.ToUpper().Contains(SASearcher.ToUpper())) ? true : false;
        }

        private List<ISoftware> _AvailableSoftware = new List<ISoftware>();
        public List<ISoftware> AvailableSoftware
        {
            get { return _AvailableSoftware; }
            set
            {
                _AvailableSoftware = value;
                NotifyOfPropertyChange(() => AvailableSoftware);
            }
        }

        private ISoftware _SelectedAvailableSoftware;
        public ISoftware SelectedAvailableSoftware
        {
            get { return _SelectedAvailableSoftware; }
            set
            {
                _SelectedAvailableSoftware = value;
                NotifyOfPropertyChange(() => SelectedAvailableSoftware);
            }
        }

        public void RefreshSoftwareList()
        {
            Software softwareNode = (Software)UIpp.Software;
            if (null != softwareNode)
            {
                XMLSoftware = softwareNode.Softwares;
            }
            else
            {
                softwareNode = new Software()
                {
                    Softwares = new ObservableCollection<ISoftware>()
                };
                XMLSoftware = softwareNode.Softwares;
                UIpp.Software = softwareNode;
            }
        }

        private bool _AddVisibleBool = false;
        public bool AddVisibleBool
        {
            get { return _AddVisibleBool; }
            set
            {
                _AddVisibleBool = value;
                NotifyOfPropertyChange(() => AddVisible);
                NotifyOfPropertyChange(() => AddVisibleBool);
            }
        }
        public string AddVisible
        {
            get
            {
                return AddVisibleBool ? "Visible" : "Collapsed";
            }
        }

        private bool _PickVisibleBool = false;
        public bool PickVisibleBool
        {
            get { return _PickVisibleBool; }
            set
            {
                _PickVisibleBool = value;
                NotifyOfPropertyChange(() => PickVisibleBool);
                NotifyOfPropertyChange(() => PickVisible);
            }
        }

        public string PickVisible
        {
            get
            {
                return PickVisibleBool ? "Visible" : "Collapsed";
            }
        }

        public void SACancel()
        {
            PickVisibleBool = false;
        }

        public void SADoubleClick()
        {
            if(null != SelectedAvailableSoftware)
            {
                SASelect();
            }
        }

        public void SASelect()
        {
            CanToggleType = true;
            AEFunction = "Add";
            if(null != SelectedAvailableSoftware)
            {
                AEType = SelectedAvailableSoftware.Type == "Application";
                AEID = "";
                AELabel = SelectedAvailableSoftware.Name;
                AEIncludeID = "";
                AEPkgID = "";
                AEProgramName = "";
                AEName = "";
                if (SelectedAvailableSoftware.Type == "Application")
                {
                    AEName = (SelectedAvailableSoftware as Application).Name;
                }
                else
                {
                    AEPkgID = (SelectedAvailableSoftware as Package).PkgId;
                    AEProgramName = (SelectedAvailableSoftware as Package).ProgramName;
                }
                GenerateGUID();
                PickVisibleBool = false;
                AddVisibleBool = true;
            }
        }

        public void AECancel()
        {
            AddVisibleBool = false;
            CanToggleType = true;
        }

        public void AddSoftwareManual()
        {
            CanToggleType = true;
            AEFunction = "Add";
            AEType = true;
            AEID = "";
            AEIncludeID = "";
            AELabel = "";
            AEPkgID = "";
            AEProgramName = "";
            AEName = "";
            GenerateGUID();
            AddVisibleBool = true;
        }

        public bool CanAESubmit
        {
            get
            {
                if(AEType)
                {
                    return (!string.IsNullOrEmpty(AEName) && !string.IsNullOrEmpty(AEID) && !string.IsNullOrEmpty(AELabel));
                }
                else
                {
                    return (!string.IsNullOrEmpty(AEPkgID) && !string.IsNullOrEmpty(AEID) && !string.IsNullOrEmpty(AELabel) && !string.IsNullOrEmpty(AEProgramName));
                }
            }
        }

        public void AESubmit()
        {
            if(AEFunction == "Edit")
            {
                SelectedXMLSoftware.Id = AEID;
                SelectedXMLSoftware.IncludeID = AEIncludeID;
                SelectedXMLSoftware.Label = AELabel;
                if (AEType)
                {
                    (SelectedXMLSoftware as Application).Name = AEName;
                }
                else
                {
                    (SelectedXMLSoftware as Package).PkgId = AEPkgID;
                    (SelectedXMLSoftware as Package).ProgramName = AEProgramName;
                }
            }
            else
            {
                if (AEType)
                {
                    Application app = new Application()
                    {
                        Id = AEID,
                        IncludeID = AEIncludeID,
                        Label = AELabel,
                        Name = AEName
                    };
                    XMLSoftware.Add(app);
                }
                else
                {
                    Package pkg = new Package()
                    {
                        Id = AEID,
                        IncludeID = AEIncludeID,
                        Label = AELabel,
                        PkgId = AEPkgID,
                        ProgramName = AEProgramName
                    };
                    XMLSoftware.Add(pkg);
                }
            }
            CanToggleType = true;
            AddVisibleBool = false;
            eventAggregator.BeginPublishOnUIThread(new EventAggregators.ChangeUI("SoftwareChange", null));
        }

        public void AddSoftware()
        {
            PickVisibleBool = true;
        }

        public void RemoveSoftware()
        {
            XMLSoftware.Remove(SelectedXMLSoftware);
        }
        public bool CanRemoveSoftware
        {
            get { return null != SelectedXMLSoftware; }
        }

        private bool _CanToggleType;
        public bool CanToggleType
        {
            get { return _CanToggleType; }
            set
            {
                _CanToggleType = value;
                NotifyOfPropertyChange(() => CanToggleType);
            }
        }

        public void EditSoftware()
        {
            CanToggleType = false;
            AEFunction = "Edit";
            AEType = SelectedXMLSoftware.Type == "Application";
            AEID = SelectedXMLSoftware.Id;
            AELabel = SelectedXMLSoftware.Label;
            AEIncludeID = SelectedXMLSoftware.IncludeID;
            AEPkgID = "";
            AEProgramName = "";
            AEName = "";
            if (SelectedXMLSoftware.Type == "Application")
            {
                AEName = (SelectedXMLSoftware as Application).Name;
            }
            else
            {
                AEPkgID = (SelectedXMLSoftware as Package).PkgId;
                AEProgramName = (SelectedXMLSoftware as Package).ProgramName;
            }
            AddVisibleBool = true;
        }

        public bool CanEditSoftware
        {
            get { return null != SelectedXMLSoftware; }
        }

        public void MoveSoftware(string direction)
        {
            int oldIndex = XMLSoftware.IndexOf(SelectedXMLSoftware);
            int newIndex;
            switch(direction)
            {
                case "top":
                    newIndex = 0;
                    break;
                case "up":
                    newIndex = (oldIndex == 0) ? 0 : oldIndex - 1;
                    break;
                case "down":
                    newIndex = (oldIndex == (XMLSoftware.Count - 1)) ? XMLSoftware.Count - 1 : newIndex = oldIndex + 1;
                    break;
                case "bottom":
                    newIndex = XMLSoftware.Count - 1;
                    break;
                default:
                    newIndex = oldIndex;
                    break;
            }
            XMLSoftware.Move(oldIndex, newIndex);
        }

        public bool CanMoveSoftware
        {
            get { return null != SelectedXMLSoftware; }
        }

        private string _AEFunction = "Edit";
        public string AEFunction
        {
            get { return _AEFunction; }
            set
            {
                _AEFunction = value;
                NotifyOfPropertyChange(() => AEFunction);
            }
        }

        private bool _AEType;
        public bool AEType
        {
            get { return _AEType; }
            set
            {
                _AEType = value;
                NotifyOfPropertyChange(() => AEType);
                NotifyOfPropertyChange(() => AETypePackage);
                NotifyOfPropertyChange(() => AETypeApplication);
                NotifyOfPropertyChange(() => CanAESubmit);
            }
        }

        public void GenerateGUID()
        {
            AEID = Guid.NewGuid().ToString();
        }

        private string _AEID;
        public string AEID
        {
            get { return _AEID; }
            set
            {
                _AEID = value;
                NotifyOfPropertyChange(() => AEID);
                NotifyOfPropertyChange(() => CanAESubmit);
            }
        }

        public string AETypePackage
        {
            get { return AEType ? "Hidden" : "Visible"; }
        }

        public string AETypeApplication
        {
            get{ return AEType ? "Visible" : "Hidden"; }
        }

        private string _AELabel;
        public string AELabel
        {
            get { return _AELabel; }
            set
            {
                _AELabel = value;
                NotifyOfPropertyChange(() => AELabel);
                NotifyOfPropertyChange(() => CanAESubmit);
            }
        }

        private string _AEIncludeID;
        public string AEIncludeID
        {
            get { return _AEIncludeID; }
            set
            {
                _AEIncludeID = value;
                NotifyOfPropertyChange(() => AEIncludeID);
            }
        }

        private string _AEName;
        public string AEName
        {
            get { return _AEName; }
            set
            {
                _AEName = value;
                NotifyOfPropertyChange(() => AEName);
                NotifyOfPropertyChange(() => CanAESubmit);
            }
        }

        private string _AEProgramName;
        public string AEProgramName
        {
            get { return _AEProgramName; }
            set
            {
                _AEProgramName = value;
                NotifyOfPropertyChange(() => AEProgramName);
                NotifyOfPropertyChange(() => CanAESubmit);
            }
        }

        private string _AEPkgID;
        public string AEPkgID
        {
            get { return _AEPkgID; }
            set
            {
                _AEPkgID = value;
                NotifyOfPropertyChange(() => AEPkgID);
                NotifyOfPropertyChange(() => CanAESubmit);
            }
        }

        public void SoftwareDoubleClick()
        {
            if(null != SelectedXMLSoftware)
            {
                EditSoftware();
            }
        }

        private ISoftware _SelectedXMLSoftware;
        public ISoftware SelectedXMLSoftware
        {
            get { return _SelectedXMLSoftware; }
            set
            {
                _SelectedXMLSoftware = value;
                NotifyOfPropertyChange(() => SelectedXMLSoftware);
                NotifyOfPropertyChange(() => CanRemoveSoftware);
                NotifyOfPropertyChange(() => CanMoveSoftware);
                NotifyOfPropertyChange(() => CanEditSoftware);
            }
        }

        private ObservableCollection<ISoftware> _XMLSoftware = new ObservableCollection<ISoftware>();
        public ObservableCollection<ISoftware> XMLSoftware
        {
            get { return _XMLSoftware; }
            set
            {
                _XMLSoftware = value;
                NotifyOfPropertyChange(() => XMLSoftware);
                Globals.SoftwareViewModel = this;
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
