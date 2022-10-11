using Lab4CodeFirstCrudApp;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4CodeFirstWebApp.Models
{
    public class EmployeeViewModel: EditImageViewModel
    {
        [Required]
        
        public string EmpName { get; set; }

        [Required]
        public string Address { get; set; }


        [Required]
        public decimal Salary { get; set; }


        [Required]
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem>? Department { get; set; }
    }
}
