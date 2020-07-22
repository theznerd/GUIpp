using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Models;
using UI__Editor.Interfaces;
using UI__Editor.Models.ActionClasses;
using Caliburn.Micro;
using System.Web.Compilation;
using System.Collections.ObjectModel;
using System.IO;

namespace UI__Editor.Controllers
{
    public static class XMLToClassModel
    {
        private static string ColorConverter(string s)
        {
            if(s.StartsWith("#"))
            { return s; }
            else if (!string.IsNullOrEmpty(s))
            {
                return "#" + s;
            }
            else
            {
                return s;
            }
        }

        public static UIpp GenerateUIpp(XmlDocument xmlDoc, string path)
        {
            UIpp uipp = new UIpp();

            // Strip Comments
            XmlNodeList allComments = xmlDoc.SelectNodes("//comment()");
            foreach(XmlNode c in allComments)
            {
                c.ParentNode.RemoveChild(c);
            }

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
            
            uipp.Color = ColorConverter(uippNode.GetAttribute("Color"));
            
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
                uipp.Flat = false;
            }
            uipp.Icon = uippNode.GetAttribute("Icon");
            if (!string.IsNullOrEmpty(uippNode.GetAttribute("RootXMLPath")))
            {
                uipp.RootXMLPath = uippNode.GetAttribute("RootXMLPath");
            }
            else
            {
                uipp.RootXMLPath = Path.GetDirectoryName(path);
            }
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

        private static string CDATARemover(string s)
        {
            if (s.StartsWith("<![CDATA["))
            {
                s = s.Replace("<![CDATA[", "");
                s = s.Substring(0, s.Length - 3);
            }
            return s;
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
                            /* This is handled by the only valid subelement in AppTree (SoftwareSets)
                            if(ne is IChildElement)
                            {
                                appTree.SubChildren.Add(ne as IChildElement);
                            }
                            */
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
                        if(!string.IsNullOrEmpty(importNode.InnerXml))
                        {
                            externalCall.Content = CDATARemover(importNode.InnerXml);
                        }
                        element = externalCall;
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
                        if (!string.IsNullOrEmpty(importNode.InnerXml))
                        {
                            info.Content = CDATARemover(importNode.InnerXml);
                        }
                        element = info;
                        break;
                    case "InfoFullScreen":
                        InfoFullScreen infoFullScreen = new InfoFullScreen(Globals.EventAggregator)
                        {
                            Image = importNode.GetAttribute("Image")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("BackgroundColor")))
                            infoFullScreen.BackgroundColor = ColorConverter(importNode.GetAttribute("BackgroundColor"));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("TextColor")))
                            infoFullScreen.TextColor = ColorConverter(importNode.GetAttribute("TextColor"));
                        if (!string.IsNullOrEmpty(importNode.InnerXml))
                        {
                            infoFullScreen.Content = CDATARemover(importNode.InnerXml);
                        }
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
                        if (!string.IsNullOrEmpty(importNode.InnerXml))
                        {
                            tsVar.Content = CDATARemover(importNode.InnerXml);
                        }
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
                        ActionGroup actionGroup = new ActionGroup(Globals.EventAggregator)
                        {
                            Name = importNode.GetAttribute("Name"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x);
                            actionGroup.Children.Add(ne);
                        }
                        element = actionGroup;
                        break;
                    case "Case":
                        Case caseClass = new Case(parent)
                        {
                            RegEx = importNode.GetAttribute("RegEx"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("CaseInsensitive")))
                            caseClass.CaseInsensitive = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("CaseInsensitive")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("DontEval")))
                            caseClass.DontEval = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("DontEval")));
                        element = caseClass;
                        break;
                    case "Check":
                        Check check = new Check(parent as Preflight)
                        {
                            CheckCondition = importNode.GetAttribute("CheckCondition"),
                            Description = importNode.GetAttribute("Description"),
                            ErrorDescription = importNode.GetAttribute("ErrorDescription"),
                            Text = importNode.GetAttribute("Text"),
                            WarnCondition = importNode.GetAttribute("WarnCondition"),
                            WarnDescription = importNode.GetAttribute("WarnDescription"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        element = check;
                        break;
                    case "Choice":
                        Choice choice = new Choice(parent)
                        {
                            Option = importNode.GetAttribute("Option"),
                            Value = importNode.GetAttribute("Value"),
                            AlternateValue = importNode.GetAttribute("AlternateValue"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        element = choice;
                        break;
                    case "ChoiceList":
                        ChoiceList choiceList = new ChoiceList(parent)
                        {
                            OptionList = importNode.GetAttribute("OptionList"),
                            ValueList = importNode.GetAttribute("ValueList"),
                            AlternateValueList = importNode.GetAttribute("AlternateValueList"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        element = choiceList;
                        break;
                    case "Field":
                        Field field = new Field()
                        {
                            Name = importNode.GetAttribute("Name"),
                            Hint = importNode.GetAttribute("Hint"),
                            List = importNode.GetAttribute("List"),
                            Prompt = importNode.GetAttribute("Prompt"),
                            Question = importNode.GetAttribute("Question"),
                            RegEx = importNode.GetAttribute("RegEx")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("ReadOnly")))
                            field.ReadOnly = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("ReadOnly")));
                        element = field;
                        break;
                    case "InputCheckbox":
                    case "CheckboxInput":
                        InputCheckbox inputCheckbox = new InputCheckbox(parent as Input)
                        {
                            CheckedValue = importNode.GetAttribute("CheckedValue"),
                            Default = importNode.GetAttribute("Default"),
                            Question = importNode.GetAttribute("Question"),
                            Variable = importNode.GetAttribute("Variable"),
                            UncheckedValue = importNode.GetAttribute("UncheckedValue"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        element = inputCheckbox;
                        break;
                    case "InputChoice":
                    case "ChoiceInput":
                        InputChoice inputChoice = new InputChoice(parent as Input)
                        {
                            AlternateVariable = importNode.GetAttribute("AlternateValue"),
                            Default = importNode.GetAttribute("Default"),
                            Question = importNode.GetAttribute("Question"),
                            Variable = importNode.GetAttribute("Variable"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("AutoComplete")))
                            inputChoice.AutoComplete = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("AutoComplete")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Required")))
                            inputChoice.Required = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Required")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Sort")))
                            inputChoice.Sort = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Sort")));
                        if (int.TryParse(importNode.GetAttribute("DropDownSize"), out int inputChoiceDropDownSize))
                            inputChoice.DropDownSize = inputChoiceDropDownSize;
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, inputChoice);
                            inputChoice.SubChildren.Add(ne as IChildElement);
                        }
                        element = inputChoice;
                        break;
                    case "InputInfo":
                    case "InfoInput":
                        InputInfo inputInfo = new InputInfo(parent as Input)
                        {
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Color")))
                            inputInfo.Color = ColorConverter(importNode.GetAttribute("Color"));
                        if (int.TryParse(importNode.GetAttribute("DropDownSize"), out int inputInfoNumberOfLines))
                            inputInfo.NumberOfLines = inputInfoNumberOfLines;
                        if (!string.IsNullOrEmpty(importNode.InnerXml))
                        {
                            inputInfo.Content = CDATARemover(importNode.InnerXml);
                        }
                        element = inputInfo;
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
                        Match match = new Match(parent as IParentElement)
                        {
                            DisplayName = importNode.GetAttribute("DisplayName"),
                            Variable = importNode.GetAttribute("Variable"),
                            Version = importNode.GetAttribute("Version"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("VersionOperator")))
                            match.VersionOperator = importNode.GetAttribute("VersionOperator");
                        element = match;
                        break;
                    case "Property":
                        Property property = new Property(parent)
                        {
                            Name = importNode.GetAttribute("Name"),
                            Value = importNode.GetAttribute("Value"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Type")))
                            property.Type = importNode.GetAttribute("Type");
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Key")))
                            property.Key = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Key")));
                        element = property;
                        break;
                    case "Set":
                        Set set = new Set(parent)
                        {
                            Name = importNode.GetAttribute("Name"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, set);
                            set.SubChildren.Add(ne as IChildElement);
                        }
                        element = set;
                        break;
                    case "SoftwareGroup":
                        SoftwareGroup softwareGroup = new SoftwareGroup(parent)
                        {
                            Id = importNode.GetAttribute("Id"),
                            Label = importNode.GetAttribute("Label"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Default")))
                            softwareGroup.Default = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Default")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Required")))
                            softwareGroup.Required = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Required")));
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, softwareGroup);
                            softwareGroup.SubChildren.Add(ne as IChildElement);
                        }
                        element = softwareGroup;
                        break;
                    case "SoftwareListRef":
                        SoftwareListRef softwareListRef = new SoftwareListRef(parent as IParentElement)
                        {
                            Id = importNode.GetAttribute("Id"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        element = softwareListRef;
                        break;
                    case "SoftwareRef":
                        SoftwareRef softwareRef = new SoftwareRef(parent)
                        {
                            Id = importNode.GetAttribute("Id"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Hidden")))
                            softwareRef.Hidden = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Hidden")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Default")))
                            softwareRef.Default = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Default")));
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("Required")))
                            softwareRef.Required = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("Required")));
                        element = softwareRef;
                        break;
                    case "SoftwareSets":
                        AppTree at = parent as AppTree;
                        ObservableCollection<IChildElement> e = new ObservableCollection<IChildElement>();
                        foreach (XmlNode x in importNode.ChildNodes)
                        {
                            ne = NewAction(x, at);
                            e.Add(ne as IChildElement);
                        }
                        at.SubChildren = e;
                        element = null;
                        break;
                    case "Text":
                        Text text = new Text(parent as IParentElement)
                        {
                            Type = importNode.GetAttribute("Type"),
                            Value = importNode.GetAttribute("Value"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        element = text;
                        break;
                    case "Variable":
                        Variable variable = new Variable(parent)
                        {
                            Name = importNode.GetAttribute("Name"),
                            Condition = importNode.GetAttribute("Condition")
                        };
                        if (!string.IsNullOrEmpty(importNode.GetAttribute("DontEval")))
                            variable.DontEval = Convert.ToBoolean(StringToBoolString(importNode.GetAttribute("DontEval")));
                        if (!string.IsNullOrEmpty(importNode.InnerXml))
                        {
                            variable.Content = CDATARemover(importNode.InnerXml);
                        }
                        element = variable;
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
                List<Models.Message> xmlMessages = GetMessages(messagesList);
                foreach (Models.Message m in xmlMessages)
                {
                    messages.MessageCollection.Where(x => x.Id == m.Id).FirstOrDefault().Content = m.Content;
                }
            }
            return messages;
        }

        private static List<Models.Message> GetMessages(XmlNodeList messages)
        {
            List<Models.Message> returnMessages = new List<Models.Message>();
            foreach(XmlElement message in messages)
            {
                Models.Message m = new Models.Message()
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
