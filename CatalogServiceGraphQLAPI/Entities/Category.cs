using System.ComponentModel.DataAnnotations;

namespace CatalogServiceGraphQLAPI.Entities
{
    public class Category
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string ImageURL { get; set; }

    }
}
