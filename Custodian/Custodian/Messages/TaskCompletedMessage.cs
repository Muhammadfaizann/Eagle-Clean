using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Messages
{
    internal class TaskCompletedMessage : ValueChangedMessage<string>
    {
        public TaskCompletedMessage(string value) : base(value)
        {

        }
    }
}