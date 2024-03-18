using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class DiciplineDto
    {
        public DiciplineDto() { }

        public DiciplineDto(Dicipline dicipline)
        {
            Id = dicipline.Id;
            Name = dicipline.Name;
            FieldId = dicipline.FieldId;
            IsActive = dicipline.IsActive;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int FieldId { get; set; }

        public bool IsActive { get; set; }
    }
}
