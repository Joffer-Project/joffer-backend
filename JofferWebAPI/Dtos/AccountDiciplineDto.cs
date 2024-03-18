using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class AccountDiciplineDto
    {
        public AccountDiciplineDto() { }

        public AccountDiciplineDto(AccountDicipline accountDicipline)
        {
            Id = accountDicipline.Id;
            AccountId = accountDicipline.AccountId;
            FieldId = accountDicipline.FieldId;
            DiciplineId = accountDicipline.DiciplineId;
            IsActive = accountDicipline.IsActive;
        }

        public int Id { get; set; }

        public int AccountId { get; set; }

        public int FieldId { get; set; }

        public int DiciplineId { get; set; }

        public bool IsActive { get; set; }
    }
}
