using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.ViewModels.Preview
{
    public class InfoFullScreenViewModel : PropertyChangedBase, IPreview
    {
        public IEventAggregator EventAggregator { get; set; }
        public string WindowHeight { get; set; } = "Regular";
        public string Font { get; set; } = "Tahoma";
        public bool HasCustomPreview { get; } = true;
        public bool PreviewRefreshButtonVisible { get { return false; } }
        public bool PreviewAcceptButtonVisible { get { return false; } }
        public bool PreviewCancelButtonVisible { get { return false; } }
        public bool PreviewBackButtonVisible { get { return false; } }

        private string _Image;
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        private string _TextColor;
        public string TextColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;
                NotifyOfPropertyChange(() => TextColor);
            }
        }

        private string _BackgroundColor;
        public string BackgroundColor
        {
            get { return _BackgroundColor; }
            set
            {
                _BackgroundColor = value;
                NotifyOfPropertyChange(() => BackgroundColor);
            }
        }
    }
}
