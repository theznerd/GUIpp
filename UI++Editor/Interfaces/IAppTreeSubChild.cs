using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.Interfaces
{
    public interface IAppTreeSubChild
    {
        bool Default { get; set; }
        bool Required { get; set; }
        IElement Parent { get; set; }
    }
}
