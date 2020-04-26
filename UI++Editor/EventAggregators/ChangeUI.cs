using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.EventAggregators
{
    public class ChangeUI
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

        public ChangeUI(string changeType, object changeData)
        {
            Type = changeType;
            Data = changeData;
        }
    }
}
