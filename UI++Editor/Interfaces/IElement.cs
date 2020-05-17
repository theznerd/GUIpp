using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UI__Editor.Interfaces
{
    public interface IElement
    {
        string ActionType { get; }
        bool HasSubChildren { get; }
        ViewModels.Actions.IAction ViewModel { get; set; }
        XmlNode GenerateXML();
    }
}
