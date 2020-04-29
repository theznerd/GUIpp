using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Preview.Children
{
    class CheckBoxInputViewModel : PropertyChangedBase, IChild
    {
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

        private string _Content;
        public string Content
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
