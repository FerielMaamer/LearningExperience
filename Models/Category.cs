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

        //[InverseProperty(nameof(IndividualTask.Category))]
        public List<IndividualTask>? Tasks { get; set; }
    }
}
