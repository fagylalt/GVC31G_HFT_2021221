using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Models
{
    [Table("assignment")]
    public class Assignment
    {
        
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Description { get; set; }
            public DateTime dueDate { get; set; }
            [NotMapped]
            public virtual Employee Employee { get; set; }
            [ForeignKey(nameof(Employee))]
            public int EmployeeId { get; set; }
        
    }
}
