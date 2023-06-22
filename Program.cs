

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

var b = db.Blogs.FirstOrDefault(); // Retrieve a blog post

if (b != null)
{
    Console.WriteLine($"Title: {b.Title}");
    Console.WriteLine($"Content: {b.Content}");
    Console.WriteLine("Comments:");

    // Access the comments collection to trigger lazy loading
    foreach (var comment in b.Comments)
    {
        Console.WriteLine($"- {comment.Content}");
    }
}
//In this code, the blog post is retrieved using FirstOrDefault, but the comments are not loaded immediately. 
// When you access the Comments collection within the foreach loop, lazy loading is triggered,
// and the associated comments are loaded from the database at that point.


