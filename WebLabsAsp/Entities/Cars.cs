using System.ComponentModel.DataAnnotations;

namespace WebLabsAsp.Entities;

public class Car
{
    public Guid Id { get; set; }
    
    public string Brand { get; set; }
    
    public string Description { get; set; }
    
    public int Price { get; set; }
    
    public string Image { get; set; }
    
    [Display(Name="Category")]
    public CarGroup Group { get; set; }
}

public class CarGroup
{
    public Guid CarGroupId { get; set; }
    
    public string GroupName { get; set; }
}