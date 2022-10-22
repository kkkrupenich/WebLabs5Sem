using System.ComponentModel.DataAnnotations;

namespace WebLabsAsp.Blazor.Client.Models;

public class CounterModel
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Choose value from interval: [0 - 2147483647].")]
    public int customCount { get; set; }
}