using System;
using System.Collections.Generic;

namespace csharpbeltexam.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public List<BrightIdea> BrightIdeas { get; set; }

        public User()
        {
            List<BrightIdea> Weddings = new List<BrightIdea>();
         }
    }
}

// else if (@wedding.Guests.UserId == @ViewBag.User.UserId) 
// {
//     <td><a href="/weddings/@wedding.Id/leave">Un-RSVP</a></td>
// }