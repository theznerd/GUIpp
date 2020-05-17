using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class ChoiceList : IElement, IChoice
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Choice List"; } }
        public string AlternateValueList { get; set; }
        public string OptionList { get; set; }
        public string ValueList { get; set; }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "ChoiceList", null);
            XmlAttribute optionList = d.CreateAttribute("OptionList");
            XmlAttribute valueList = d.CreateAttribute("ValueList");
            XmlAttribute alternateValueList = d.CreateAttribute("AlternateValueList");

            // Set Attribute Values
            optionList.Value = OptionList;
            valueList.Value = ValueList;
            alternateValueList.Value = AlternateValueList;

            // Append Attribute
            if(!string.IsNullOrEmpty(OptionList))
            {
                output.Attributes.Append(optionList);
            }
            if (!string.IsNullOrEmpty(ValueList))
            {
                output.Attributes.Append(valueList);
            }
            if(!string.IsNullOrEmpty(AlternateValueList))
            {
                output.Attributes.Append(alternateValueList);
            }

            return output;
        }
    }
}
