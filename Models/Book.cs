using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BookClub.Models
{
    public class Book
    {
        [Key]
        public int BookId {get;set;}

        [Required(ErrorMessage = "Book name is required")]
        [StringLength(25, ErrorMessage = "Book name cannot be more than 25 characters.")] 
        public string Name {get;set;}

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(25, ErrorMessage = "Author name cannot be more than 25 characters.")] 
        public string Author {get;set;}

        [StringLength(320, ErrorMessage = "Notes cannot exceed 320 characters")] 
        public string Notes {get;set;}

    /* -------------------------------------------------------------------------------- */
    // DATETIMEs
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    /* -------------------------------------------------------------------------------- */
    // RELATIONS

        public int CreatorId {get;set;}
        public User Creator {get;set;}

        public List<UserBookRelation> LikedBy {get;set;} 

        public List<Comment> AllComments {get;set;}

    }
}