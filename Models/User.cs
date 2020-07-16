using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BookClub.Models
{
    public class User
    {
        [Key]
        public int UserId{get;set;}


        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage ="First name must be atleast 2 characters long")]
        [StringLength(25, ErrorMessage = "First name cannot be more than 25 characters.")] 
        public string FirstName {get;set;}

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage ="Last name must be atleast 2 characters long")]
        [StringLength(25, ErrorMessage = "Last name cannot be more than 25 characters.")] 
        public string LastName {get;set;}

        [Required(ErrorMessage = "A user name is required")]
        [StringLength(25, ErrorMessage = "User name cannot be more than 25 characters.")] 
        public string UserName {get;set;}


        [Column("Password", TypeName = "LONGTEXT")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage ="Password must be atleast 5 characters long")]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^.*(?=.{6,18})(?=.*)(?=.*[A-Za-z]).*$", ErrorMessage = "Password must contain atleast 1 letter, 1 number")]
        public string Password {get;set;}

        public bool IsMin {get;set;} = false;

        public bool retmas {get;set;} = false;

        public bool LockStat {get;set;} = true;


/* -------------------------------------------------------------------------------- */
// DATETIMEs
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;


/* -------------------------------------------------------------------------------- */
// RELATIONS

        public List<Book> CreatedBooks {get;set;}

        public List<UserBookRelation> BooksLiked {get;set;} 

        public List<Comment> AllComments {get;set;}



/* -------------------------------------------------------------------------------- */
// PASSWORD COMPARING

        [NotMapped]
        [Compare("Password", ErrorMessage="Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword {get;set;}

        [NotMapped]
        [DataType(DataType.Password)]
        public string OldPassword {get;set;}

        
    }
}