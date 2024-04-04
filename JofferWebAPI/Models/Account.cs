using System.ComponentModel.DataAnnotations;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Models;

public class Account
{
    public Account()
    {

    }

    public Account(AccountDto accountDto)
    {
        Name = accountDto.Name;
        Auth0Id = accountDto.Auth0Id;
        Password = accountDto.Password;
        AccountType = accountDto.AccountType;
        Email = accountDto.Email;
        IsPremium = accountDto.IsPremium;
        IsActive = accountDto.IsActive;
    }
    
    [Key]
    public int Id { get; set; }

    public string Auth0Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public string Email { get; set; }

    public bool IsPremium { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccountDicipline> AccountDiciplines { get; set; } = new List<AccountDicipline>();

    public virtual ICollection<Talent> Talents { get; set; } = new List<Talent>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<Recruiter> Recruiters { get; set; } = new List<Recruiter>();
}
