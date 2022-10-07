using System.ComponentModel.DataAnnotations;

namespace WebLabsAsp.Entities;

public class Car
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "{0} is required")]
    public string Brand { get; set; }
    
    [Required(ErrorMessage = "{0} is required")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "{0} is required")]
    public int Price { get; set; }
    public string Image { get; set; }
    
    public string Group { get; set; }
    
    public Guid CarGroupId { get; set; }
    
    [Required(ErrorMessage = "{0} is required")]
    [Display(Name="Category")]
    public virtual CarGroup CarGroup { get; set; }
}

public class CarGroup
{
    public Guid CarGroupId { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string GroupName { get; set; }
}