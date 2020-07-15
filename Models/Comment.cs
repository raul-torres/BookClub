using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BookClub.Models
{
    public class Comment
    {
        [Key]
        public int CommentId {get;set;}

        [Required(ErrorMessage = "Please inlude your comment.")]
        [StringLength(300, ErrorMessage = "Comment cannot exceed 320 characters")] 
        public string Content {get;set;}

    /* -------------------------------------------------------------------------------- */
    // DATETIMEs
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    /* -------------------------------------------------------------------------------- */
    // RELATIONS
        public int CreatorId {get;set;}
        public User Creator {get;set;}

        public int BelongBookId {get;set;}
        public Book BelongBook {get;set;}
    }
}