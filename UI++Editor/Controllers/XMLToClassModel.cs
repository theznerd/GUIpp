using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Classes;
using UI__Editor.Interfaces;

namespace UI__Editor.Controllers
{
    public static class XMLToClassModel
    {
        public static UIpp GenerateUIpp(XmlDocument xmlDoc)
        {
            UIpp uipp = new UIpp();

            // Set Attributes
            XmlElement uippNode = xmlDoc.DocumentElement;
            if (!string.IsNullOrEmpty(uippNode.GetAttribute("AlwaysOnTop")))
            {
                uipp.AlwaysOnTop = Convert.ToBoolean(uippNode.GetAttribute("AlwaysOnTop"));
            }
            else
            {
                uipp.AlwaysOnTop = true;
            }
            uipp.Color = uippNode.GetAttribute("Color");
            if (!string.IsNullOrEmpty(uippNode.GetAttribute("DialogSidebar")))
            {
                uipp.DialogSidebar = Convert.ToBoolean(uippNode.GetAttribute("DialogSidebar"));
            }
            else
            {
                uipp.DialogSidebar = true;
            }
            if (!string.IsNullOrEmpty(uippNode.GetAttribute("Flat")))
            {
                uipp.Flat = Convert.ToBoolean(uippNode.GetAttribute("Flat"));
            }
            else
            {
                uipp.DialogSidebar = false;
            }
            uipp.Icon = uippNode.GetAttribute("Icon");
            uipp.RootXMLPath = uippNode.GetAttribute("RootXMLPath");
            uipp.Title = uippNode.GetAttribute("Title");

            // Add Software Node
            XmlNode swNode = xmlDoc.SelectSingleNode("/UIpp/Software");
            uipp.Elements.Add(GetSoftwareNode(swNode));

            // Add Actions Node

            return uipp;
        }

        // Generate Software Objects
        private static Software GetSoftwareNode(XmlNode xmlNode)
        {
            Software software = new Software();
            XmlNodeList apps = xmlNode.ChildNodes;
            software.Softwares = new System.Collections.ObjectModel.ObservableCollection<ISoftware>();
            List<ISoftware> softwares = GetSoftwares(apps);
            foreach(ISoftware s in softwares)
            {
                software.Softwares.Add(s);
            }
            return software;
        }

        private static List<ISoftware> GetSoftwares(XmlNodeList apps)
        {
            List<ISoftware> applications = new List<ISoftware>();
            foreach(XmlElement app in apps)
            {
                if(app.Name == "Application")
                {
                    Application a = new Application
                    {
                        Id = app.GetAttribute("Id"),
                        IncludeID = app.GetAttribute("IncludeID"),
                        Label = app.GetAttribute("Label"),
                        Name = app.GetAttribute("Name")
                    };
                    applications.Add(a);
                }
                else if(app.Name == "Package")
                {
                    Package p = new Package()
                    {
                        Id = app.GetAttribute("Id"),
                        IncludeID = app.GetAttribute("IncludeID"),
                        Label = app.GetAttribute("Label"),
                        Name = app.GetAttribute("Name"),
                        PkgId = app.GetAttribute("PkgId"),
                        ProgramName = app.GetAttribute("ProgramName")
                    };
                    applications.Add(p);
                }
            }
            return applications;
        }
    }
}
