﻿using System;
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
    public class SoftwareDiscovery : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return true; } }
        public string ActionType { get; } = "Software Discovery";
        public ObservableCollection<Match> SubChildren { get; set; }
        public string Condition { get; set; }

        public SoftwareDiscovery(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.SoftwareDiscoveryViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            // Append Children
            foreach (Match match in SubChildren)
            {
                XmlNode importNode = d.ImportNode(match.GenerateXML(), true);
                output.AppendChild(importNode);
            }

            return output;
        }
    }
}
