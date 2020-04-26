using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Menus
{
    public class LoadSaveViewModel : PropertyChangedBase
    {
        private IEventAggregator _eventAggregator;
        public LoadSaveViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
        }

        public void NewButton()
        {
            _eventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("newFile", null));
        }

        public void SaveButton()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files (*.xml)|*.xml";
            if(sfd.ShowDialog() == true)
            {
                _eventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("saveFile", sfd.FileName));
            }
        }

        public void LoadButton()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";
            if(ofd.ShowDialog() == true)
            {
                _eventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("loadFile", ofd.FileName));
            }
        }
    }
}
