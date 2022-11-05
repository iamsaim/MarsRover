using System.ComponentModel.DataAnnotations;

namespace MarsRover.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? DeletedDate { get; set; }

    }
}
