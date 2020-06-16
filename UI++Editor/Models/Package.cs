using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UI__Editor.Interfaces;

namespace UI__Editor.Models
{
    public class Package : PropertyChangedBase, IElement, ISoftware
    {
        public ViewModels.Actions.IAction ViewModel { get; set; }
        public IElement Parent { get; set; }
        public bool HasSubChildren { get { return false; } }
        public string ActionType { get { return "Package"; } }
        private string _Id;
        public string Id // required
        {
            get { return _Id; }
            set
            {
                _Id = value;
                NotifyOfPropertyChange(() => Id);
            } 
        } 
        public string IncludeID { get; set; }
        private string _Label;
        public string Label // required
        {
            get { return _Label; }
            set
            {
                _Label = value;
                NotifyOfPropertyChange(() => Label);
            }
        }
        private string _PkgId;
        public string PkgId // required
        {
            get { return _PkgId; }
            set
            {
                _PkgId = value;
                NotifyOfPropertyChange(() => PkgId);
            }
        }
        private string _ProgramName { get; set; }
        public string ProgramName // required
        {
            get { return _ProgramName; }
            set
            {
                _ProgramName = value;
                NotifyOfPropertyChange(() => ProgramName);
            }
        }
        public string Name { get; set; } // not part of UI++ XML, used for Available Software only
        public string Type { get { return this.GetType().Name; } } // not part of UI++ XML, used for Software Blade

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
        public XmlNode GenerateXML()
        {
            // Create XML Node and Attributes
            XmlDocument d = new XmlDocument();
            XmlNode output = d.CreateNode("element", "Package", null);
            XmlAttribute id = d.CreateAttribute("Id");
            XmlAttribute includeID = d.CreateAttribute("IncludeID");
            XmlAttribute label = d.CreateAttribute("Label");
            XmlAttribute pkgId = d.CreateAttribute("PkgID");
            XmlAttribute programName = d.CreateAttribute("ProgramName");

            // Set Attribute Values
            id.Value = Id;
            includeID.Value = IncludeID;
            label.Value = Label;
            pkgId.Value = PkgId;
            programName.Value = ProgramName;

            // Append Attributes to Node
            output.Attributes.Append(id);
            output.Attributes.Append(label);
            output.Attributes.Append(pkgId);
            output.Attributes.Append(programName);
            if(!string.IsNullOrEmpty(IncludeID))
            {
                output.Attributes.Append(includeID);
            }

            return output;
        }
    }
}
