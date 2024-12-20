﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
using Skillshare.Domain.Entities;

namespace Skillshare.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseCategory> courseCategories { get; set; }

        public DbSet<CourseRequirment> courseRequirments { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<UserCourseInrollment> students { get; set; }

        public DbSet<WhatYouLearnFromCourse> whatyouWillLearn { get; set; }
        public DbSet<WhoIsthisCoursefor> whoIsthisCoursefor { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<CourseLanguge> CourseLanguges { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CartItem>()
                .HasOne(c => c.cart)
                .WithMany(c => c.cartItems)
                .HasForeignKey(c => c.cartId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}