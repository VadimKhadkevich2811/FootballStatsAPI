using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FootballStats.Domain.Common;

namespace FootballStats.Domain.Entities;

public class TrainingPlayer : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int PlayerId { get; set; }
    public virtual Player? Player { get; set; }

    [Required]
    public int TrainingId { get; set; }
    public virtual Training? Training { get; set; }
}