

using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public string DbPath { get; }
    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blog.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Data Source={DbPath}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Blog>().ToTable("MyBlogs");

        //modelBuilder.Entity<Blog>().HasMany(b => b.Comments).WithOne(c => c.Blog).HasForeignKey(c => c.BlogId);
        modelBuilder.Entity<Blog>()
        .Property(b => b.ConcurrencyToken)
        .IsConcurrencyToken();
    }
}

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public virtual List<Comment> Comments { get; set; }
    public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
}

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }

}