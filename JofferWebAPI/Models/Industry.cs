using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class Industry
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }
}
