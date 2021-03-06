﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Preview
{
    public class _NoPreviewViewModel : IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewBackButtonVisible { get { return false; } }
        public bool PreviewCancelButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }
        public string WindowHeight { get; set; } = "Regular";
        public string Font { get { return Globals.DisplayFont; } }
        public bool HasCustomPreview { get; } = false;
    }
}