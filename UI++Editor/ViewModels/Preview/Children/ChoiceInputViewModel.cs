using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Preview.Children
{
    class ChoiceInputViewModel : PropertyChangedBase, IChild
    {
        private string _Font;
        public string Font
        {
            get { return _Font; }
            set
            {
                _Font = value;
                NotifyOfPropertyChange(() => Font);
            }
        }

        private string _Question;
        public string Question
        {
            get { return _Question; }
            set
            {
                _Question = value;
                NotifyOfPropertyChange(() => Question);
            }
        }

        private ObservableCollection<string> _Content;
        public ObservableCollection<string> Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                NotifyOfPropertyChange(() => Content);
            }
        }
    }
}
