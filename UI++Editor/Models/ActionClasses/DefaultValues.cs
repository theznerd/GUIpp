using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;
using Caliburn.Micro;
using Xceed.Wpf.Toolkit.Primitives;

namespace UI__Editor.Models.ActionClasses
{
    public class DefaultValues : PropertyChangedBase, IElement, IAction
    {
        public IEventAggregator EventAggregator { get; set; }
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get; } = "DefaultValues";
        public bool? ShowProgress { get; set; } = true;
        public string Condition { get; set; }

        public class ValueType : PropertyChangedBase
        {
            public string Name { get; set; }
            private bool _IsSelected;
            public bool IsSelected
            {
                get { return _IsSelected; }
                set
                {
                    _IsSelected = value;
                    NotifyOfPropertyChange(() => IsSelected);
                }
            }
        }
        private List<ValueType> _ValueTypeList;
        public List<ValueType> ValueTypeList
        {
            get { return _ValueTypeList; }
            set
            {
                _ValueTypeList = value;
                NotifyOfPropertyChange(() => ValueTypeList);
            }
        }

        public DefaultValues(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ValueTypeList = new List<ValueType>()
            {
                new ValueType(){ Name = "All", IsSelected = true },
                new ValueType(){ Name = "Asset" },
                new ValueType(){ Name = "Domain" },
                new ValueType(){ Name = "Mgmt" },
                new ValueType(){ Name = "Net" },
                new ValueType(){ Name = "OS" },
                new ValueType(){ Name = "Security" },
                new ValueType(){ Name = "User" },
                new ValueType(){ Name = "VM" }
            };
            ViewModel = new ViewModels.Actions.DefaultValuesViewModel(this);
        }

        public string GenerateValueTypes()
        {
            string output;
            if ((ValueTypeList.Where(vt => vt.Name == "All").First()).IsSelected || ValueTypeList.Where(vt => vt.IsSelected).Count() == 0)
            {
                output = "All";
            }
            else
            {
                output = "";
                foreach (ValueType vt in ValueTypeList.Where(vt => vt.IsSelected))
                {
                    output += vt.Name + ",";
                }
                output = output.TrimEnd(',');
            }
            return output;
        }

        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Action", null);
            XmlAttribute type = d.CreateAttribute("Type");
            XmlAttribute showProgress = d.CreateAttribute("ShowProgress");
            XmlAttribute valueTypes = d.CreateAttribute("ValueTypes");
            XmlAttribute condition = d.CreateAttribute("Condition");

            // Assign attribute values
            type.Value = ActionType;
            showProgress.Value = ShowProgress.ToString();
            valueTypes.Value = GenerateValueTypes();
            condition.Value = Condition;

            // Append Attributes
            output.Attributes.Append(type);
            if(null != ShowProgress)
            {
                output.Attributes.Append(showProgress);
            }
            output.Attributes.Append(valueTypes);
            if (!string.IsNullOrEmpty(Condition))
            {
                output.Attributes.Append(condition);
            }

            return output;
        }
    }
}
