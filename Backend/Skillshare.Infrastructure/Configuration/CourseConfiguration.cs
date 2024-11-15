using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Domain.Entities;

namespace Skillshare.Infrastructure.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.Requirments).WithOne(c => c.Course).HasForeignKey(c => c.CourseId);
            builder.HasMany(c => c.whatYouLearnFromCourse).WithOne(c => c.Course).HasForeignKey(c => c.CourseId);
            builder.HasMany(c => c.whoIsthisCoursefors).WithOne(c => c.Course).HasForeignKey(c => c.CourseId);

            builder.HasMany(c => c.Students).WithMany(c => c.coursesInrollments).UsingEntity<UserCourseInrollment>();
            builder.HasOne(c => c.Instructor).WithMany(c => c.CoursesICreated)
                .HasForeignKey(c => c.InstructorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.category).WithMany(c => c.Courses).HasForeignKey(c => c.CategoryId);
        }
    }
}
// course has one instructor and instructor has many courses with foreign key instrustorid 