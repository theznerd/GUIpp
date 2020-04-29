using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Preview.Children
{
    class TextInputViewModel : PropertyChangedBase, IChild
    {
        private string _Default;
        public string Default
        {
            get { return _Default; }
            set
            {
                _Default = value;
                NotifyOfPropertyChange(() => Default);
            }
        }

        private string _Hint;
        public string Hint
        {
            get { return _Hint; }
            set
            {
                _Hint = value;
                NotifyOfPropertyChange(() => Hint);
            }
        }

        private bool _Password;
        public bool Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        private string _Prompt;
        public string Prompt
        {
            get { return _Prompt; }
            set
            {
                _Prompt = value;
                NotifyOfPropertyChange(() => Prompt);
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
    }
}
