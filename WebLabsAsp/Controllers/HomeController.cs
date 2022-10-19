using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebLabsAsp.Entities;

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
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager)
    {
        _listDemo = new List<ListDemo>
        {
            new ListDemo{ ListItemValue=1, ListItemText="Item 1"},
            new ListDemo{ ListItemValue=2, ListItemText="Item 2"},
            new ListDemo{ ListItemValue=3, ListItemText="Item 3"}
        };

        _userManager = userManager;
    }
    
    [Route("")]
    [Route("Shop")]
    public IActionResult Index()
    {
        ViewData["Text"] = "Lab02";
        ViewData["Lst"] = new SelectList(_listDemo,"ListItemValue","ListItemText");
        return View();
    }
    
    public FileResult GetAvatarFromBytes(byte[] bytesAvatar)
    {
        return File(bytesAvatar, "image/*");
    }

    [HttpGet]
    public async Task<IActionResult> GetAvatar()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return null;
        }

        if (user.Avatar == null) return NotFound();
        FileResult imageUserFile = GetAvatarFromBytes(user.Avatar);
        return imageUserFile;
    }
}