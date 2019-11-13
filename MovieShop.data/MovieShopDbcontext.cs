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

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Purchase> purchases { get; set; }

        //all rules for dbset
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Movie>(ConfigureGenre);
            modelBuilder.Entity<MovieGenre>(ConfigureGenre);
            modelBuilder.Entity<User>(ConfigureGenre);
            modelBuilder.Entity<Role>(ConfigureGenre);
            modelBuilder.Entity<UserRole>(ConfigureGenre);
            modelBuilder.Entity<Purchase>(ConfigureGenre);

        }

        private void ConfigureGenre(EntityTypeBuilder<Purchase> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("Purchases");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.PurchaseNumber).ValueGeneratedOnAdd();
            builder.HasIndex(p => new { p.UserId, p.MovieId }).IsUnique();

        }

        private void ConfigureGenre(EntityTypeBuilder<UserRole> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            builder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
        }

        private void ConfigureGenre(EntityTypeBuilder<Role> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("Roles");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(20);

        }

        private void ConfigureGenre(EntityTypeBuilder<User> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.IsLocked).HasDefaultValue(false);
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
