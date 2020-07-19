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

        private static IElement NewAction(XmlNode xmlNode, IElement parent = null)
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
                            ne = NewAction(x, appTree);
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
                        ErrorInfo errorInfo = new ErrorInfo(Globals.EventAggregator)
                        {
                            Image = importNode.GetAttribute("Image"),
                            InfoImage = importNode.GetAttribute("InfoImage"),
                            Name = importNode.GetAttribute("Name"),
                            Title = importNode.GetAttribute("Title"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("CenterTitle")))
                            errorInfo.CenterTitle = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("CenterTitle")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowBack")))
                            errorInfo.ShowBack = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowBack")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowCancel")))
                            errorInfo.ShowCancel = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowCancel")));
                        element = errorInfo;
                        break;
                    case "ExternalCall":
                        ExternalCall externalCall = new ExternalCall(Globals.EventAggregator)
                        {
                            ExitCodeVariable = importNode.GetAttribute("ExitCodeVariable"),
                            Title = importNode.GetAttribute("Title"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ExitCodeVariable")))
                            externalCall.ExitCodeVariable = importNode.GetAttribute("ExitCodeVariable");
                        element = externalCall;
                        // 
                        // Handle the content tag (CDATA parser)
                        // 
                        break;
                    case "FileRead":
                        FileRead fileRead = new FileRead(Globals.EventAggregator)
                        {
                            FileName = importNode.GetAttribute("Filename"),
                            Variable = importNode.GetAttribute("Variable"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("DeleteLine")))
                            fileRead.DeleteLine = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("DeleteLine")));
                        element = fileRead;
                        break;
                    case "Info":
                        Info info = new Info(Globals.EventAggregator)
                        {
                            Image = importNode.GetAttribute("Image"),
                            InfoImage = importNode.GetAttribute("InfoImage"),
                            Name = importNode.GetAttribute("Name"),
                            Title = importNode.GetAttribute("Title"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowBack")))
                            info.ShowBack = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowBack")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowCancel")))
                            info.ShowCancel = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowCancel")));
                        if (int.TryParse(importNode.GetAttribute("Timeout"), out int infoTimeout))
                            info.Timeout = infoTimeout;
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("TimeoutAction")))
                            info.TimeoutAction = importNode.GetAttribute("TimeoutAction");
                        // 
                        // Handle the content tag (CDATA parser)
                        // 
                        element = info;
                        break;
                    case "InfoFullScreen":
                        InfoFullScreen infoFullScreen = new InfoFullScreen(Globals.EventAggregator)
                        {
                            Image = importNode.GetAttribute("Image")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("BackgroundColor")))
                            infoFullScreen.BackgroundColor = importNode.GetAttribute("BackgroundColor");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("TextColor")))
                            infoFullScreen.TextColor = importNode.GetAttribute("TextColor");
                        // 
                        // Handle the content tag (CDATA parser)
                        // 
                        break;
                    case "Input":
                        Input input = new Input(Globals.EventAggregator)
                        {
                            Name = importNode.GetAttribute("Name"),
                            Title = importNode.GetAttribute("Title"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Size")))
                            input.Size = importNode.GetAttribute("Size");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowBack")))
                            input.ShowBack = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowBack")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowCancel")))
                            input.ShowCancel = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowCancel")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("CenterTitle")))
                            input.CenterTitle = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("CenterTitle")));
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, input);
                            if (ne is IChildElement)
                            {
                                input.SubChildren.Add(ne as IChildElement);
                            }
                        }
                        element = input;
                        break;
                    case "Preflight":
                        Preflight preflight = new Preflight(Globals.EventAggregator)
                        {
                            Title = importNode.GetAttribute("Title"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Size")))
                            preflight.Size = importNode.GetAttribute("Size");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowBack")))
                            preflight.ShowBack = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowBack")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowCancel")))
                            preflight.ShowCancel = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowCancel")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("CenterTitle")))
                            preflight.CenterTitle = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("CenterTitle")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowOnFailureOnly")))
                            preflight.CenterTitle = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowOnFailureOnly")));
                        if (int.TryParse(importNode.GetAttribute("Timeout"), out int preflightTimeout))
                            preflight.Timeout = preflightTimeout;
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("TimeoutAction")))
                            preflight.TimeoutAction = importNode.GetAttribute("TimeoutAction");
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, preflight);
                            if (ne is IChildElement)
                            {
                                preflight.SubChildren.Add(ne as IChildElement);
                            }
                        }
                        element = preflight;
                        break;
                    case "RandomString":
                        RandomString randomString = new RandomString(Globals.EventAggregator)
                        {
                            Variable = importNode.GetAttribute("Variable"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("AllowedChars")))
                            randomString.AllowedChars = importNode.GetAttribute("AllowedChars");
                        if (int.TryParse(importNode.GetAttribute("Length"), out int randomLength))
                            randomString.Length = randomLength;
                        element = randomString;
                        break;
                    case "RegRead":
                        RegRead regRead = new RegRead(Globals.EventAggregator)
                        {
                            Default = importNode.GetAttribute("Default"),
                            Key = importNode.GetAttribute("Key"),
                            Variable = importNode.GetAttribute("Variable"),
                            Value = importNode.GetAttribute("Value"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Reg64")))
                            regRead.Reg64 = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Reg64")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Hive")))
                            regRead.Hive = importNode.GetAttribute("Hive");
                        element = regRead;
                        break;
                    case "RegWrite":
                        RegWrite regWrite = new RegWrite(Globals.EventAggregator)
                        {
                            Key = importNode.GetAttribute("Key"),
                            Value = importNode.GetAttribute("Value"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Reg64")))
                            regWrite.Reg64 = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Reg64")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Type")))
                            regWrite.ValueType = importNode.GetAttribute("Type");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Hive")))
                            regWrite.Hive = importNode.GetAttribute("Hive");
                        element = regWrite;
                        break;
                    case "SaveItems":
                        SaveItems saveItems = new SaveItems(Globals.EventAggregator)
                        {
                            Items = importNode.GetAttribute("Items"),
                            Path = importNode.GetAttribute("Path"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        element = saveItems;
                        break;
                    case "SoftwareDiscovery":
                        SoftwareDiscovery softwareDiscovery = new SoftwareDiscovery(Globals.EventAggregator)
                        {
                            Condition = importNode.GetAttribute("Condition"),
                        };
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, softwareDiscovery);
                            if (ne is IChildElement)
                            {
                                softwareDiscovery.SubChildren.Add(ne as IChildElement);
                            }
                        }
                        element = softwareDiscovery;
                        break;
                    case "Switch":
                        Switch switchClass = new Switch(Globals.EventAggregator)
                        {
                            OnValue = importNode.GetAttribute("OnValue"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("DontEval")))
                            switchClass.DontEval = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("DontEval")));
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, switchClass);
                            if (ne is IChildElement)
                            {
                                switchClass.SubChildren.Add(ne as IChildElement);
                            }
                        }
                        element = switchClass;
                        break;
                    case "TSVar":
                        TSVar tsVar = new TSVar(Globals.EventAggregator)
                        {
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("DontEval")))
                            tsVar.DontEval = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("DontEval")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Name")))
                            tsVar.Variable = importNode.GetAttribute("Name");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Variable")))
                            tsVar.Variable = importNode.GetAttribute("Variable");
                        // 
                        // Handle the content tag (CDATA parser)
                        //
                        element = tsVar;
                        break;
                    case "TSVarList":
                        TSVarList tsVarList = new TSVarList(Globals.EventAggregator)
                        {
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ApplicationVariableBase")))
                            tsVarList.ApplicationVariableBase = importNode.GetAttribute("ApplicationVariableBase");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("PackageVariableBase")))
                            tsVarList.PackageVariableBase = importNode.GetAttribute("PackageVariableBase");
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, tsVarList);
                            if (ne is IChildElement)
                            {
                                tsVarList.SubChildren.Add(ne as IChildElement);
                            }
                        }
                        element = tsVarList;
                        break;
                    case "UserAuth":
                        UserAuth userAuth = new UserAuth(Globals.EventAggregator)
                        {
                            Attributes = importNode.GetAttribute("Attributes"),
                            Domain = importNode.GetAttribute("Domain"),
                            DomainController = importNode.GetAttribute("DomainController"),
                            Group = importNode.GetAttribute("Group"),
                            Title = importNode.GetAttribute("Title")
                        };
                        if (int.TryParse(importNode.GetAttribute("MaxRetryCount"), out int uaMaxRetryCount))
                            userAuth.MaxRetryCount = uaMaxRetryCount;
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("DisableCancel")))
                            userAuth.DisableCancel = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("DisableCancel")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("DoNotFallback")))
                            userAuth.DoNotFallback = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("DoNotFallback")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("GetGroups")))
                            userAuth.GetGroups = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("GetGroups")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ShowBack")))
                            userAuth.ShowBack = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ShowBack")));
                        element = userAuth;
                        break;
                    case "Vars":
                        Vars vars = new Vars(Globals.EventAggregator)
                        {
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Direction")))
                            vars.Direction = importNode.GetAttribute("Direction");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Filename")))
                            vars.Filename = importNode.GetAttribute("Filename");
                        element = vars;
                        break;
                    case "WMIRead":
                        WMIRead wmiRead = new WMIRead(Globals.EventAggregator)
                        {
                            Class = importNode.GetAttribute("Class"),
                            Default = importNode.GetAttribute("Default"),
                            KeyQualifier = importNode.GetAttribute("KeyQualifier"),
                            Property = importNode.GetAttribute("Property"),
                            Query = importNode.GetAttribute("Query"),
                            Variable = importNode.GetAttribute("Variable"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Namespace")))
                            wmiRead.Namespace = importNode.GetAttribute("Namespace");
                        element = wmiRead;
                        break;
                    case "WMIWrite":
                        WMIWrite wmiWrite = new WMIWrite(Globals.EventAggregator)
                        {
                            Class = importNode.GetAttribute("Class"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Namespace")))
                            wmiWrite.Namespace = importNode.GetAttribute("Namespace");
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, wmiWrite);
                            if (ne is IChildElement)
                            {
                                wmiWrite.SubChildren.Add(ne as IChildElement);
                            }
                        }
                        element = wmiWrite;
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
                        InputText inputText = new InputText(parent as Input)
                        {
                            Default = importNode.GetAttribute("Default"),
                            Hint = importNode.GetAttribute("Hint"),
                            Prompt = importNode.GetAttribute("Prompt"),
                            Question = importNode.GetAttribute("Question"),
                            RegEx = importNode.GetAttribute("RegEx"),
                            Variable = importNode.GetAttribute("Variable"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ADValidate")))
                            inputText.ADValidate = importNode.GetAttribute("ADValidate");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ForceCase")))
                            inputText.ForceCase = importNode.GetAttribute("ForceCase");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("HScroll")))
                            inputText.HScroll = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("HScroll")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Password")))
                            inputText.Password = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Password")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ReadOnly")))
                            inputText.ReadOnly = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ReadOnly")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Required")))
                            inputText.Required = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Required")));
                        element = inputText;
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
