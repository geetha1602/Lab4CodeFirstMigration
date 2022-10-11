using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab4CodeFirstCrudApp
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string EmpName { get; set; }

        
        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }

        public string ImagePath { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

      
    }
}
