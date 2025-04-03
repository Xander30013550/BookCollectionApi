using Microsoft.EntityFrameworkCore;

namespace BookCollectionApi.Model
{
    public class BookContext : DbContext
    {
        //public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Library>()
        //        .HasMany(c => c.Books)
        //        .WithOne(c => c.library)
        //        .HasForeignKey(a => a.LibraryID);

        //    //modelBuilder.Seed();
        //}

        //public DbSet<Book> Books { get; set; }
        //public DbSet<Library> Librarys { get; set; }

    }
}
