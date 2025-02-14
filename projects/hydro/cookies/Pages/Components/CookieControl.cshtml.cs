using Hydro;
using Microsoft.AspNetCore.Mvc;

namespace Cookies.Pages.Components;
public class CookieControl : HydroComponent
{  
    public bool ShowSetCookies { get; set; }

    /// <summary>
    /// Called when the component is instantiated on the page
    /// </summary>
    public override void Mount()
    {
        var msg = CookieStorage.Get<string>("date");
        ShowSetCookies = string.IsNullOrWhiteSpace(msg);
    }

    public void SetCookie()
    {
        CookieStorage.Set("date", DateTime.Now.ToString());
        Dispatch(new MessageUpdatedEvent("A cookie is now set"), scope: Scope.Global);
        Redirect(Url.Page("/Index")); // Url.Page is a helper method to generate a URL to a Razor page. It starts from /Pages folder.
    }

    public void RemoveCookie()
    {
        CookieStorage.Delete("date");
        Dispatch(new MessageUpdatedEvent("Cookie removed"), scope: Scope.Global);
        Redirect(Url.Page("/Index")); // Url.Page is a helper method to generate a URL to a Razor page. It starts from /Pages folder.
    }
}