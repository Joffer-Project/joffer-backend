using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos
{
    public class RoleDto
    {
        public RoleDto() { }

        public RoleDto(Role dicipline)
        {
            Id = dicipline.Id;
            Name = dicipline.Name;
            IsActive = dicipline.IsActive;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int FieldId { get; set; }

        public bool IsActive { get; set; }
    }
}
