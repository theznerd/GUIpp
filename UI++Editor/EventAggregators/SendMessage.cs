using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.EventAggregators
{
    public class SendMessage
    {
        public string Type
        {
            get;
            private set;
        }

        public object Data
        {
            get;
            private set;
        }

        public SendMessage(string messageType, object messageData)
        {
            Type = messageType;
            Data = messageData;
        }
    }
}
