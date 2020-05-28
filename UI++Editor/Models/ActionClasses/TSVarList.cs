using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;

namespace UI__Editor.Models.ActionClasses
{
    public class TSVarList : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return true; } }
        public string ActionType { get; } = "TS Variable List";
        public string ApplicationVariableBase { get; set; }
        public string PackageVariableBase { get; set; }
        public ObservableCollection<ISoftwareRef> SoftwareRefs { get; set; }
        public string Condition { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute applicationVariableBase = d.CreateAttribute("ApplicationVariableBase");
            XmlAttribute packageVariableBase = d.CreateAttribute("PackageVariableBase");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            applicationVariableBase.Value = ApplicationVariableBase;
            packageVariableBase.Value = PackageVariableBase;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if(!string.IsNullOrEmpty(ApplicationVariableBase))
            {
                output.Attributes.Append(applicationVariableBase);
            }
            if(!string.IsNullOrEmpty(PackageVariableBase))
            {
                output.Attributes.Append(packageVariableBase);
            }
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (ISoftwareRef softwareRef in SoftwareRefs)
            {
                XmlNode importNode = d.ImportNode(softwareRef.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
