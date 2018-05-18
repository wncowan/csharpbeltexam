using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace csharpbeltexam.Models
{
    public class BrightIdea : BaseEntity
    {
        public int Id { get; set; }
        // public string WedderOne { get; set; }
        // public string WedderTwo { get; set; }

        // [DisplayFormat(DataFormatString = "{0:D}")]
        // public DateTime Date { get; set; }
        // public string Address { get; set; }
        [Required]
        public string Idea { get; set; }
        public User User { get; set; }
        public int CreatorId { get; set; }
        public List<Guest> Guests { get; set; }

        public BrightIdea()
        {
            List<Guest> Guests = new List<Guest>();
        }
    }
}