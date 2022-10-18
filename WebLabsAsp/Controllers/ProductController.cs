using Microsoft.AspNetCore.Mvc;
using WebLabsAsp.Data;
using WebLabsAsp.Entities;
using WebLabsAsp.Extensions;
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
    [Route("Catalog/Page_{page=1}")]
    public IActionResult Index(Guid? group, int page)
    {
        var carFiltered = _context.Cars
            .Where(d => !group.HasValue || d.CarGroupId == group.Value);
        
        ViewData["Groups"] = _context.CarGroups;
        ViewData["CurrentGroup"] = group ?? Guid.Empty;
        
        if (Request.IsAjax())
            return PartialView("_ListPartial", ListViewModel<Car>.GetModel(carFiltered, page, _pageSize));
        return View(ListViewModel<Car>.GetModel(carFiltered, page, _pageSize));
    }
}