using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos;

public class AccountDto
{
    public AccountDto() { }

    public AccountDto(Account account)
    {
        Id = account.Id;
        Name = account.Name;
        Auth0Id = account.Auth0Id;
        Password = account.Password;
        AccountType = account.AccountType;
        Email = account.Email;
        IsPremium = account.IsPremium;
        IsActive = account.IsActive;
    }
    
    public int Id { get; set; }

    public string Auth0Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public string AccountType { get; set; }

    public string Email { get; set; }

    public bool IsPremium { get; set; }

    public bool IsActive { get; set; }
}