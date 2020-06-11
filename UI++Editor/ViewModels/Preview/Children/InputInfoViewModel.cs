using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace UI__Editor.ViewModels.Preview.Children
{
    class InputInfoViewModel : PropertyChangedBase, IChild
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

        private string _Color = "#000000";
        public string Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                NotifyOfPropertyChange(() => Color);
            }
        }

        private int _NumberOfLines = 1;
        public int NumberOfLines
        {
            get { return _NumberOfLines; }
            set
            {
                _NumberOfLines = value;
                NotifyOfPropertyChange(() => NumberOfLines);
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
