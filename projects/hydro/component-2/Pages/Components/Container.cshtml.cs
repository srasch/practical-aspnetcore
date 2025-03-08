using Hydro;

namespace Component1.Pages.Components;

public class Container : HydroComponent
{  
    public string Text { get; set; } = "Hello, World!";  

    public void Show(string text)
    {
        Text = text;
    }
}