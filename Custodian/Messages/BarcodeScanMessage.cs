using CommunityToolkit.Mvvm.Messaging.Messages;
using Custodian.Models;

namespace Custodian.Messages;

public class BarcodeScanMessage : ValueChangedMessage<Barcode>
{
    public BarcodeScanMessage(Barcode value) : base(value)
    {
        
    }
}