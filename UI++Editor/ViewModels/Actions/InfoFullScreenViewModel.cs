using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models.ActionClasses;
using UI__Editor.ViewModels.Preview;

namespace UI__Editor.ViewModels.Actions
{
    public class InfoFullScreenViewModel : PropertyChangedBase, IAction
    {
        public IPreview PreviewViewModel { get; set; } = new Preview.InfoFullScreenViewModel();
        public IEventAggregator EventAggregator;
        public object ModelClass { get; set; }
        public string ActionTitle { get { return "Info Full Screen"; } }
        public string HiddenAttributes
        {
            get
            {
                string ha;
                ha = "Background Color: " + BackgroundColor;
                ha += "\r\nText Color: " + TextColor;
                ha += "\r\nImage: " + Image;
                return ha;
            }
        }

        public InfoFullScreenViewModel(InfoFullScreen s)
        {
            ModelClass = s;
            EventAggregator = s.EventAggregator;
            BackgroundColor = s.BackgroundColor;
            TextColor = s.TextColor;
            Image = s.Image;
            Text = s.Content;
        }

        public string BackgroundColor
        {
            get { return (ModelClass as InfoFullScreen).BackgroundColor; }
            set
            {
                (ModelClass as InfoFullScreen).BackgroundColor = value;
                (PreviewViewModel as Preview.InfoFullScreenViewModel).BackgroundColor = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => BackgroundColor);
            }
        }

        public string TextColor
        {
            get { return (ModelClass as InfoFullScreen).TextColor; }
            set
            {
                (ModelClass as InfoFullScreen).TextColor = value;
                (PreviewViewModel as Preview.InfoFullScreenViewModel).TextColor = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => TextColor);
            }
        }

        public string Image
        {
            get { return (ModelClass as InfoFullScreen).Image; }
            set
            {
                (ModelClass as InfoFullScreen).Image = value;
                (PreviewViewModel as Preview.InfoFullScreenViewModel).Image = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("AttributeChange", null));
                NotifyOfPropertyChange(() => Image);
            }
        }

        public string Text
        {
            get { return (ModelClass as InfoFullScreen).Content; }
            set
            {
                (ModelClass as InfoFullScreen).Content = value;
                (PreviewViewModel as Preview.InfoFullScreenViewModel).Text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public string Condition
        {
            get { return (ModelClass as InfoFullScreen).Condition; }
            set
            {
                (ModelClass as InfoFullScreen).Condition = value;
                EventAggregator.BeginPublishOnUIThread(new EventAggregators.SendMessage("ConditionChange", null));
                NotifyOfPropertyChange(() => Condition);
            }
        }
    }
}
