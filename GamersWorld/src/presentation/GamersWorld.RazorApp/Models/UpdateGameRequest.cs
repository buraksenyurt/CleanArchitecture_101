using System.ComponentModel.DataAnnotations;

namespace GamersWorld.RazorApp.Models;

public class UpdateGameRequest
{
    public int GameId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
    [Display(Name = "Game Title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Status is required.")]
    [Display(Name = "Status")]
    public short Status { get; set; }

    [Required(ErrorMessage = "Point is required.")]
    [Range(0.0, 10.0, ErrorMessage = "Point must be between 0 and 10.")]
    [Display(Name = "Point (0–10)")]
    public double Point { get; set; }

    [Required(ErrorMessage = "List price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "List price must be greater than 0.")]
    [DataType(DataType.Currency)]
    [Display(Name = "List Price")]
    public decimal ListPrice { get; set; }

    [Display(Name = "Archived")]
    public bool IsArchived { get; set; }

    [Required(ErrorMessage = "Image ID is required.")]
    [Display(Name = "Image ID (GUID)")]
    public Guid ImageId { get; set; }
}
