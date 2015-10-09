namespace Sort2015.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Sort2015.Data.Models;

    public partial class GLFeedContext : DbContext
    {
        public GLFeedContext()
            : base("name=GLFeedContext")
        {
        }

        public virtual DbSet<DailyGem> DailyGems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyGem>()
                .Property(e => e.Guid)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.RawDescription)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.Quote)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.LdsOrgUrl)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.SourceRss)
                .IsUnicode(false);

            modelBuilder.Entity<DailyGem>()
                .Property(e => e.LangCode)
                .IsUnicode(false);
        }
    }
}
