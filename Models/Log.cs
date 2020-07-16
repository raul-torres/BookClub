using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BookClub.Models
{
    public class Log
    {
        [Key]
        public int LogId {get;set;}

        public string Info {get;set;}

        
/* -------------------------------------------------------------------------------- */
// DATETIMEs
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;


/* -------------------------------------------------------------------------------- */
// RELATIONS

        public int CreatorId {get;set;}

        public User Creator {get;set;}

    }
}