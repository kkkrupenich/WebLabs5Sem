using Microsoft.AspNetCore.Mvc;
using WebLabsAsp.Entities;

namespace WebLabsAsp.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}