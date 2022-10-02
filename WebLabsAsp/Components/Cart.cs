using Microsoft.AspNetCore.Mvc;

namespace WebLabsAsp.Components;

public class Cart : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}