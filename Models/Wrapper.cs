using System;
using System.Collections.Generic;

namespace csharpbeltexam.Models
{
    public class Wrapper : BaseEntity
    {
        public List<User> Users { get; set; }
        public List<BrightIdea> BrightIdeas { get; set; }
        public List<Guest> Guests { get; set; }


        public Wrapper(List<User> users, List<BrightIdea> bright_ideas, List<Guest> guests)
        {
            Users = users;
            BrightIdeas = bright_ideas;
            Guests = guests;
        }
    }
}