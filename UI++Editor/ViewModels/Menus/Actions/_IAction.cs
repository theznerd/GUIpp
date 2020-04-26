using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UI__Editor.ViewModels.Menus.Actions
{
    public interface IAction
    {
        // Back End Data
        string ID { get; set; }
        bool HasChildren { get; }
        string ActionTitle { get; }
        object PreviewViewModel { get; }

        string XMLOutput();
        ObservableCollection<object> Children { get; set; }
    }
}
