using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Preview
{
    public interface IPreview
    {
        IEventAggregator EventAggregator { get; set; }
        bool PreviewRefreshButtonVisible { get; }
        bool PreviewBackButtonVisible { get; }
        bool PreviewCancelButtonVisible { get; }
        bool PreviewAcceptButtonVisible { get; }
        string Font { get; set; }
        bool HasCustomPreview { get; }
        string WindowHeight { get; set; }
    }
}
