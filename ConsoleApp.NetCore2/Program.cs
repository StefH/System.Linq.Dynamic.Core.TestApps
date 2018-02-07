using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json;
using NFluent;

namespace ConsoleApp.NetCore2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // var b1 = new Blog { Rating = 10, Url = "http://test.com" };
                //var b1 = db.Blogs.First();

                //var p1 = new Post { Blog = b1, BlogId = b1.BlogId, Content = "p1", Title = "t1" };
                //var p2 = new Post { Blog = b1, BlogId = b1.BlogId, Content = "p2", Title = "t2" };

                //db.Posts.Add(p1);
                //db.Posts.Add(p2);

                //db.SaveChanges();
                var postList = db.Posts.ToList();

                var realQuery = db.Blogs.GroupJoin(
                    postList,
                    blog => blog.BlogId,
                    post => post.BlogId,
                    (blg, pst) => new { Name = blg.Url, NumberOfPosts = pst.Count() });

                var dynamicQuery = db.Blogs.AsQueryable().GroupJoin(
                    postList,
                    "BlogId",
                    "BlogId",
                    "new(outer.Url as Name, inner.Count() as NumberOfPosts)");

                // Assert
                var realResult = realQuery.ToArray();
                Check.That(realResult).IsNotNull();
                Console.WriteLine(JsonConvert.SerializeObject(realResult));

                var dynamicResult = dynamicQuery.ToDynamicArray();
                Check.That(dynamicResult).IsNotNull();
                Console.WriteLine(JsonConvert.SerializeObject(dynamicResult));
            }
        }
    }
}
