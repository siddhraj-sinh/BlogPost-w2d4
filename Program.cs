

using var db = new BloggingContext();

db.Database.EnsureCreated();

// Start a transaction
using (var transaction = db.Database.BeginTransaction())
{
    try
    {
        // Create a new blog post
        var blog = new Blog { Title = "My new blog post", Content = "This is my new blog post content." };
        db.Blogs.Add(blog);
        db.SaveChanges();

        // Create comments for the blog post
        var comment1 = new Comment { Content = "Great post!", BlogId = blog.Id };
        var comment2 = new Comment { Content = "I enjoyed reading this!", BlogId = blog.Id };
        db.Comments.AddRange(comment1, comment2);
        db.SaveChanges();

        // Commit the transaction if everything is successful
        transaction.Commit();

        Console.WriteLine("Transaction committed successfully.");
    }
    catch (Exception ex)
    {
        // An error occurred, rollback the transaction
        transaction.Rollback();
        Console.WriteLine("Transaction rolled back due to an error: " + ex.Message);
    }
}

