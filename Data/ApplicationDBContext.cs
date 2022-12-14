using Microsoft.EntityFrameworkCore;
using Wemuda_book_app.Model;

namespace Wemuda_book_app.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<StatusUpdate> StatusUpdates { get; set;}
        public DbSet<BookStatus> BookStatuses { get; set; }
    }
}
