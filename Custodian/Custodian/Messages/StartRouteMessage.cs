using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Custodian.Messages
{
    public class StartRouteMessage : ValueChangedMessage<string>
    {
        public StartRouteMessage(string value) : base(value)
        {

        }
    }
}