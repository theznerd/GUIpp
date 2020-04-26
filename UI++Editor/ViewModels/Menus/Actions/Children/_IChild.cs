using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace UI__Editor.ViewModels.Menus.Actions.Children
{
    public interface IChild 
    {
        string ID { get; set; }
        bool HasChildren { get; }
        string ChildTitle { get; }
        string XMLOutput();
        ObservableCollection<object> Children { get; set; }
    }
}
