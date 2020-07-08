using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Preview.Children
{
    class InputTextViewModel : PropertyChangedBase, IChild, IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewBackButtonVisible { get { return false; } }
        public bool PreviewCancelButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }
        public bool HasCustomPreview { get { return false; } }
        public string WindowHeight { get; set; }

        private bool _ReadOnly;
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                NotifyOfPropertyChange(() => ReadOnly);
            }
        }

        public string Font
        {
            get { return Globals.DisplayFont; }
        }

        public string PasswordVisibility
        {
            get
            {
                return Password ? "Visible" : "Collapsed";
            }
        }

        public string TextBoxVisibility
        {
            get
            {
                return Password ? "Collapsed" : "Visible";
            }
        }

        public string PromptVisibility
        {
            get
            {
                return string.IsNullOrEmpty(Default) ? "Visible" : "Hidden";
            }
        }

        private string _Default;
        public string Default
        {
            get { return _Default; }
            set
            {
                _Default = value;
                NotifyOfPropertyChange(() => Default);
                NotifyOfPropertyChange(() => PromptVisibility);
            }
        }

        private string _CharCasing;
        public string CharCasing
        {
            get { return _CharCasing; }
            set
            {
                _CharCasing = value;
                NotifyOfPropertyChange(() => CharCasing);
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
