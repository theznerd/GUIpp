using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.Interfaces
{
    public interface IParentElement
    {
        ObservableCollection<IChildElement> SubChildren { get; set; }
        string[] ValidChildren { get; set; }
    }
}
