using Hydro;

namespace Cookies.Pages.Components;
public class Message : HydroComponent
{  
    public string Text { get; set; }

    public Message()
    {
        Subscribe<MessageUpdatedEvent>(data => Text = data.Message);
    }

    public override void Mount()
    {
        var msg = CookieStorage.Get<string>("date");

        if (!string.IsNullOrWhiteSpace(msg))
            Text = msg;
        else
            Text = "No cookies set";
    }
}