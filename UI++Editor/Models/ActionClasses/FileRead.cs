﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;

namespace UI__Editor.Models.ActionClasses
{
    public class FileRead : PropertyChangedBase, IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "File Read";
        public bool? DeleteLine { get; set; } = true;
        public string FileName { get; set; } // required
        public string Variable { get; set; } // required
        public string Condition { get; set; }

        // Code to handle TreeView Selection
        private bool _TVSelected = false;
        public bool TVSelected
        {
            get { return _TVSelected; }
            set
            {
                _TVSelected = value;
                NotifyOfPropertyChange(() => TVSelected);
            }
        }

        private bool _TVIsExpanded = true;
        public bool TVIsExpanded
        {
            get { return _TVIsExpanded; }
            set
            {
                _TVIsExpanded = value;
                NotifyOfPropertyChange(() => TVIsExpanded);
            }
        }
        public FileRead(IEventAggregator ea)
        {
            EventAggregator = ea;
            ViewModel = new ViewModels.Actions.FileReadViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute deleteLine = d.CreateAttribute("DeleteLine");
            XmlAttribute fileName = d.CreateAttribute("FileName");
            XmlAttribute variable = d.CreateAttribute("Variable");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = "FileRead";
            deleteLine.Value = DeleteLine.ToString();
            fileName.Value = FileName;
            variable.Value = Variable;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if(null != DeleteLine)
            {
                output.Attributes.Append(deleteLine);
            }
            output.Attributes.Append(fileName);
            output.Attributes.Append(variable);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
