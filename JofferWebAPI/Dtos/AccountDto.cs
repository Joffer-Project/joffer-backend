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
        Username = account.Username;
        Password = account.Password;
        AccountType = account.AccountType;
        ReachByPhone = account.ReachByPhone;
        ReachByEmail = account.ReachByEmail;
        IsPremium = account.IsPremium;
        IsActive = account.IsActive;
    }
    
    public int Id { get; set; }

    public string Auth0Id { get; set; }
    public string Name { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string AccountType { get; set; }

    public bool ReachByPhone { get; set; }

    public bool ReachByEmail { get; set; }

    public bool IsPremium { get; set; }

    public bool IsActive { get; set; }
}