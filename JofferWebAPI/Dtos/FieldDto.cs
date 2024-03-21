using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class FieldDto
    {
        public FieldDto() { }

        public FieldDto(Field field)
        {
            Id = field.Id;
            Name = field.Name;
            IsActive = field.IsActive;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
