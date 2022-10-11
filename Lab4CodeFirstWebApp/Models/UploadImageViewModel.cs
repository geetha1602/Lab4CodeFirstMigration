using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Lab4CodeFirstWebApp.Models
{
    public class UploadImageViewModel
    {
        
        public IFormFile UploadImage { get; set; }
    }
}
