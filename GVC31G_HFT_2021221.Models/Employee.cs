using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GVC31G_HFT_2021221.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Manager Manager { get; set; }
        [ForeignKey(nameof(Manager))]
        public int ManagerId { get; set; }
        public virtual ICollection<Assignment> CurrentTask { get; set; }
        public Employee()
        {
            CurrentTask = new HashSet<Assignment>();
        }
    }
}
