using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Xml;
using UI__Editor.Classes;
using MahApps.Metro.IconPacks;

namespace UI__Editor.Controllers
{
    public static class ConfigMgrScanner
    {
        public static XmlDocument GenerateSoftwareList(string CMServer, string CMSiteCode)
        {
            // Generate base document
            XmlDocument softwareList = new XmlDocument();
            XmlDeclaration xmlDeclaration = softwareList.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlNode rootNode = softwareList.CreateElement("SynchronizedSoftware");
            softwareList.AppendChild(rootNode);
            softwareList.InsertBefore(xmlDeclaration, rootNode); // add the declaration

            // Scan for packages
            List<CMPackage> packages = GetCMPackages(CMServer, CMSiteCode);

            // Scan for applications
            List<CMApplication> applications = GetCMApplications(CMServer, CMSiteCode);

            // Create Pacakges Node
            XmlNode packagesNode = softwareList.CreateElement("Packages");
            foreach(CMPackage p in packages)
            {
                XmlNode packageNode = softwareList.CreateElement("Package");
                foreach(CMProgram prog in p.Programs)
                {
                    XmlNode progNode = softwareList.CreateElement("Program");
                    XmlAttribute progAttrib = softwareList.CreateAttribute("Name");
                    progAttrib.Value = prog.Name;
                    progNode.Attributes.Append(progAttrib);
                    packageNode.AppendChild(progNode);
                }
                XmlAttribute pname = softwareList.CreateAttribute("Name");
                XmlAttribute pid = softwareList.CreateAttribute("PkgId");
                pname.Value = p.Name;
                pid.Value = p.PkgId;
                packageNode.Attributes.Append(pname);
                packageNode.Attributes.Append(pid);

                packagesNode.AppendChild(packageNode);
            }
            rootNode.AppendChild(packagesNode);

            // Create Applications Node
            XmlNode applicationsNode = softwareList.CreateElement("Applications");
            foreach (CMApplication a in applications)
            {
                XmlNode applicationNode = softwareList.CreateElement("Application");
                XmlAttribute aname = softwareList.CreateAttribute("Name");
                aname.Value = a.Name;
                applicationNode.Attributes.Append(aname);
                applicationsNode.AppendChild(applicationNode);
            }
            rootNode.AppendChild(applicationsNode);

            // Output Document
            return softwareList;
        }

        public static List<CMPackage> GetCMPackages(string CMServer, string CMSiteCode)
        {
            List<CMPackage> packages = new List<CMPackage>();

            // ConfigMgr Server Scope
            ManagementScope scope = new ManagementScope("\\\\" + CMServer + "\\root\\SMS\\site_" + CMSiteCode);

            // Get All Packages
            ObjectQuery query = new ObjectQuery("select PackageID,Name from SMS_Package");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection pkgs = searcher.Get();

            // Get All Programs
            query.QueryString = "select PackageID,ProgramName from SMS_Program";
            searcher.Query = query;
            ManagementObjectCollection progs = searcher.Get();
            List<CMProgram> progsList = new List<CMProgram>();
            foreach(ManagementObject prog in progs)
            {
                CMProgram po = new CMProgram()
                {
                    Name = prog.GetPropertyValue("ProgramName") as string,
                    PkgId = prog.GetPropertyValue("PackageID") as string
                };
                progsList.Add(po);
            }

            // Merge
            foreach(ManagementObject p in pkgs)
            {
                string pid = p.GetPropertyValue("PackageID") as string;
                List<CMProgram> programs = progsList.Where(x => x.PkgId == pid).ToList();

                if(programs.Count > 0)
                {
                    CMPackage pkg = new CMPackage()
                    {
                        Name = p.GetPropertyValue("Name") as string,
                        PkgId = p.GetPropertyValue("PackageID") as string,
                        Programs = programs
                    };
                    packages.Add(pkg);
                }
            }
            return packages;
        }

        public static List<CMApplication> GetCMApplications(string CMServer, string CMSiteCode)
        {
            List<CMApplication> applications = new List<CMApplication>();

            // ConfigMgr Server Scope
            ManagementScope scope = new ManagementScope("\\\\" + CMServer + "\\root\\SMS\\site_" + CMSiteCode);

            // Get All Applications
            ObjectQuery query = new ObjectQuery("select LocalizedDisplayName from SMS_Application");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection apps = searcher.Get();

            foreach(ManagementObject app in apps)
            {
                CMApplication a = new CMApplication()
                {
                    Name = app.GetPropertyValue("LocalizedDisplayName") as string
                };
                applications.Add(a);
            }
            return applications;
        }
    }

    public class CMPackage
    {
        public string Name;
        public string PkgId;
        public List<CMProgram> Programs;
    }

    public class CMApplication
    {
        public string Name;
    }

    public class CMProgram
    {
        public string PkgId;
        public string Name;
    }
}
