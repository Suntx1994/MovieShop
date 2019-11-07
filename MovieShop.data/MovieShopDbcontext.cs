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

        public DbSet<Movie> Moives { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        //all rules for dbset
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Movie>(ConfigureGenre);
            modelBuilder.Entity<MovieGenre>(ConfigureGenre);

        }

        private void ConfigureGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenres");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);
        }

        private void ConfigureGenre(EntityTypeBuilder<Movie> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Title).IsUnique(false);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.HomePage).HasMaxLength(2084);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)");

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
