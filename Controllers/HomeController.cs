using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookClub.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace BookClub.Controllers
{
    public class HomeController : Controller
    {
        // Connection to EFC
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        // Handling register form
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        // Handling login form
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        // Handling logout
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        // Handling home page
        [HttpGet("home")]
        public IActionResult Home()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == HttpContext.Session.GetInt32("UserInSession"));
            List<Book> AllBooks = dbContext.Books
                .Include(L => L.LikedBy)
                .ThenInclude(U => U.User)
                .OrderByDescending(a => a.LikedBy.Count)
                .ToList();
            ViewBag.User = ThisUser;
            return View(AllBooks);
        }

        // Handling my books page
        [HttpGet("myhome")]
        public IActionResult MyHome()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == HttpContext.Session.GetInt32("UserInSession"));
            List<Book> AllBooks = dbContext.Books
                .Where(t => t.CreatorId == ThisUser.UserId)
                .Include(L => L.LikedBy)
                .ThenInclude(U => U.User)
                .OrderByDescending(a => a.LikedBy.Count)
                .ToList();
            ViewBag.User = ThisUser;
            return View(AllBooks);
        }
        // Handling my books page
        [HttpGet("likedhome")]
        public IActionResult LikedHome()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == HttpContext.Session.GetInt32("UserInSession"));
            List<Book> AllBooks = dbContext.Books
                .Include(L => L.LikedBy)
                .ThenInclude(U => U.User)
                .OrderByDescending(a => a.LikedBy.Count)
                .ToList();
            ViewBag.User = ThisUser;
            return View(AllBooks);
        }


        // Handling adding book form
        [HttpGet("addbook")]
        public IActionResult AddBook()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // Handling account section
        [HttpGet("account")]
        public IActionResult Account()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            return View(ThisUser);
        }

        // Handling first name update form
        [HttpGet("updatefirstname")]
        public IActionResult UpdateFirstName()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            return View(ThisUser);
        }

        // Handling last name update form
        [HttpGet("updatelastname")]
        public IActionResult UpdateLastName()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            return View(ThisUser);
        }

        // Handling User name update form
        [HttpGet("updateusername")]
        public IActionResult UpdateUserName()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            return View(ThisUser);
        }

        // Handling password update form
        [HttpGet("updatepassword")]
        public IActionResult UpdatePassword()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // Showing information about this book
        [HttpGet("thisbook/{IdBook}")]
        public IActionResult ThisBook(int IdBook)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            Book ThisBook = dbContext.Books
                .Include(C => C.Creator)
                .Include(Co => Co.AllComments)
                .Include(L => L.LikedBy)
                .ThenInclude(U => U.User)
                .FirstOrDefault(B => B.BookId == IdBook);
            ViewBag.Book = ThisBook;
            ViewBag.CurrentUser = (int)HttpContext.Session.GetInt32("UserInSession");
            return View();
        }

        // Showing book edit page
        [HttpGet("editbook/{IdBook}")]
        public IActionResult EditBook(int IdBook)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            } 
            Book ThisBook = dbContext.Books
                .FirstOrDefault(b => b.BookId == IdBook);
            if(ThisBook.CreatorId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(ThisBook);
        }

        

        
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ POST REQUESTS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // Handling account creator
        [HttpPost("accountcreator")]
        public IActionResult register(User NewUser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.UserName == NewUser.UserName))
                {
                    ModelState.AddModelError("UserName", "User name is already in use.");
                    return View("Register");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                dbContext.Add(NewUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserInSession", NewUser.UserId);
                return RedirectToAction("Home");
            }
            return View("Register");
        }
        
        // Allowing user to log in
        [HttpPost("access")]
        public IActionResult access(LoginUser AccessUser)
        {
            if(ModelState.IsValid)
            {
                var UserInDb = dbContext.Users.FirstOrDefault(u => u.UserName == AccessUser.LUser);
                if(UserInDb == null)
                {
                    ModelState.AddModelError("LUser", "Invalid username or password");
                    return View("Login");
                }
                var Hasher = new PasswordHasher<LoginUser>();
                var Result = Hasher.VerifyHashedPassword(AccessUser, UserInDb.Password, AccessUser.LPassword);
                if(Result == 0)
                {
                    ModelState.AddModelError("LUser", "Invalid user name or password");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserInSession", UserInDb.UserId);
                return RedirectToAction("Home");
            }
            return View("Login");
        }

        // Adding new book 
        [HttpPost ("newbook")]
        public IActionResult newbook(Book NewBook)
        {
            if(ModelState.IsValid)
            {
                NewBook.CreatorId = (int)HttpContext.Session.GetInt32("UserInSession");
                dbContext.Add(NewBook);
                dbContext.SaveChanges();
                return RedirectToAction("Home"); 
            }
            return View("addbook");
        }

        // Saving first name changes
        [HttpPost ("saveupdatefirstname")]
        public IActionResult saveupdatefirstname(User UpdatedUser)
        {
            ModelState.Remove("LastName");
            ModelState.Remove("UserName");
            ModelState.Remove("Password");
            if(ModelState.IsValid)
            {
                User CurrentUser = dbContext.Users
                    .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                CurrentUser.FirstName = UpdatedUser.FirstName;
                CurrentUser.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Account");
            }
            return View("updatefirstname");
        }

        // Saving last name changes
        [HttpPost ("saveupdatelastname")]
        public IActionResult saveupdatelastname(User UpdatedUser)
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("UserName");
            ModelState.Remove("Password");
            if(ModelState.IsValid)
            {
                User CurrentUser = dbContext.Users
                    .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                CurrentUser.LastName = UpdatedUser.LastName;
                CurrentUser.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Account");
            }
            return View("updatelastname");
        }

        // Saving user name changes
        [HttpPost ("saveupdateusername")]
        public IActionResult saveupdateusername(User UpdatedUser)
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Password");
            if(ModelState.IsValid)
            {
                User CurrentUser = dbContext.Users
                    .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                CurrentUser.UserName = UpdatedUser.UserName;
                CurrentUser.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Account");
            }
            return View("updateusername");
        }

        // Saving password changes
        [HttpPost("saveupdatepassword")]
            public IActionResult saveupdatepassword(User UpdatedUser)
            {
                if(UpdatedUser.OldPassword == null)
                {
                    ModelState.AddModelError("OldPassword", "Old password is required");
                    return View("updatepassword");
                }
                var UserInDb = dbContext.Users
                    .FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                var Hasher = new PasswordHasher<User>();
                var Result = Hasher.VerifyHashedPassword(UpdatedUser, UserInDb.Password, UpdatedUser.OldPassword);
                if(Result == 0)
                {
                    ModelState.AddModelError("OldPassword", "Invalid password");
                    return View("updatepassword");
                }
                ModelState.Remove("FirstName");
                ModelState.Remove("LastName");
                ModelState.Remove("UserName");
                if(ModelState.IsValid)
                {
                    UpdatedUser.Password = Hasher.HashPassword(UpdatedUser, UpdatedUser.Password);
                    UserInDb.Password = UpdatedUser.Password;
                    UserInDb.UpdatedAt = DateTime.Now;
                    dbContext.SaveChanges();
                    return RedirectToAction("Account");
                }
                return View("updatepassword");
            }
        // Handling new comment creation 
        [HttpPost("newcomment/{IdBook}")]
        public IActionResult newcomment(Comment NewComment, int IdBook)
        {
            if(ModelState.IsValid)
            {
                NewComment.CreatorId = (int)HttpContext.Session.GetInt32("UserInSession");
                NewComment.BelongBookId = IdBook;
                dbContext.Add(NewComment);
                dbContext.SaveChanges();
                return Redirect("/thisbook/" + IdBook);
            }
            return View("ThisBook");
        }

        // Liking a book from home
        [HttpPost("home_likebook/{IdBook}")]
        public IActionResult home_likebook(UserBookRelation NewRelation, int IdBook)
        {
            NewRelation.UserId = (int)HttpContext.Session.GetInt32("UserInSession");
            NewRelation.BookId = IdBook;
            dbContext.Add(NewRelation);
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }

        // Unliking a book from home
        [HttpPost("home_unlikebook/{IdBook}")]
        public IActionResult home_unlikebook(int IdBook)
        {
            UserBookRelation toDelete = dbContext.UserBookRelations
                .Where(R => R.BookId == IdBook)
                .FirstOrDefault(Us => Us.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            dbContext.Remove(toDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }

        // Unliking a book from likedHome
        [HttpPost("likedhome_unlikebook/{IdBook}")]
        public IActionResult likedhome_unlikebook(int IdBook)
        {
            UserBookRelation toDelete = dbContext.UserBookRelations
                .Where(R => R.BookId == IdBook)
                .FirstOrDefault(Us => Us.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            dbContext.Remove(toDelete);
            dbContext.SaveChanges();
            return RedirectToAction("LikedHome");
        }

        // Liking a book from info page
        [HttpPost("thisbook_like/{IdBook}")]
        public IActionResult thisbook_likebook(UserBookRelation NewRelation, int IdBook)
        {
            NewRelation.UserId = (int)HttpContext.Session.GetInt32("UserInSession");
            NewRelation.BookId = IdBook;
            dbContext.Add(NewRelation);
            dbContext.SaveChanges();
            return Redirect("/thisbook/" + IdBook);
        }
         // Unliking a book from info page
        [HttpPost("thisbook_unlike/{IdBook}")]
        public IActionResult thisbook_unlikebook(int IdBook)
        {
            UserBookRelation toDelete = dbContext.UserBookRelations
                .Where(R => R.BookId == IdBook)
                .FirstOrDefault(Us => Us.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            dbContext.Remove(toDelete);
            dbContext.SaveChanges();
            return Redirect("/thisbook/" + IdBook);
        }

        // Deleting a comment
        [HttpPost("commentdelete/{IdComment}/{IdBook}")]
        public IActionResult commentdelete(int IdComment, int IdBook)
        {
            Comment ThisComment = dbContext.Comments
                .FirstOrDefault(C => C.CommentId == IdComment);
            dbContext.Remove(ThisComment);
            dbContext.SaveChanges();
            return Redirect("/thisbook/" + IdBook);
        }

        // Deleting a book
        [HttpPost("deletebook/{IdBook}")]
        public IActionResult deletebook(int IdBook)
        {
            Book ThisBook = dbContext.Books
                .FirstOrDefault(B => B.BookId == IdBook);
            dbContext.Remove(ThisBook);
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }
        
        // Saving Changes to book
        [HttpPost("savebookchanges/{IdBook}")]
        public IActionResult EditBook(Book UpdatedBook, int IdBook)
        {
            Book ThisBook = dbContext.Books
                .FirstOrDefault(B => B.BookId == IdBook);
            if(ModelState.IsValid)
            {
                ThisBook.Name = UpdatedBook.Name;
                ThisBook.Author = UpdatedBook.Author;
                ThisBook.Notes = UpdatedBook.Notes;
                ThisBook.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return Redirect("/thisbook/" + IdBook);
            }
            return View(ThisBook);
        }

    }
}
