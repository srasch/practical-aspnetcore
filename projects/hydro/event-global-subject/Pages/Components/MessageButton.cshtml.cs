using System.Security.Cryptography;
using Hydro;

namespace Events.Pages.Components;

public class MessageButton : HydroComponent
{  
    public void Show(string text)
    {
        var r = Random.Shared.GetItems(["left", "right"], 1);
        DispatchGlobal(new MessageChangedEvent(text), subject:r[0]);
    }
}
