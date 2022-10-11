namespace Lab4CodeFirstWebApp.Models
{
    public class EditImageViewModel : UploadImageViewModel
    {
        public int EmpId { get; set; }
        public string? ExistingImage { get; set; }
    }
}
