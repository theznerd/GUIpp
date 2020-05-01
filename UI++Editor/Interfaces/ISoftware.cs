using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.Interfaces
{
    public interface ISoftware : IElement
    {
        string Id { get; set; }
        string Label { get; set; }
        string IncludeID { get; set; }
        string Name { get; set; }
        string Type { get; }
    }
}
