using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models
{
    public class IndividualTask
    {
        [Key]
        public int TaskId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public int Priority { get; set; }

        public int Frequency { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;


        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        
        public User? User { get; set; }

        public int CatId { get; set; }
        [ForeignKey(nameof(CatId))]
        
        public IndividualTask? Category { get; set; }
    }
}
