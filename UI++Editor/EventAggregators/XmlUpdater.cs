using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.EventAggregators
{
    public class XmlUpdater
    {
        public string Type
        {
            get;
            private set;
        }

        public object Node
        {
            get;
            private set;
        }

        public object Data
        {
            get;
            private set;
        }

        public XmlUpdater(string type, object node, object data)
        {
            Type = type;
            Node = node;
            Data = data;
        }
    }
}
