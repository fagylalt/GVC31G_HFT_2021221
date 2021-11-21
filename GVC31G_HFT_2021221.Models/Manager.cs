using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVC31G_HFT_2021221.Models
{
    [Table("Managers")]
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]

        public string Name { get; set; }
        public string DepartmentName { get; set; }

        [NotMapped]
        public virtual ICollection<Employee> Employees { get; set; }
        public Manager()
        {
            Employees = new HashSet<Employee>();
        }
    }
}
