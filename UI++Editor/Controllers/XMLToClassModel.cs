using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Models;
using UI__Editor.Interfaces;
using UI__Editor.Models.ActionClasses;

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

            // Add Messages Node
            XmlNode mNode = xmlDoc.SelectSingleNode("/UIpp/Messages");
            uipp.Messages = GetMessagesNode(mNode);

            // Add Software Node
            XmlNode swNode = xmlDoc.SelectSingleNode("/UIpp/Software");
            uipp.Software = GetSoftwareNode(swNode);

            // Add Actions Node
            XmlNode aNode = xmlDoc.SelectSingleNode("/UIpp/Actions");
            uipp.Actions = GetActionsNode(aNode);

            return uipp;
        }

        // Generate Actions Node
        private static Actions GetActionsNode(XmlNode xmlNode)
        {
            Actions a = new Actions();
            a.actions = new System.Collections.ObjectModel.ObservableCollection<IElement>();
            foreach(XmlNode child in xmlNode.ChildNodes)
            {
                IElement element = NewAction(child);
                if(null != element)
                {
                    a.actions.Add(element);
                }
            }
            return a;
        }

        private static string StringToBoolString(string s)
        {
            if(s.ToLower() == "true")
            {
                return "true";
            }
            else if(s.ToLower() == "false")
            {
                return "false";
            }
            else
            {
                return null;
            }
        }

        private static IElement NewAction(XmlNode xmlNode)
        {
            IElement element = null;
            IElement ne = null; // child elements
            XmlElement importNode = (xmlNode as XmlElement);
            if (xmlNode.Name == "Action")
            {
                switch (importNode.GetAttribute("Type"))
                {
                    case "AppTree":
                        AppTree appTree = new AppTree(Globals.EventAggregator)
                        {
                            ApplicationVariableBase = importNode.GetAttribute("ApplicationVariableBase"),
                            PackageVariableBase = importNode.GetAttribute("PackageVariableBase"),
                            Title = importNode.GetAttribute("Title"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("CenterTitle")))
                            appTree.CenterTitle = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("CenterTitle")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Size")))
                            appTree.Size = importNode.GetAttribute("Size");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Expanded")))
                            appTree.Expanded = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Expanded")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowBack")))
                            appTree.ShowBack = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowBack")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowCancel")))
                            appTree.ShowCancel = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowCancel")));
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x);
                            if(ne is IChildElement)
                            {
                                appTree.SubChildren.Add(ne as IChildElement);
                            }
                        }
                        element = appTree;
                        break;
                    case "DefaultValues":
                        DefaultValues defaultValues = new DefaultValues(Globals.EventAggregator)
                        {
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ValueTypes")))
                        {
                            string[] defaultValueTypes = importNode.GetAttribute("ValueTypes").Split(',');
                            defaultValues.ValueTypeList.Where(x => x.Name == "All").First().IsSelected = false;
                            foreach (string defaultValueType in defaultValueTypes)
                            {
                                if(defaultValues.ValueTypeList.Where(x => x.Name == defaultValueType).Count() > 0)
                                    defaultValues.ValueTypeList.Where(x => x.Name == defaultValueType).First().IsSelected = true;
                            }
                        }
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowProgress")))
                            defaultValues.ShowProgress = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowProgress")));
                        element = defaultValues;
                        break;
                    case "ErrorInfo":
                        break;
                    case "ExternalCall":
                        break;
                    case "FileRead":
                        break;
                    case "Info":
                        break;
                    case "InfoFullScreen":
                        break;
                    case "Input":
                        break;
                    case "Preflight":
                        break;
                    case "RandomString":
                        break;
                    case "RegRead":
                        break;
                    case "RegWrite":
                        break;
                    case "SaveItems":
                        break;
                    case "SoftwareDiscovery":
                        break;
                    case "Switch":
                        break;
                    case "TSVar":
                        break;
                    case "TSVarList":
                        break;
                    case "UserAuth":
                        break;
                    case "Vars":
                        break;
                    case "WMIRead":
                        break;
                    case "WMIWrite":
                        break;
                }
            }
            else
            {
                switch (xmlNode.Name)
                {
                    case "ActionGroup":
                        
                        break;
                    case "Case":
                        break;
                    case "Check":
                        break;
                    case "Choice":
                        break;
                    case "ChoiceList":
                        break;
                    case "Field":
                        break;
                    case "InputCheckbox":
                    case "CheckboxInput":
                        break;
                    case "InputChoice":
                    case "ChoiceInput":
                        break;
                    case "InputInfo":
                    case "InfoInput":
                        break;
                    case "InputText":
                    case "TextInput":
                        break;
                    case "Match":
                        break;
                    case "Property":
                        break;
                    case "Set":
                        break;
                    case "SoftwareGroup":
                        break;
                    case "SoftwareListRef":
                        break;
                    case "SoftwareRef":
                        break;
                    case "SoftwareSets":
                        break;
                    case "Text":
                        break;
                    case "Variable":
                        break;
                }
            }
            return element;
        }

        // Generate Messages Node
        private static Messages GetMessagesNode(XmlNode xmlNode)
        {
            Messages messages = new Messages();
            if(null != xmlNode)
            {
                XmlNodeList messagesList = xmlNode.ChildNodes;
                List<Message> xmlMessages = GetMessages(messagesList);
                foreach (Message m in xmlMessages)
                {
                    messages.MessageCollection.Where(x => x.Id == m.Id).FirstOrDefault().Content = m.Content;
                }
            }
            return messages;
        }

        private static List<Message> GetMessages(XmlNodeList messages)
        {
            List<Message> returnMessages = new List<Message>();
            foreach(XmlElement message in messages)
            {
                Message m = new Message()
                {
                    Id = message.GetAttribute("Id"),
                    Content = message.InnerText
                };
                returnMessages.Add(m);
            }
            return returnMessages;
        }

        // Generate Software Objects
        private static Software GetSoftwareNode(XmlNode xmlNode)
        {
            Software software = new Software();
            if (null != xmlNode)
            {
                XmlNodeList apps = xmlNode.ChildNodes;
                software.Softwares = new System.Collections.ObjectModel.ObservableCollection<ISoftware>();
                List<ISoftware> softwares = GetSoftwares(apps);
                foreach (ISoftware s in softwares)
                {
                    software.Softwares.Add(s);
                }

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
                        PkgId = app.GetAttribute("PkgID"),
                        ProgramName = app.GetAttribute("ProgramName")
                    };
                    applications.Add(p);
                }
            }
            return applications;
        }
    }
}
