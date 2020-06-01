using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.Interfaces
{
    public interface IAction : IElement
    {
        string ActionType { get; }
        IEventAggregator EventAggregator { get; set; }
    }
}
