using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public partial class AccountDicipline
{
    public AccountDicipline() { }

    public AccountDicipline(AccountDiciplineDto accountDiciplineDto) {
        AccountId = accountDiciplineDto.AccountId;
        FieldId = accountDiciplineDto.FieldId;
        DiciplineId = accountDiciplineDto.DiciplineId;
        IsActive = accountDiciplineDto.IsActive;
    }

    [Key]
    public int Id { get; set; }
    
    public int AccountId { get; set; }

    public int FieldId { get; set; }

    public int DiciplineId { get; set; }

    public bool IsActive { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Dicipline Dicipline { get; set; } = null!;

    public virtual Field Field { get; set; } = null!;
}
