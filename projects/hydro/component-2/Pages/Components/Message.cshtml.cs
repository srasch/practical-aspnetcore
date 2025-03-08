using Hydro;

namespace Component1.Pages.Components;
public class Message : HydroComponent
{  
    public string Text { get; set; }   

    public string Text2 { get; set;}

    public void TriggerRender()
    {
        Text2 = "Render Triggered";
    }
}