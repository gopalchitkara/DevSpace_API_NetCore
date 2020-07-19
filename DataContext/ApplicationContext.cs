using DevSpace_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevSpace_API.DataContext
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReadingList> ReadingLists { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<HashtagForWidget> HashtagsForWidgets { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<DraftHashtag> DraftHashtags { get; set; }
    }
}