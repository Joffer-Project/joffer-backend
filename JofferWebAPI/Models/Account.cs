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
        PhoneNumber = accountDto.PhoneNumber;
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

    public string PhoneNumber {  get; set; }

    public bool IsPremium { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Talent> Talents { get; set; } = new List<Talent>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public List<AccountIndustries> AccountIndustries { get; set;}

    public List<AccountRoles> AccountRoles { get; set; }
}
