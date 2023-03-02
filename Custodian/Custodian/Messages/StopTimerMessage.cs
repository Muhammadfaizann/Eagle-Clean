using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Messages
{
    public class StopTimerMessage : ValueChangedMessage<string>
    {
        public StopTimerMessage(string value) : base(value)
        {

        }
    }
}