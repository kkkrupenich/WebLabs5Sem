using Microsoft.AspNetCore.Mvc;
using WebLabsAsp.Models;

namespace WebLabsAsp.Components;

public class MenuViewComponent : ViewComponent
{
    private List<MenuItem> _menuItems = new()
    {
        new MenuItem{ Controller="Home", Action="Index", Text="Lab07"},
        new MenuItem{ Controller="Product", Action="Index", Text="Catalog"},

    };
    
    private List<MenuItem> _adminMenuItems = new()
    {
        new MenuItem{ Controller="Home", Action="Index", Text="Lab07"},
        new MenuItem{ Controller="Product", Action="Index", Text="Catalog"},
        new MenuItem{ IsPage = true, Area = "Admin", Page = "/Index", Text = "Admin"}

    };

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var controller = ViewContext.RouteData.Values["Controller"]?.ToString();
        var area = ViewContext.RouteData.Values["Area"]?.ToString();

        var checkList = User.IsInRole("admin") ? _adminMenuItems : _menuItems;
        foreach (var menuI in checkList)
        {
            if (controller != null && controller.Equals(menuI.Controller))
            {
                menuI.Active = "active";
            }
            else if (area != null && area.Equals(menuI.Area))
            {
                menuI.Active = "active";
            }
        }
        
        if (User != null && User.IsInRole("admin")) return View(_adminMenuItems);
        return View(_menuItems);
    }
}