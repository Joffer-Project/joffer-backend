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
        Username = accountDto.Username;
        Password = accountDto.Password;
        AccountType = accountDto.AccountType;
        ReachByPhone = accountDto.ReachByPhone;
        ReachByEmail = accountDto.ReachByEmail;
        IsPremium = accountDto.IsPremium;
        IsActive = accountDto.IsActive;
    }
    
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public bool ReachByPhone { get; set; }

    public bool ReachByEmail { get; set; }

    public bool IsPremium { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccountDicipline> AccountDiciplines { get; set; } = new List<AccountDicipline>();

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<Recruiter> Recruiters { get; set; } = new List<Recruiter>();
}
