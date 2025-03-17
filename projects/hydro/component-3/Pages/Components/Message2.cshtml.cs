using Hydro;

namespace Component3.Pages.Components;
public class Message2 : HydroComponent
{  
    public string Text { get; set; }   

    public string Text2 { get; set;}

    public override void Mount()
    {
        Text = "From Mount";
    }

    public override void Render()
    {
        Text2 = "From Render";
    }

    public void TriggerRender()
    {
        Text2 = "Render Triggered";
    }
}