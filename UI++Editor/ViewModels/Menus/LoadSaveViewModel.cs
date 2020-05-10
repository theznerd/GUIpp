using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

        public async void NewButton()
        {
            MetroWindow appWindow = (System.Windows.Application.Current.MainWindow as MetroWindow);
            MessageDialogResult result = await appWindow.ShowMessageAsync("Confirm", "Are you sure you want to create a new file?", MessageDialogStyle.AffirmativeAndNegative);
            if (result.ToString() == "Affirmative")
            {
                _eventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("newFile", null));
            }
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

        public async void LoadButton()
        {
            MetroWindow appWindow = (System.Windows.Application.Current.MainWindow as MetroWindow);
            MessageDialogResult result = await appWindow.ShowMessageAsync("Confirm", "You may have changes which have not been saved.\r\nAre you sure you want to load a new file?", MessageDialogStyle.AffirmativeAndNegative);
            if (result.ToString() == "Affirmative")
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "XML Files (*.xml)|*.xml";
                if (ofd.ShowDialog() == true)
                {
                    _eventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("loadFile", ofd.FileName));
                }
            }
        }
    }
}
