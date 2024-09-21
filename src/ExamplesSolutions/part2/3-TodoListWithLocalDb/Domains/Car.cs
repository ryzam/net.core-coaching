using System.ComponentModel.DataAnnotations;

namespace _2_TodoListWithLocalDb.Domains
{
    //Step 5
    public class Car // Table
    {
        public Car()
        {

        }
        [Key] //Tell ORM to configure Id as PrimaryKey
        public int Id { get; set; }

        [Required(ErrorMessage = "Sila masukkan nama Brand")]
        public string Brand { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public int YearOfManufacture { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

    }
}
