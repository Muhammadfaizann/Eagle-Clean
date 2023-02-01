

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Custodian.Messages;

public class LoginMessage : ValueChangedMessage<string>
{
    public LoginMessage(string value) : base(value)
    {
        
    }
}