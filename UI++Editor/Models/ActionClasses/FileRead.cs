using System;
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
    public class FileRead : IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public string ActionType { get; } = "FileRead";
        public bool? DeleteLine { get; set; }
        public string FileName { get; set; } // required
        public string Variable { get; set; } // required
        public string Condition { get; set; }

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
            type.Value = ActionType;
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
