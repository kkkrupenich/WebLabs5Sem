using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebLabsAsp.Controllers;

public class ListDemo
{
    public int ListItemValue { get; set; }
    public string ListItemText { get; set; }
}
public class HomeController : Controller
{
    // GET
    private List<ListDemo> _listDemo;
    
    public HomeController()
    {
        _listDemo = new List<ListDemo>
        {
            new ListDemo{ ListItemValue=1, ListItemText="Item 1"},
            new ListDemo{ ListItemValue=2, ListItemText="Item 2"},
            new ListDemo{ ListItemValue=3, ListItemText="Item 3"}
        };
    }
    
    [Route("")]
    [Route("Shop")]
    public IActionResult Index()
    {
        ViewData["Text"] = "Lab02";
        ViewData["Lst"] = new SelectList(_listDemo,"ListItemValue","ListItemText");
        return View();
    }
}