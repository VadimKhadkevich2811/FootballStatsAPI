using System.ComponentModel.DataAnnotations;

namespace FootballStats.Domain.Common;

public abstract class BaseEntity
{
    [Required]
    public bool IsDeleted { get; set; }
}