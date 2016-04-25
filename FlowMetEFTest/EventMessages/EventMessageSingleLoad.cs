using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMetEFTest.EventMessages
{
    class EventMessageSingleLoad<T>
    {
        public EventMessageSingleLoad(string identifier, T value)
        {
            Identifier = identifier;
            Value = value;
        }

        public string Identifier { get; private set; }
        public T Value { get; private set; }
    }
}
