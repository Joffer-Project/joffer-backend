namespace JofferWebAPI.Models;

public partial class AccountDicipline
{
    public int AccountId { get; set; }

    public int FieldId { get; set; }

    public int DiciplineId { get; set; }

    public bool IsActive { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Dicipline Dicipline { get; set; } = null!;

    public virtual Field Field { get; set; } = null!;
}
