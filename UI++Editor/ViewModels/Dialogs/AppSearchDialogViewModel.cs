using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.ViewModels.Menus;

namespace UI__Editor.ViewModels.Dialogs
{
    public class AppSearchDialogViewModel : PropertyChangedBase
    {
        private SoftwareViewModel svm;

        public AppSearchDialogViewModel()
        {
            svm = Globals.SoftwareViewModel;
        }

        private string _AffirmativeButtonText;
        public string AffirmativeButtonText
        {
            get { return _AffirmativeButtonText; }
            set
            {
                _AffirmativeButtonText = value;
                NotifyOfPropertyChange(() => AffirmativeButtonText);
            }
        }

        private string _NegativeButtonText;
        public string NegativeButtonText
        {
            get { return _NegativeButtonText; }
            set
            {
                _NegativeButtonText = value;
                NotifyOfPropertyChange(() => NegativeButtonText);
            }
        }


    }
}
