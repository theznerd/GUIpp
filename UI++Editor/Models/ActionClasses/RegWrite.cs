using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;

namespace UI__Editor.Models.ActionClasses
{
    public class RegWrite : PropertyChangedBase, IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "Registry Write";
        public string Hive { get; set; } = "HKLM";
        public string Key { get; set; } // Required
        public bool? Reg64 { get; set; } = true;
        public string ValueType { get; set; } = "REG_SZ";
        public string Value { get; set; } // required
        public string Content { get; set; }
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
        public RegWrite(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ViewModel = new ViewModels.Actions.RegWriteViewModel(this);
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute _default = d.CreateAttribute("Default");
            XmlAttribute hive = d.CreateAttribute("Hive");
            XmlAttribute key = d.CreateAttribute("Key");
            XmlAttribute reg64 = d.CreateAttribute("Reg64");
            XmlAttribute valueType = d.CreateAttribute("ValueType");
            XmlAttribute value = d.CreateAttribute("Value");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            hive.Value = Hive;
            key.Value = Key;
            reg64.Value = Reg64.ToString();
            valueType.Value = ValueType;
            value.Value = Value;
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if (!string.IsNullOrEmpty(Hive))
            {
                output.Attributes.Append(hive);
            }
            output.Attributes.Append(key);
            if(null != Reg64)
            {
                output.Attributes.Append(reg64);
            }
            if (!string.IsNullOrEmpty(ValueType))
            {
                output.Attributes.Append(valueType);
            }
            output.Attributes.Append(value);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            output.InnerText = Content;

            return output;
        }
    }
}
