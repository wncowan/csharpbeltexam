using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using csharpbeltexam.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace csharpbeltexam.Controllers
{
    public class HomeController : Controller
    {

        private BrightIdeaContext _context;

        public HomeController(BrightIdeaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User CheckUser = _context.Users.SingleOrDefault(u => u.Email == model.Email);
                Console.WriteLine(CheckUser);
                if(CheckUser != null)
                {
                    TempData["EmailInUseError"] = "Email Aleady in use";
                    return RedirectToAction("Index");
                }
                User newUser = new User
                {
                    FirstName = model.FirstName,
                    Alias = model.Alias,
                    Email = model.Email,
                    Created_At = DateTime.Now,
                    Updated_At = DateTime.Now
                };
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.Password = hasher.HashPassword(newUser, model.Password);
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("currentUserId", newUser.UserId);
                HttpContext.Session.SetString("currentFirstName", newUser.FirstName);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(string email, string password)
        {
            User logUser = _context.Users.SingleOrDefault(user => user.Email == email);
            if (logUser == null)
            {
                TempData["EmailError"] = "Invalid email!";
                return RedirectToAction("Index");
            }
            else
            {
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                if (hasher.VerifyHashedPassword(logUser, logUser.Password, password) != 0)
                {
                    HttpContext.Session.SetInt32("currentUserId", logUser.UserId);
                    HttpContext.Session.SetString("currentUserEmail", logUser.Email);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["PasswordError"] = "Invalid password";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        [Route("/dashboard")]
        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("currentUserId");
            if (userId == null)
            {
                TempData["UserError"] = "You must be logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)userId);
                List<User> users = _context.Users.Include(u => u.BrightIdeas).ToList();
                List<BrightIdea> bright_ideas = _context.BrightIdeas.Include(w => w.Guests).Include(w => w.User).OrderByDescending(w => w.Guests.Count()).ToList();
                //maybe an error but has no effect
                List<Guest> guests = _context.Guests.Include(g => g.BrightIdea).ThenInclude(w => w.User).ToList();
       

                ViewBag.User = currentUser;
                ViewBag.BrightIdeas = bright_ideas;
                
                Wrapper model = new Wrapper(users, bright_ideas, guests);
                return View(model);
            }
        }

        [HttpGet]
        [Route("/bright_ideas/create")]
        public IActionResult NewBrightIdea()
        {
            int? userId = HttpContext.Session.GetInt32("currentUserId");
            if (userId == null)
            {
                TempData["UserError"] = "You must be logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)userId);
                ViewBag.User = currentUser;
                return View();
            }
        }

        [HttpPost]
        [Route("/add_bright_idea")]
        public IActionResult CreateBrightIdea(BrightIdeaViewModel model)
        {
            int? userId = HttpContext.Session.GetInt32("currentUserId");
            if (userId == null)
            {
                TempData["UserError"] = "You must be logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)userId);
                ViewBag.User = currentUser;
                if (ModelState.IsValid)
                {
                    // Wedding newWedding = new Wedding
                    // {
                    //     WedderOne = model.WedderOne,
                    //     WedderTwo = model.WedderTwo,
                    //     Address = model.Address,
                    //     CreatorId = currentUser.UserId,
                    //     User = currentUser
                    // };
                    BrightIdea NewBrightIdea = new BrightIdea
                    {
                        Idea = model.Idea,
                        CreatorId = currentUser.UserId,
                        User = currentUser
                    };
                    // if (model.Date < DateTime.Now)
                    // {
                    //     TempData["DateError"] = "Dates cannot be in the past";
                    //     return RedirectToAction("NewWedding");
                    // }
                    // else 
                    // {
                        // newWedding.Date = model.Date;
                        _context.Add(NewBrightIdea);
                        _context.SaveChanges();
                        return RedirectToAction("Dashboard");
                    // }
                }

                TempData["IdeaError"] = "You must enter an idea!";
                return RedirectToAction("Dashboard");
            }   
        }

        [HttpGet]
        [Route("/bright_ideas/{id}")]
        public IActionResult BrightIdea(int id)
        {
            int? userId = HttpContext.Session.GetInt32("currentUserId");
            if (userId == null)
            {
                TempData["UserError"] = "You must be logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)userId);
                ViewBag.User = currentUser;

                BrightIdea thisBrightIdea = _context.BrightIdeas.Include(w => w.User).SingleOrDefault(w => w.Id == (int)id);
                List<Guest> guests = _context.Guests.Where(g => g.BrightIdeaId == (int)id).Include(g => g.User).ToList();
                // List<User> allUsers = _context.Users.ToList();

                // ViewBag.Users = allUsers;
                ViewBag.BrightIdea = thisBrightIdea;
                ViewBag.Guests = guests;
                return View();
            }
        }



        [HttpGet]
        [Route("/bright_ideas/{id}/like")]
        public IActionResult RSVP(int id)
        {
            int? userId = HttpContext.Session.GetInt32("currentUserId");
            if (userId == null)
            {
                TempData["UserError"] = "You must be logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)userId);
                ViewBag.User = currentUser;
                BrightIdea thisBrightIdea = _context.BrightIdeas.SingleOrDefault(w => w.Id == (int)id);

                Guest newGuest = new Guest
                {
                    UserId = currentUser.UserId,
                    BrightIdeaId = thisBrightIdea.Id,
                    BrightIdea = thisBrightIdea,
                    User = currentUser
                };
                _context.Add(newGuest);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }

// @if(wedding.CreatorId == @ViewBag.User.UserId)
//             {
//                 <td><a href="/delete">Delete</a></td>
//             }
//             else
//             {
//                 <td><a href="/weddings/@wedding.Id/rsvp">RSVP</a></td>
//             }

        [HttpGet]
        [Route("/bright_ideas/{id}/unlike")]
        public IActionResult Leave(int id)
        {
            int? userId = HttpContext.Session.GetInt32("currentUserId");
            if (userId == null)
            {
                TempData["UserError"] = "You must be logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)userId);
                ViewBag.User = currentUser;
                // Wedding thisWedding = _context.Weddings.SingleOrDefault(w => w.Id == (int)weddingId);
                // Guest thisGuest = _context.Guests.SingleOrDefault(g => g.Id == (int)guestId);
                Guest thisGuest = _context.Guests.Where(g => g.UserId == (int)userId).Where(g => g.BrightIdeaId == id).SingleOrDefault();
                

                _context.Remove(thisGuest);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        [Route("/bright_ideas/{id}/delete")]
        public IActionResult Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("currentUserId");
            if (userId == null)
            {
                TempData["UserError"] = "You must be logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)userId);
                ViewBag.User = currentUser;
                BrightIdea thisBrightIdea = _context.BrightIdeas.SingleOrDefault(w => w.Id == (int)id);
                _context.Remove(thisBrightIdea);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        [Route("/users/{id}")]
        public IActionResult ShowUser(int id)
        {
            int? userId = HttpContext.Session.GetInt32("currentUserId");
            if (userId == null)
            {
                TempData["UserError"] = "You must be logged in!";
                return RedirectToAction("Index");
            }
            else
            {
                // User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)userId);
                User showUser = _context.Users.SingleOrDefault(u => u.UserId == id);
                // ViewBag.User = currentUser;
                ViewBag.ShowUser = showUser;

                List<BrightIdea> bright_ideas = _context.BrightIdeas.Where(w => w.CreatorId == id).ToList();
                List<Guest> guests = _context.Guests.Where(g => g.UserId == id).Include(g => g.User).ToList();


                //Wedding thisWedding = _context.Weddings.Include(w => w.User).SingleOrDefault(w => w.Id == (int)weddingId);
                //List<Guest> guests = _context.Guests.Where(g => g.WeddingId == (int)weddingId).Include(g => g.User).ToList();
                // List<User> allUsers = _context.Users.ToList();

                // ViewBag.Users = allUsers;
                ViewBag.BrightIdeas = bright_ideas;
                ViewBag.Guests = guests;
                return View();
            }
        }

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}