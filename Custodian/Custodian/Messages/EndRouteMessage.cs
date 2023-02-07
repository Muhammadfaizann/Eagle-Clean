using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Messages
{
    public class EndRouteMessage : ValueChangedMessage<string>
    {
        public EndRouteMessage(string value) : base(value)
        {

        }
    }
}