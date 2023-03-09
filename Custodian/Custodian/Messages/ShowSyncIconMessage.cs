using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Messages
{
    public class ShowSyncIconMessage : ValueChangedMessage<string>
    {
        public ShowSyncIconMessage(string value) : base(value)
        {

        }
    }
}