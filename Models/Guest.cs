using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharpbeltexam.Models
{
    public class Guest : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public BrightIdea BrightIdea { get; set; }
        public int BrightIdeaId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}