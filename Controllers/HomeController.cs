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

        // Handling Lock Page
        [HttpGet("lock")]
        public IActionResult Lock()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            return View();
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
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
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
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
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
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
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
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
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
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
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
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
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
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
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
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
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
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
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
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
            Book ThisBook = dbContext.Books
                .Include(C => C.Creator)
                .Include(y => y.AllComments)
                    .ThenInclude(x => x.Creator)
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
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
            Book ThisBook = dbContext.Books
                .FirstOrDefault(b => b.BookId == IdBook);
            if(ThisBook.CreatorId != (int)HttpContext.Session.GetInt32("UserInSession"))
            {
                return RedirectToAction("Home");
            }
            return View(ThisBook);
        }
        // Showing admin verification page
        [HttpGet("logadmin")]
        public IActionResult LogAdmin()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.LockStat == true)
            {
                return RedirectToAction("Lock");
            }
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            } else if(ThisUser.IsMin == true){
                return View();
            }
            return RedirectToAction("Home");
        }

        

        
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ POST REQUESTS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // Handling account creator
        [HttpPost("accountcreator")]
        public IActionResult register(Log NewLog, User NewUser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.UserName == NewUser.UserName))
                {
                    ModelState.AddModelError("UserName", "User name is already in use.");
                    return View("Register");
                }
                List<User> AUs = dbContext.Users
                    .ToList();
                if(AUs.Count == 0)
                {
                    NewUser.IsMin = true;
                    NewUser.retmas = true;
                    NewUser.LockStat = false;
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                dbContext.Add(NewUser);
                NewLog.Info = "NEW ACCOUNT HAS BEEN CREATED: " + NewUser.FirstName + " " + NewUser.LastName + " USER NAME: " + NewUser.UserName;
                NewLog.CreatorId = NewUser.UserId;
                dbContext.Add(NewLog);
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




    // ADMIN OPTIONS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Allowing user to log in
        [HttpPost("adminaccess")]
        public IActionResult AdminAccess(LoginUser AccessUser)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            if(ModelState.IsValid)
            {
                var UserInDb = dbContext.Users
                    .FirstOrDefault(u => u.UserName == AccessUser.LUser);
                if(UserInDb == null)
                {
                    ModelState.AddModelError("LUser", "Invalid username or password");
                    return View("logadmin");
                }
                var Hasher = new PasswordHasher<LoginUser>();
                var Result = Hasher.VerifyHashedPassword(AccessUser, UserInDb.Password, AccessUser.LPassword);
                if(Result == 0)
                {
                    ModelState.AddModelError("LUser", "Invalid user name or password");
                    return View("logadmin");
                }
                if(UserInDb.IsMin == true)
                {
                    List<Log> AllLogs = dbContext.Logs
                        .Include(C => C.Creator)
                        .OrderByDescending(A => A.CreatedAt)
                        .ToList();
                    return View(AllLogs);
                }
                return RedirectToAction("Home");
            }
            return View("logadmin");
        }

        // Redirecting back to logs
        [HttpPost("adbacklogs")]
        public IActionResult adbacklogs()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            List<Log> AllLogs = dbContext.Logs
                .Include(C => C.Creator)
                .OrderByDescending(A => A.CreatedAt)
                .ToList();
            return View("AdminAccess", AllLogs);
        }
        
        // Controlling books
        [HttpPost("admanbooks")]
        public IActionResult AdManBooks()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            List<Book> ABooks = dbContext.Books
                        .Include(C => C.Creator)
                        .Include(co => co.AllComments)
                        .Include(l => l.LikedBy)
                        .OrderByDescending(a => a.LikedBy.Count)
                        .ToList();
            return View(ABooks);
        }

        // Removing book as admin
        [HttpPost("adremovebook/{IdBook}")]
        public IActionResult adremovebook(Log NewLog, int IdBook)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            Book ThisBook = dbContext.Books
                .Include(X => X.Creator)
                .FirstOrDefault(B => B.BookId == IdBook);
            NewLog.Info = "ADMIN " + ThisUser.FirstName + ThisUser.LastName + " REMOVED A BOOK: " + ThisBook.Name + " BOOK WAS RECOMMENDED BY " + ThisBook.Creator.FirstName + ThisBook.Creator.LastName;
            NewLog.CreatorId = ThisUser.UserId;
            dbContext.Add(NewLog);
            dbContext.Remove(ThisBook);
            dbContext.SaveChanges();
            List<Book> ABooks = dbContext.Books
                .Include(C => C.Creator)
                .Include(co => co.AllComments)
                .Include(l => l.LikedBy)
                .OrderByDescending(a => a.LikedBy.Count)
                .ToList();
            return View("AdManBooks", ABooks);
        }

        // All accounts
        [HttpPost("admanaccounts")]
        public IActionResult AdManAccounts()
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            List<User> AlUse = dbContext.Users
                .Include(C => C.CreatedBooks)
                .Include(B => B.BooksLiked)
                .Include(A => A.AllComments)
                .OrderByDescending(cr => cr.CreatedAt)
                .ToList();
            return View(AlUse);
        }

        // Removing Account
        [HttpPost("adremoveacc/{IdUser}")]
        public IActionResult adremoveacc(Log NewLog, int IdUser)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            User ToDel = dbContext.Users
                .FirstOrDefault(S => S.UserId == IdUser);
            NewLog.Info = "ADMIN " + ThisUser.FirstName + " " + ThisUser.LastName + " REMOVED THIS USER ACCOUNT: " + ToDel.FirstName + " " + ToDel.LastName + " USERNAME: " + ToDel.UserName;
            NewLog.CreatorId = ThisUser.UserId;
            dbContext.Add(NewLog);
            dbContext.Remove(ToDel);
            dbContext.SaveChanges();
            List<User> AlUse = dbContext.Users
                .Include(C => C.CreatedBooks)
                .Include(B => B.BooksLiked)
                .Include(A => A.AllComments)
                .OrderByDescending(cr => cr.CreatedAt)
                .ToList();
            return View("AdManAccounts", AlUse);
        }

        // Adding Admin
        [HttpPost("adcreatead/{IdUser}")]
        public IActionResult adcreatead(Log NewLog, int IdUser)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            User UpUser = dbContext.Users
                .FirstOrDefault(s => s.UserId == IdUser);
            UpUser.IsMin = true;
            UpUser.UpdatedAt = DateTime.Now;
            NewLog.Info = "ADMIN " + ThisUser.FirstName + " " + ThisUser.LastName + " UPGRADED THIS USER TO ADMIN: " + UpUser.FirstName + " " + UpUser.LastName + " USERNAME: " + UpUser.UserName;
            NewLog.CreatorId = ThisUser.UserId;
            dbContext.Add(NewLog);
            dbContext.SaveChanges();
            List<User> AlUse = dbContext.Users
                .Include(C => C.CreatedBooks)
                .Include(B => B.BooksLiked)
                .Include(A => A.AllComments)
                .OrderByDescending(cr => cr.CreatedAt)
                .ToList();
            return View("AdManAccounts", AlUse);
        }
        
        // Removing Admin
        [HttpPost("adremovead/{IdUser}")]
        public IActionResult adremovead(Log NewLog, int IdUser)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            User DownUser = dbContext.Users
                .FirstOrDefault(s => s.UserId == IdUser);
            DownUser.IsMin = false;
            DownUser.UpdatedAt = DateTime.Now;
            NewLog.Info = "ADMIN " + ThisUser.FirstName + " " + ThisUser.LastName + " DOWNGRADED THIS USER FROM ADMIN: " + DownUser.FirstName + " " + DownUser.LastName + " USERNAME: " + DownUser.UserName;
            NewLog.CreatorId = ThisUser.UserId;
            dbContext.Add(NewLog);
            dbContext.SaveChanges();
            List<User> AlUse = dbContext.Users
                .Include(C => C.CreatedBooks)
                .Include(B => B.BooksLiked)
                .Include(A => A.AllComments)
                .OrderByDescending(cr => cr.CreatedAt)
                .ToList();
            return View("AdManAccounts", AlUse);
        }

        // Unlocking Account
        [HttpPost("adunlockacc/{IdUser}")]
        public IActionResult adunlockacc(int IdUser, Log NewLog)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            User LockUser = dbContext.Users
                .FirstOrDefault(s => s.UserId == IdUser);
            LockUser.LockStat = false;
            NewLog.Info = "ADMIN " + ThisUser.FirstName + " " + ThisUser.LastName + " HAS UNLOCKED THE ACCOUNT OF " + LockUser.FirstName + " " + LockUser.LastName + " USER NAME: " + LockUser.UserName;
            NewLog.CreatorId = ThisUser.UserId;
            dbContext.Add(NewLog);
            dbContext.SaveChanges();
            List<User> AlUse = dbContext.Users
                .Include(C => C.CreatedBooks)
                .Include(B => B.BooksLiked)
                .Include(A => A.AllComments)
                .OrderByDescending(cr => cr.CreatedAt)
                .ToList();
            return View("AdManAccounts", AlUse); 
        }

        // Locking Account
        [HttpPost("adlockacc/{IdUser}")]
        public IActionResult adlockacc(int IdUser, Log NewLog)
        {
            int? Session = HttpContext.Session.GetInt32("UserInSession");
            if(Session == null)
            {
                return RedirectToAction("Login");
            }
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            if(ThisUser.IsMin == false)
            {
                return RedirectToAction("Home");
            }
            User LockUser = dbContext.Users
                .FirstOrDefault(s => s.UserId == IdUser);
            LockUser.LockStat = true;
            NewLog.Info = "ADMIN " + ThisUser.FirstName + " " + ThisUser.LastName + " HAS LOCKED THE ACCOUNT OF " + LockUser.FirstName + " " + LockUser.LastName + " USER NAME: " + LockUser.UserName;
            NewLog.CreatorId = ThisUser.UserId;
            dbContext.Add(NewLog);
            dbContext.SaveChanges();
            List<User> AlUse = dbContext.Users
                .Include(C => C.CreatedBooks)
                .Include(B => B.BooksLiked)
                .Include(A => A.AllComments)
                .OrderByDescending(cr => cr.CreatedAt)
                .ToList();
            return View("AdManAccounts", AlUse); 
        }




        // Adding new book 
        [HttpPost ("newbook")]
        public IActionResult newbook(Book NewBook, Log NewLog)
        {
            if(ModelState.IsValid)
            {
                User ThisUser = dbContext.Users
                    .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
                NewBook.CreatorId = (int)HttpContext.Session.GetInt32("UserInSession");
                dbContext.Add(NewBook);
                NewLog.Info = "NEW BOOK WAS RECOMMENDED BY: " + ThisUser.FirstName + " " + ThisUser.LastName + " USERNAME: " + ThisUser.UserName + " BOOK ADDED: " + NewBook.Name;
                NewLog.CreatorId = ThisUser.UserId;
                dbContext.Add(NewLog);
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
        public IActionResult deletebook(Log NewLog, int IdBook)
        {
            User ThisUser = dbContext.Users
                .FirstOrDefault(U => U.UserId == (int)HttpContext.Session.GetInt32("UserInSession"));
            Book ThisBook = dbContext.Books
                .FirstOrDefault(B => B.BookId == IdBook);
            NewLog.Info = "BOOK WAS DELETED BY: " + ThisUser.FirstName + " " + ThisUser.LastName + " USERNAME: " + ThisUser.UserName + " BOOK REMOVED: " + ThisBook.Name;
            NewLog.CreatorId = ThisUser.UserId;
            dbContext.Add(NewLog);
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
