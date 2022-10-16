using Microsoft.AspNetCore.Mvc;
using WebLabsAsp.Models;

namespace WebLabsAsp.Components;

public class MenuViewComponent : ViewComponent
{
    private List<MenuItem> _menuItems = new()
    {
        new MenuItem{ Controller="Home", Action="Index", Text="Lab02"},
        new MenuItem{ Controller="Product", Action="Index", Text="Catalog"},

    };

    private List<MenuItem> _adminMenuItems = new()
    {
        new MenuItem { Controller = "Car", Action = "Index", Text = "Cars" },
        new MenuItem { Controller = "CarGroup", Action = "Index", Text = "CarsGroups" },
        new MenuItem { Controller = "Users", Action = "Index", Text = "Users" },
        new MenuItem { Controller = "Role", Action = "Index", Text = "Role" },
        new MenuItem { IsPage = true, Area = "Admin", Page = "/Index", Text = "Admin" }
    };
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (User != null && User.IsInRole("admin")) return View(_adminMenuItems);
        return View(_menuItems);
    }
}