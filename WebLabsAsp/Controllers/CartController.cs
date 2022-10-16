using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLabsAsp.Data;
using WebLabsAsp.Models;

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
    public IActionResult Index()
    {
        return View(_cart.Items.Values);
    }
    
    [Authorize]
    public IActionResult Add(Guid id, string returnUrl)
    {
        var item = _context.Cars.Find(id);
        
        if(item!=null)
            _cart.AddToCart(item);
        
        return Redirect(returnUrl);
    }

    public IActionResult Delete(Guid id)
    {
        _cart.RemoveFromCart(id);
        return RedirectToAction("Index");
    }
}