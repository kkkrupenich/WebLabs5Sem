using Microsoft.AspNetCore.Mvc;
using WebLabsAsp.Data;
using WebLabsAsp.Entities;
using WebLabsAsp.Models;

namespace WebLabsAsp.Controllers;

public class ProductController : Controller
{
    public List<Car> _cars { get; set; }
    List<CarGroup> _carGroups;
    ApplicationDbContext _context;
    int _pageSize;

    public ProductController(ApplicationDbContext context)
    {
        _pageSize = 3;
        _context = context;
    }

    // GET
    [Route("Catalog")]
    [Route("Catalog/Page_{pageNo=1}")]
    public IActionResult Index(Guid group, int pageNo)
    {
        var carFiltered = _context.Cars.Where(c => group == Guid.Empty || c.CarGroupId == group);
        
        ViewData["Groups"] = _context.CarGroups;
        var currentGroup = group != Guid.Empty ? group : Guid.Empty;
        ViewData["CurrentGroup"] = currentGroup;

        var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        
        if (isAjax)
            return PartialView("_ListPartial", ListViewModel<Car>.GetModel(carFiltered, pageNo, _pageSize));
        return View(ListViewModel<Car>.GetModel(carFiltered, pageNo, _pageSize));
    }
}