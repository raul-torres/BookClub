using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BookClub.Models
{
    public class UserBookRelation
    {
        [Key]
        public int UserBookRelationId {get;set;}

        public int UserId{get;set;}

        public User User{get;set;}

        public int BookId {get;set;}

        public Book Book {get;set;}

         /* -------------------------------------------------------------------------------- */
        // DATETIMEs
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}