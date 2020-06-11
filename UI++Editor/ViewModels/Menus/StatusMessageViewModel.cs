using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI__Editor.Models;

namespace UI__Editor.ViewModels.Menus
{
    public class StatusMessageViewModel : PropertyChangedBase
    {
        public class StatusMessage : PropertyChangedBase
        {
            private string _MessageID;
            public string MessageID
            {
                get { return _MessageID; }
                set
                {
                    _MessageID = value;
                    NotifyOfPropertyChange(() => MessageID);
                }
            }
            private string _Message;
            public string Message
            {
                get { return _Message; }
                set
                {
                    _Message = value;
                    NotifyOfPropertyChange(() => Message);
                }
            }
        }

        public UIpp uipp;

        public StatusMessageViewModel(UIpp u)
        {
            uipp = u;
            StatusMessageDataGrid = uipp.Messages.MessageCollection;
        }

        private ObservableCollection<Models.Message> _StatusMessageDataGrid;
        public ObservableCollection<Models.Message> StatusMessageDataGrid
        {
            get { return _StatusMessageDataGrid; }
            set
            {
                _StatusMessageDataGrid = value;
                NotifyOfPropertyChange(() => StatusMessageDataGrid);
            }
        }
    }
}
