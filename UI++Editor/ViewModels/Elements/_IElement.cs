﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Elements
{
    public interface IElement
    {
        IPreview PreviewViewModel { get; set; } // the associated preview view model
        object ModelClass { get; set; } // the associated model class
    }
}
