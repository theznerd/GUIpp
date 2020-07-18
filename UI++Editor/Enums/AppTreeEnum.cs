using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.Enums
{
    public static class AppTreeEnum
    {
        public enum IconStyle
        {
            Folder = 0,
            FolderLock = 18,
            App = 36,
            AppLock = 54,
            AppAdd = 72,
            AppRemove = 90
        }
        public enum CheckStyle
        {
            Unchecked = 16,
            Checked = 32,
            Indeterminate = 48,
            DisabledUnchecked = 64,
            DisabledChecked = 80,
            DisabledIndeterminate = 96,
            HighlightUnchecked = 112,
            HighlightChcked = 128,
            HighlightIndeterminate = 144
        }
    }
}
