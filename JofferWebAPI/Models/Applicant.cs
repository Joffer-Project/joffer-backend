namespace JofferWebAPI.Models;

public partial class Applicant
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string AboutMe { get; set; } = null!;

    public int SalaryMinimum { get; set; }

    public string EmploymentStatus { get; set; } = null!;

    public byte[]? Avatar { get; set; }

    public bool IsActive { get; set; }

    public virtual Account Account { get; set; } = null!;
}
