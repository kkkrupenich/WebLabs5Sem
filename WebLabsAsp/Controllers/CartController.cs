using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLabsAsp.Data;
using WebLabsAsp.Models;
using WebLabsAsp.Extensions;

namespace WebLabsAsp.Controllers;

public class CartController : Controller
{
    private ApplicationDbContext _context;
    private Cart _cart;
    
    public CartController(ApplicationDbContext context, Cart cart)
    {
        _context = context;
        _cart = cart;
    }
    
    // GET
    [Route("Cart")]
    public IActionResult Index()
    {
        return View(_cart.Items.Values);
    }
    
    [Authorize]
    public IActionResult Add(Guid id, string returnUrl)
    {
        var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
        var car = _context.Cars.Find(id);
        if(car!=null)
        {
            cart.AddToCart(car);
            HttpContext.Session.Set<Cart>("cart",cart);
        }
        return Redirect(returnUrl);
    }

    public IActionResult RemoveOne(Guid id, string returnUrl)
    {
        var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
        var car = _context.Cars.Find(id);
        if(car!=null)
        {
            cart.RemoveOneFromCart(car);
            HttpContext.Session.Set<Cart>("cart",cart);
        }
        return Redirect(returnUrl);
    }

    public IActionResult Delete(Guid id)
    {
        _cart.RemoveFromCart(id);
        return RedirectToAction("Index");
    }

    public IActionResult ClearAll()
    {
        _cart.ClearAll();
        return RedirectToAction("Index");
    }

    public int Count() => _cart.Count;

    public int Price() => _cart.Price;
}