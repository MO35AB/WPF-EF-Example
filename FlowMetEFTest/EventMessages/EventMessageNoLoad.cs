using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMetEFTest.EventMessages
{
    class EventMessageNoLoad
    {
        public EventMessageNoLoad(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; private set; }        
    }
}
