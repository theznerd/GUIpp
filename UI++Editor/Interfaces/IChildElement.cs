using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.Interfaces
{
    public interface IChildElement : IElement
    {
        string[] ValidChildren { get; set; }
        string[] ValidParents { get; set; }
    }
}
