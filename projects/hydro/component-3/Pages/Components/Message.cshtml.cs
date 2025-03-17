using Hydro;

namespace Component3.Pages.Components;
public class Message : HydroComponent
{  
    public string Text { get; set; }   

    public string Text2 { get; set;}

    public override void Mount()
    {
        Text = "From Mount";
    }

    public override Task MountAsync()
    {
        Text = "From MountAsync";
        return Task.CompletedTask;
    }

    public override void Render()
    {
        Text2 = "From Render";
    }

    public override Task RenderAsync()
    {
        Text2 = "From RenderAsync";
        return Task.CompletedTask;
    }

    public void TriggerRender()
    {
        Text2 = "Render Triggered";
    }
}