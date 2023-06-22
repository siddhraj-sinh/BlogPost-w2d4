

using Microsoft.EntityFrameworkCore;

using var db = new BloggingContext();

db.Database.EnsureCreated();

//create a new blog post

var blog = new Blog { Title = "My first blog post" ,Content="This is my first blog post content.."};

db.Blogs.Add(blog);
db.SaveChanges();

//create a comment for the blog post

var comment1 = new Comment { Content = "Great Post !",BlogId=blog.Id };

var comment2 = new Comment { Content = "I enjoyed reading this !",BlogId=blog.Id };
db.Comments.AddRange(comment1, comment2);
db.SaveChanges();


// User 1 edits the blog post
using (var context1 = new BloggingContext())
{
    var blog1 = context1.Blogs.FirstOrDefault();
    if (blog1 != null)
    {
        // Perform the desired edits
        blog1.Title = "Updated Title by User 1";
        blog1.Content = "Updated Content by User 1";

        context1.SaveChanges();
    }
}
// Introduce a delay before User 2 edits the same blog post
await Task.Delay(TimeSpan.FromSeconds(2));
// User 2 edits the same blog post
using (var context2 = new BloggingContext())
{
    var blog2 = context2.Blogs.FirstOrDefault();
    if (blog2 != null)
    {
        // Perform the desired edits
        blog2.Title = "Updated Title by User 2";
        blog2.Content = "Updated Content by User 2";

        try
        {
            context2.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var entry = ex.Entries.FirstOrDefault();
            if (entry != null)
            {
                var databaseValues = entry.GetDatabaseValues();
                var currentTitle = databaseValues.GetValue<string>("Title");
                var currentContent = databaseValues.GetValue<string>("Content");

                Console.WriteLine("Concurrency conflict occurred!");
                Console.WriteLine($"Current Title: {currentTitle}");
                Console.WriteLine($"Current Content: {currentContent}");
            }
        }
    }
}


