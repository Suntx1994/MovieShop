using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MovieShop.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieShop.Data
{
    public class MovieShopDbcontext : DbContext
    {
        public MovieShopDbcontext(DbContextOptions<MovieShopDbcontext> options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }

        //all rules for dbset
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(ConfigureGenre);
        }

        private void ConfigureGenre(EntityTypeBuilder<Genre> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("Genres");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).HasMaxLength(25);

        }
    }
}
