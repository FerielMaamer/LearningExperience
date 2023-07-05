using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }

        public string Name { get; set; }

        [InverseProperty("Category")]
        public ICollection<IndividualTask>? Tasks { get; set; }
    }
}
