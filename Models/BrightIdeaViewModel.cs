using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharpbeltexam.Models
{
    public class BrightIdeaViewModel : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        // [Required(ErrorMessage="You must enter a name")]
        // public string WedderOne { get; set; }

        // [Required(ErrorMessage="You must enter a name")]
        // public string WedderTwo { get; set; }

        // [Required(ErrorMessage="You must enter a date")]
        // // [DataType(DataType.DateTime)]
        // public DateTime Date { get; set; }

        // [Required(ErrorMessage="You must enter an address")]
        // public string Address { get; set; }

        [Required(ErrorMessage="You must enter an idea")]
        public string Idea { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Guest> Guests { get; set; }
    }
}