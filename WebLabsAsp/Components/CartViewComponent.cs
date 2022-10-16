using Microsoft.AspNetCore.Mvc;
using WebLabsAsp.Models;

namespace WebLabsAsp.Components;

public class CartViewComponent : ViewComponent
{
    private Cart _cart;
    
    // public CartViewComponent(Cart cart)
    // {
    //     _cart = cart;
    // }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}