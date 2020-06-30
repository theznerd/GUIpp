using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;
using UI__Editor.ViewModels.Elements;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions.Children
{
    public class SoftwareListRefViewModel : IAction
    {
        public IPreview PreviewViewModel { get; set; }
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Software List Ref"; } }
        public string HiddenAttributes
        {
            get { return ""; }
        }

        public SoftwareListRefViewModel(SoftwareListRef s)
        {
            ModelClass = s;
        }

        public string Id
        {
            get { return (ModelClass as SoftwareListRef).Id; }
            set
            {
                (ModelClass as SoftwareListRef).Id = value;
            }
        }

        public string Condition
        {
            get { return (ModelClass as SoftwareListRef).Condition; }
            set
            {
                (ModelClass as SoftwareListRef).Condition = value;
            }
        }
    }
}
