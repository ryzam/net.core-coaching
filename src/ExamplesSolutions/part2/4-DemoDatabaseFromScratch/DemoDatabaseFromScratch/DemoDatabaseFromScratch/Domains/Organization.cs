using System.ComponentModel.DataAnnotations;

namespace DemoDatabaseFromScratch.Domains
{
    public class Organization
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama organisasi diperlukan")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string TelephoneNo { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Display(Name="PosKod")]
        public string ZipCode { get; set; }

        //Variable Naming convention untuk colletion perlu ada 's'
        //Relation one-to-many
        public List<Employee> Employees { get; set; } = new();
    }
}
