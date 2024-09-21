using System.ComponentModel.DataAnnotations;

namespace DemoDatabaseFromScratch.Domains
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StaffId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //Relation one-to-one
        public int OrganizationId { get; set; } //ForeignKey <ParentId PrimaryKey>
        public Organization? Organization { get; set; } //Tak masuk dalam database
    }
}
