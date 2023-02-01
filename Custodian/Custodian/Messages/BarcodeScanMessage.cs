using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Custodian.Messages;

public class BarcodeScanMessage : ValueChangedMessage<string>
{
    public BarcodeScanMessage(string value) : base(value)
    {
        
    }
}