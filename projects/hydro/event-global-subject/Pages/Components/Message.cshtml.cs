using Hydro;

namespace Events.Pages.Components;
public class Message : HydroComponent
{  
    public string Text { get; set; }    

    public string Filter { get; set; }

    public Message()
    {
        Subscribe<MessageChangedEvent>(subject: () => Filter , e => Text = e.Message);
    }
}


