using Microsoft.EntityFrameworkCore;
 
namespace BookClub.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get;set;}
        public DbSet<Book> Books {get;set;}
        public DbSet<UserBookRelation> UserBookRelations {get;set;}
        public DbSet<Comment> Comments {get;set;}

     
    }
}